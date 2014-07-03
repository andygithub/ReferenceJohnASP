﻿Imports System.Collections.Generic
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Data.Entity
Imports System.Data
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Core
Imports System.Data.Entity.Core.Metadata.Edm

''' <summary>
''' Generic repository
''' </summary>
Public Class GenericRepository
    Implements IRepository
    Private ReadOnly _connectionStringName As String
    Private _context As DbContext

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericRepository"/> class.
    ''' </summary>
    Public Sub New()
        Me.New(String.Empty)
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericRepository"/> class.
    ''' </summary>
    ''' <param name="connectionStringName">Name of the connection string.</param>
    Public Sub New(connectionStringName As String)
        Me._connectionStringName = connectionStringName
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the <see cref="GenericRepository"/> class.
    ''' </summary>
    ''' <param name="context">The context.</param>
    Public Sub New(context As DbContext)
        If context Is Nothing Then Throw New ArgumentNullException("context")
        _context = context
    End Sub

    Public Sub New(context As ObjectContext)
        If context Is Nothing Then Throw New ArgumentNullException("context")
        _context = New DbContext(context, True)
    End Sub

    Public Function GetByKey(Of TEntity As Class)(keyValue As Object) As TEntity Implements IRepository.GetByKey
        Dim key As EntityKey = GetEntityKey(Of TEntity)(keyValue)

        Dim originalItem As Object = Nothing
        If DirectCast(DbContext, IObjectContextAdapter).ObjectContext.TryGetObjectByKey(key, originalItem) Then
            Return DirectCast(originalItem, TEntity)
        End If
        Return Nothing
    End Function

    Public Function GetQuery(Of TEntity As Class)() As IQueryable(Of TEntity) Implements IRepository.GetQuery


        ' 
        '             * From CTP4, I could always safely call this to return an IQueryable on DbContext 
        '             * then performed any with it without any problem:
        '             

        Return DbContext.Set(Of TEntity)()

        '
        '             * but with 4.1 release, when I call GetQuery<TEntity>().AsEnumerable(), there is an exception:
        '             * ... System.ObjectDisposedException : The ObjectContext instance has been disposed and can no longer be used for operations that require a connection.
        '             


        ' here is a work around: 
        ' - cast DbContext to IObjectContextAdapter then get ObjectContext from it
        ' - call CreateQuery<TEntity>(entityName) method on the ObjectContext
        ' - perform querying on the returning IQueryable, and it works!
        'Dim entityName = GetEntityName(Of TEntity)()
        'Return DirectCast(DbContext, IObjectContextAdapter).ObjectContext.CreateQuery(Of TEntity)(entityName)
    End Function

    Public Function GetQuery(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As IQueryable(Of TEntity) Implements IRepository.GetQuery
        Return GetQuery(Of TEntity)().Where(predicate)
    End Function

    Public Function [Get](Of TEntity As Class, TOrderBy)(orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional order As SortOrder = SortOrder.Ascending) As IEnumerable(Of TEntity) Implements IRepository.Get
        If order = SortOrder.Ascending Then
            Return GetQuery(Of TEntity)().OrderBy(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).AsEnumerable()
        End If
        Return GetQuery(Of TEntity)().OrderByDescending(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).AsEnumerable()
    End Function

    Public Function [Get](Of TEntity As Class, TOrderBy)(criteria As Expression(Of Func(Of TEntity, Boolean)), orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional order As SortOrder = SortOrder.Ascending) As IEnumerable(Of TEntity) Implements IRepository.Get
        If order = SortOrder.Ascending Then
            Return GetQuery(Of TEntity)(criteria).OrderBy(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).AsEnumerable()
        End If
        Return GetQuery(Of TEntity)(criteria).OrderByDescending(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).AsEnumerable()
    End Function

    Public Function [Single](Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As TEntity Implements IRepository.Single
        Return GetQuery(Of TEntity)().Single(criteria)
    End Function

    Public Function First(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As TEntity Implements IRepository.First
        Return GetQuery(Of TEntity)().First(predicate)
    End Function

    Public Sub Add(Of TEntity As Class)(entity As TEntity) Implements IRepository.Add
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If
        DbContext.Set(Of TEntity)().Add(entity)
    End Sub

    Public Sub Attach(Of TEntity As Class)(entity As TEntity) Implements IRepository.Attach
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If

        DbContext.Set(Of TEntity)().Attach(entity)
    End Sub

    Public Sub Delete(Of TEntity As Class)(entity As TEntity) Implements IRepository.Delete
        If entity Is Nothing Then
            Throw New ArgumentNullException("entity")
        End If
        DbContext.Set(Of TEntity)().Remove(entity)
    End Sub

    Public Sub Delete(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) Implements IRepository.Delete
        Dim records As IEnumerable(Of TEntity) = Find(Of TEntity)(criteria)

        For Each record As TEntity In records
            Delete(Of TEntity)(record)
        Next
    End Sub

    Public Function GetAll(Of TEntity As Class)() As IEnumerable(Of TEntity) Implements IRepository.GetAll
        Return GetQuery(Of TEntity)().AsEnumerable()
    End Function

    Public Function Save(Of TEntity As Class)(entity As TEntity) As TEntity
        Add(Of TEntity)(entity)
        DbContext.SaveChanges()
        Return entity
    End Function

    Public Sub Update(Of TEntity As Class)(entity As TEntity) Implements IRepository.Update
        Dim fqen = GetEntityName(Of TEntity)()

        Dim originalItem As Object = Nothing
        Dim key As EntityKey = DirectCast(DbContext, IObjectContextAdapter).ObjectContext.CreateEntityKey(fqen, entity)
        If DirectCast(DbContext, IObjectContextAdapter).ObjectContext.TryGetObjectByKey(key, originalItem) Then
            DirectCast(DbContext, IObjectContextAdapter).ObjectContext.ApplyCurrentValues(key.EntitySetName, entity)
        End If
    End Sub

    Public Function Find(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As IEnumerable(Of TEntity) Implements IRepository.Find
        Return GetQuery(Of TEntity)().Where(criteria)
    End Function

    Public Function FindOne(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As TEntity Implements IRepository.FindOne
        Return GetQuery(Of TEntity)().Where(criteria).FirstOrDefault()
    End Function

    Public Function Count(Of TEntity As Class)() As Integer Implements IRepository.Count
        Return GetQuery(Of TEntity)().Count()
    End Function

    Public Function Count(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Integer Implements IRepository.Count
        Return GetQuery(Of TEntity)().Count(criteria)
    End Function

    Public ReadOnly Property UnitOfWork() As Infrastructure.IUnitOfWork Implements IRepository.UnitOfWork
        Get
            If m_unitOfWork Is Nothing Then
                m_unitOfWork = New Infrastructure.UnitOfWork(Me.DbContext)
            End If
            Return m_unitOfWork
        End Get
    End Property

    Private Function GetEntityKey(Of TEntity As Class)(keyValue As Object) As EntityKey
        Dim entitySetName = GetEntityName(Of TEntity)()
        Dim objectSet = DirectCast(DbContext, IObjectContextAdapter).ObjectContext.CreateObjectSet(Of TEntity)()
        Dim keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers(0).ToString()
        Dim entityKey = New EntityKey(entitySetName, {New EntityKeyMember(keyPropertyName, keyValue)})
        Return entityKey
    End Function

    Private Function GetEntityName(Of TEntity As Class)() As String
        ' PluralizationService pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));
        ' return string.Format("{0}.{1}", ((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, pluralizer.Pluralize(typeof(TEntity).Name));
        ' Thanks to Kamyar Paykhan -  http://huyrua.wordpress.com/2011/04/13/entity-framework-4-poco-repository-and-specification-pattern-upgraded-to-ef-4-1/#comment-688
        Dim entitySetName As String = DirectCast(DbContext, IObjectContextAdapter).ObjectContext.MetadataWorkspace.GetEntityContainer(DirectCast(DbContext, IObjectContextAdapter).ObjectContext.DefaultContainerName, DataSpace.CSpace).BaseEntitySets.Where(Function(bes) bes.ElementType.Name = GetType(TEntity).Name).First().Name
        Return String.Format("{0}.{1}", DirectCast(DbContext, IObjectContextAdapter).ObjectContext.DefaultContainerName, entitySetName)
    End Function

    Private ReadOnly Property DbContext() As DbContext
        Get
            If Me._context Is Nothing Then
                If Me._connectionStringName = String.Empty Then
                    Me._context = Infrastructure.DbContextManager.Current
                Else
                    Me._context = Infrastructure.DbContextManager.CurrentFor(Me._connectionStringName)
                End If
            End If
            Return Me._context
        End Get
    End Property

    Private m_unitOfWork As Infrastructure.IUnitOfWork
End Class
