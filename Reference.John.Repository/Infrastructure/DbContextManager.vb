Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure


Namespace Infrastructure
    Public Class DbContextManager

        Public Shared Sub Init(Optional lazyLoadingEnabled As Boolean = True)
            Init(DefaultConnectionStringName, lazyLoadingEnabled)
        End Sub

        Public Shared Sub Init(connectionStringName As String, Optional lazyLoadingEnabled As Boolean = True)
            AddConfiguration(connectionStringName, lazyLoadingEnabled)
        End Sub

        Public Shared Sub InitStorage(storage As IDbContextStorage)
            If storage Is Nothing Then
                Throw New ArgumentNullException("storage")
            End If
            If (_storage IsNot Nothing) AndAlso (_storage IsNot storage) Then                
                Throw New ApplicationException("A storage mechanism has already been configured for this application")
            End If
            _storage = storage
        End Sub

        ''' <summary>
        ''' The default connection string name used if only one database is being communicated with.
        ''' </summary>
        Public Shared ReadOnly DefaultConnectionStringName As String = "DefaultDb"

        ''' <summary>
        ''' Used to get the current db context session if you're communicating with a single database.
        ''' When communicating with multiple databases, invoke <see cref="CurrentFor" /> instead.
        ''' </summary>
        Public Shared ReadOnly Property Current() As DbContext
            Get
                Return CurrentFor(DefaultConnectionStringName)
            End Get
        End Property

        ''' <summary>
        ''' Used to get the current DbContext associated with a key; i.e., the key 
        ''' associated with an object context for a specific database.
        ''' 
        ''' If you're only communicating with one database, you should call <see cref="Current" /> instead,
        ''' although you're certainly welcome to call this if you have the key available.
        ''' </summary>
        Public Shared Function CurrentFor(key As String) As DbContext
            If String.IsNullOrEmpty(key) Then
                Throw New ArgumentNullException("key")
            End If

            If _storage Is Nothing Then
                Throw New ApplicationException("An IDbContextStorage has not been initialized")
            End If

            Dim context As DbContext = Nothing
            SyncLock _syncLock
                If Not _dbContextBuilders.ContainsKey(key) Then
                    Throw New ApplicationException("An DbContextBuilder does not exist with a key of " & key)
                End If

                context = _storage.GetDbContextForKey(key)

                If context Is Nothing Then
                    context = _dbContextBuilders(key).BuildDbContext()
                    _storage.SetDbContextForKey(key, context)
                End If
            End SyncLock
            Return context
        End Function

        ''' <summary>
        ''' This method is used by application-specific db context storage implementations
        ''' and unit tests. Its job is to walk thru existing cached object context(s) and Close() each one.
        ''' </summary>
        Public Shared Sub CloseAllDbContexts()
            For Each ctx As DbContext In _storage.GetAllDbContexts()
                If DirectCast(ctx, IObjectContextAdapter).ObjectContext.Connection.State = System.Data.ConnectionState.Open Then
                    DirectCast(ctx, IObjectContextAdapter).ObjectContext.Connection.Close()
                End If
            Next
        End Sub

        Private Shared Sub AddConfiguration(connectionStringName As String, lazyLoadingEnabled As Boolean)
            If String.IsNullOrEmpty(connectionStringName) Then
                Throw New ArgumentNullException("connectionStringName")
            End If

            If _dbContextBuilders.ContainsKey(connectionStringName) Then
                Throw New ArgumentException("Connection String is already setup.  This would normally be an exception.")
            End If
            SyncLock _syncLock
                _dbContextBuilders.Add(connectionStringName, New DbContextBuilder(Of DbContext)(lazyLoadingEnabled, connectionStringName))
            End SyncLock
        End Sub

        ''' <summary>
        ''' An application-specific implementation of IDbContextStorage must be setup either thru
        ''' <see cref="InitStorage" /> or one of the <see cref="Init" /> overloads. 
        ''' </summary>
        Private Shared Property _storage() As IDbContextStorage

        ''' <summary>
        ''' Maintains a dictionary of db context builders, one per database.  The key is a 
        ''' connection string name used to look up the associated database, and used to decorate respective
        ''' repositories. If only one database is being used, this dictionary contains a single
        ''' factory with a key of <see cref="DefaultConnectionStringName" />.
        ''' </summary>
        Private Shared _dbContextBuilders As New Dictionary(Of String, IDbContextBuilder(Of DbContext))()

        Private Shared _syncLock As New Object()
    End Class
End Namespace