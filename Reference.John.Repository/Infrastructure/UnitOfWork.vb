Imports System.Data
Imports System.Data.Common
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports EntityFramework.Extensions

Namespace Infrastructure
    Public Class UnitOfWork
        Implements IUnitOfWork

        Private _transaction As DbTransaction
        Private _dbContext As DbContext
        Private _auditLogger As EntityFramework.Audit.AuditLogger
        Private _auditLog As EntityFramework.Audit.AuditLog

        Public Sub New(context As DbContext)
            _dbContext = context
            InitializeAuditLogExtensions()
        End Sub

        Private Sub InitializeAuditLogExtensions()
            Dim _auditConfiguration = EntityFramework.Audit.AuditConfiguration.Default

            _auditConfiguration.IncludeRelationships = True
            _auditConfiguration.LoadRelationships = False
            _auditConfiguration.DefaultAuditable = True

            'initializing the auditlog
            _auditLogger = _dbContext.BeginAudit()
        End Sub

        Public ReadOnly Property IsInTransaction() As Boolean Implements IUnitOfWork.IsInTransaction
            Get
                Return _transaction IsNot Nothing
            End Get
        End Property

        Public Sub BeginTransaction() Implements IUnitOfWork.BeginTransaction
            BeginTransaction(IsolationLevel.ReadCommitted)
        End Sub

        Public Sub BeginTransaction(isolationLevel As IsolationLevel) Implements IUnitOfWork.BeginTransaction
            If _transaction IsNot Nothing Then
                Throw New ApplicationException("Cannot begin a new transaction while an existing transaction is still running. " & "Please commit or rollback the existing transaction before starting a new one.")
            End If
            OpenConnection()
            _transaction = DirectCast(_dbContext, IObjectContextAdapter).ObjectContext.Connection.BeginTransaction(isolationLevel)
        End Sub

        Public Sub RollBackTransaction() Implements IUnitOfWork.RollBackTransaction
            If _transaction Is Nothing Then
                Throw New ApplicationException("Cannot roll back a transaction while there is no transaction running.")
            End If

            If IsInTransaction Then
                _transaction.Rollback()
                ReleaseCurrentTransaction()
            End If
        End Sub

        Public Sub CommitTransaction() Implements IUnitOfWork.CommitTransaction
            If _transaction Is Nothing Then
                Throw New ApplicationException("Cannot roll back a transaction while there is no transaction running.")
            End If

            Try
                AddDateStamps()
                _auditLog = _auditLogger.CreateLog()
                _dbContext.SaveChanges()
                _auditLog.Refresh()
                'push audit changes into db as part of transaction
                SaveAuditChanges(_auditLog)

                _transaction.Commit()
                ReleaseCurrentTransaction()

            Catch ex As Exception
                RollBackTransaction()
                Throw
            End Try
        End Sub

        Public Sub SaveChanges() Implements IUnitOfWork.SaveChanges
            If IsInTransaction Then
                Throw New ApplicationException("A transaction is running. Call CommitTransaction instead.")
            End If
            Try
                BeginTransaction()
                AddDateStamps()
                _auditLog = _auditLogger.CreateLog()
                _dbContext.SaveChanges()
                _auditLog.Refresh()
                'push audit changes into db as part of transaction
                SaveAuditChanges(_auditLog)
                _transaction.Commit()
                ReleaseCurrentTransaction()
            Catch ex As Exception
                RollBackTransaction()
                Throw
            End Try
        End Sub

        Public Sub SaveChanges(saveOptions As System.Data.Entity.Core.Objects.SaveOptions) Implements IUnitOfWork.SaveChanges
            If IsInTransaction Then
                Throw New ApplicationException("A transaction is running. Call CommitTransaction instead.")
            End If
            Try
                AddDateStamps()
                _auditLog = _auditLogger.CreateLog()
                DirectCast(_dbContext, IObjectContextAdapter).ObjectContext.SaveChanges(saveOptions)
                _auditLog.Refresh()
                'push audit changes into db as part of transaction
                SaveAuditChanges(_auditLog)
                _transaction.Commit()
                ReleaseCurrentTransaction()
            Catch ex As Exception
                RollBackTransaction()
                Throw
            End Try
        End Sub

#Region "Implementation of IDisposable"

        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Disposes off the managed and unmanaged resources used.
        ''' </summary>
        ''' <param name="disposing"></param>
        Private Sub Dispose(disposing As Boolean)
            If Not disposing Then
                Return
            End If

            If _disposed Then
                Return
            End If

            _disposed = True
        End Sub

        Private _disposed As Boolean
#End Region

        Private Sub OpenConnection()
            If DirectCast(_dbContext, IObjectContextAdapter).ObjectContext.Connection.State <> ConnectionState.Open Then
                DirectCast(_dbContext, IObjectContextAdapter).ObjectContext.Connection.Open()
            End If
        End Sub

        ''' <summary>
        ''' Releases the current transaction
        ''' </summary>
        Private Sub ReleaseCurrentTransaction()
            If _transaction IsNot Nothing Then
                _transaction.Dispose()
                _transaction = Nothing
            End If
        End Sub

        Private Sub AddDateStamps()
            _dbContext.ChangeTracker.DetectChanges()
            Dim context As Core.Objects.ObjectContext = DirectCast(_dbContext, IObjectContextAdapter).ObjectContext

            'Find all Entities that are Added/Modified that implement the date interface
            Dim objectStateEntries As IEnumerable(Of Core.Objects.ObjectStateEntry) = From e In context.ObjectStateManager.GetObjectStateEntries(EntityState.Added Or EntityState.Modified)
                                                                                Where e.IsRelationship = False AndAlso e.Entity IsNot Nothing _
                                                                                AndAlso GetType(Domain.Interfaces.IEntityDates).IsAssignableFrom(e.Entity.[GetType]())

            Dim currentTime = Domain.Providers.TimeProvider.Current.UtcNow

            For Each item In objectStateEntries
                Dim entityBase = TryCast(item.Entity, Domain.Interfaces.IEntityDates)

                If item.State = EntityState.Added Then
                    entityBase.DateCreated = currentTime
                End If

                entityBase.LastChangeDate = currentTime
            Next
        End Sub

        Private Sub SaveAuditChanges(items As EntityFramework.Audit.AuditLog)
            'one option is to fire a procedure for each audit record. 
            'this would need to be wrapped in a transaction with the rest of the change
            items.Entities.ForEach(Sub(x)
                                       Dim _params As New List(Of Object)
                                       _params.Add(New SqlClient.SqlParameter("Action", x.Action))
                                       _params.Add(New SqlClient.SqlParameter("Type", x.EntityType.FullName))
                                       If x.Keys.Count > 1 Then Throw New ArgumentOutOfRangeException("keys")
                                       _params.Add(New SqlClient.SqlParameter("EntityKey", x.Keys(0).Value))
                                       _params.Add(New SqlClient.SqlParameter("ChangeSet", items.ToXml))
                                       _params.Add(New SqlClient.SqlParameter("CountofFieldsModified", x.Properties.Count))
                                       _params.Add(New SqlClient.SqlParameter("LastChangeUser", items.Username))
                                       _dbContext.Database.ExecuteSqlCommand("InsertAuditLog @Action,@Type, @EntityKey,@ChangeSet, @CountofFieldsModified, @LastChangeUser", _params.ToArray)
                                   End Sub)
            'other option would be to have the audit table mapped to EF and perform the "adds" to the audit table before save changes takes place
            'keep in mind that if performing this option any new records created would not have the correct sequence values present because they wouldn't have the database sequence number present. and client side guids generated would be present.
            'not currently doing this because can't cast from the standard context to the custom context and don't want to take a dependency on that.
            'Dim t = DirectCast(_dbContext, Reference.John.Model.Reference_JohnEntities)
            'items.Entities.ForEach(Sub(x)
            'If x.Keys.Count > 1 Then Throw New ArgumentOutOfRangeException("keys")
            't.AuditLogs.Add(New Reference.John.Domain.AuditLog With {.Action = x.Action, .Type = x.EntityType.FullName, .EntityKey = x.Keys(0).Value, .ChangeSet = items.ToXml, .CountofFieldsModified = x.Properties.Count, .LastChangeUser = items.Username})
            '                      End Sub)

        End Sub

    End Class

End Namespace