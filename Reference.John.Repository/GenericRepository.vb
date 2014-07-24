Imports System.Collections.Generic
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

    Public Function GetQuery(Of TEntity As Class)(<Runtime.CompilerServices.CallerFilePath> Optional sourcefilePath As String = Nothing, <Runtime.CompilerServices.CallerLineNumber()> Optional sourceLineNumber As Integer = 0) As IQueryable(Of TEntity) Implements IRepository.GetQuery
        Debug.WriteLine(sourcefilePath)
        Debug.WriteLine(sourceLineNumber)
        'Dim _formattedMessage = MarkSqlString("select 1 from dual", sourcefilePath, sourceLineNumber, Nothing)
        'Debug.WriteLine(_formattedMessage)
        Dim i = GetType(TEntity)
        Return DbContext.Set(Of TEntity)()
    End Function

    Public Function GetQuery(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As IQueryable(Of TEntity) Implements IRepository.GetQuery
        Return GetQuery(Of TEntity)().Where(predicate)
    End Function

    Public Function [Get](Of TEntity As Class, TOrderBy)(orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional order As SortOrder = SortOrder.Ascending) As IEnumerable(Of TEntity) Implements IRepository.Get
        If order = SortOrder.Ascending Then
            Return GetQuery(Of TEntity)().OrderBy(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToList()
        End If
        Return GetQuery(Of TEntity)().OrderByDescending(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToList()
    End Function

    Public Async Function GetAsync(Of TEntity As Class, TOrderBy)(orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional order As SortOrder = SortOrder.Ascending) As Task(Of IEnumerable(Of TEntity)) Implements IRepository.GetAsync
        If order = SortOrder.Ascending Then
            Return Await GetQuery(Of TEntity)().OrderBy(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync()
        End If
        Return Await GetQuery(Of TEntity)().OrderByDescending(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync()
    End Function

    Public Function [Get](Of TEntity As Class, TOrderBy)(criteria As Expression(Of Func(Of TEntity, Boolean)), orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional order As SortOrder = SortOrder.Ascending) As IEnumerable(Of TEntity) Implements IRepository.Get
        If order = SortOrder.Ascending Then
            Return GetQuery(Of TEntity)(criteria).OrderBy(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToList()
        End If
        Return GetQuery(Of TEntity)(criteria).OrderByDescending(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToList()
    End Function

    Public Async Function GetAsync(Of TEntity As Class, TOrderBy)(criteria As Expression(Of Func(Of TEntity, Boolean)), orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional order As SortOrder = SortOrder.Ascending) As Task(Of IEnumerable(Of TEntity)) Implements IRepository.GetAsync
        If order = SortOrder.Ascending Then
            Return Await GetQuery(Of TEntity)(criteria).OrderBy(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync()
        End If
        Return Await GetQuery(Of TEntity)(criteria).OrderByDescending(orderBy).Skip((pageIndex) * pageSize).Take(pageSize).ToListAsync()
    End Function

    Public Function [Single](Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As TEntity Implements IRepository.Single
        Return GetQuery(Of TEntity)().Single(criteria)
    End Function

    Public Async Function SingleAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity) Implements IRepository.SingleAsync
        Return Await GetQuery(Of TEntity)().SingleAsync(criteria)
    End Function

    Public Function First(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As TEntity Implements IRepository.First
        Return GetQuery(Of TEntity)().First(predicate)
    End Function

    Public Async Function FirstAsync(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity) Implements IRepository.FirstAsync
        Return Await GetQuery(Of TEntity)().FirstAsync(predicate)
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
        'assume that when attached an entity it is modified,  this has been the case for currently tested scenarios
        DbContext.Entry(entity).State = EntityState.Modified
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
        Return GetQuery(Of TEntity)().ToList
    End Function

    Public Async Function GetAllAsync(Of TEntity As Class)() As Task(Of IEnumerable(Of TEntity)) Implements IRepository.GetAllAsync
        Return Await GetQuery(Of TEntity)().ToListAsync()
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
        Dim _item = DbContext.Entry(entity)
        Dim _val = _item.State
        Dim _val2 = _item.Property("RowVersion")
        'Dim _item = DirectCast(DbContext, IObjectContextAdapter).ObjectContext.ge
    End Sub

    Public Function Find(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As IEnumerable(Of TEntity) Implements IRepository.Find
        Return GetQuery(Of TEntity)().Where(criteria)
    End Function

    Public Async Function FindAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of IEnumerable(Of TEntity)) Implements IRepository.FindAsync
        Return Await GetQuery(Of TEntity)().Where(criteria).ToListAsync
    End Function

    Public Function FindOne(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As TEntity Implements IRepository.FindOne
        Return GetQuery(Of TEntity)().Where(criteria).FirstOrDefault()
    End Function

    Public Async Function FindOneAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity) Implements IRepository.FindOneAsync
        Return Await GetQuery(Of TEntity)().Where(criteria).FirstOrDefaultAsync()
    End Function

    Public Function Count(Of TEntity As Class)() As Integer Implements IRepository.Count
        Return GetQuery(Of TEntity)().Count()
    End Function

    Public Async Function CountAsync(Of TEntity As Class)() As Task(Of Integer) Implements IRepository.CountAsync
        Return Await GetQuery(Of TEntity)().CountAsync()
    End Function

    Public Function Count(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Integer Implements IRepository.Count
        Return GetQuery(Of TEntity)().Count(criteria)
    End Function

    Public Async Function CountAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of Integer) Implements IRepository.CountAsync
        Return Await GetQuery(Of TEntity)().CountAsync(criteria)
    End Function

    Public Function Contains(Of TEntity As Class)(value As TEntity) As Boolean Implements IRepository.Contains
        Return GetQuery(Of TEntity)().Contains(value)
    End Function

    Public Async Function ContainsAsync(Of TEntity As Class)(value As TEntity) As Task(Of Boolean) Implements IRepository.ContainsAsync
        Return Await GetQuery(Of TEntity)().ContainsAsync(value)
    End Function

    Public ReadOnly Property UnitOfWork() As Infrastructure.IUnitOfWork Implements IRepository.UnitOfWork
        Get
            If _unitOfWork Is Nothing Then
                _unitOfWork = New Infrastructure.UnitOfWork(Me.DbContext)
            End If
            Return _unitOfWork
        End Get
    End Property

    Public Sub ExecuteProcedure(procedureCommand As String, parameters() As SqlClient.SqlParameter) Implements IRepository.ExecuteProcedure
        _context.Database.ExecuteSqlCommand(procedureCommand, parameters)
    End Sub

    Public Async Sub ExecuteProcedureAsync(procedureCommand As String, parameters() As SqlClient.SqlParameter) Implements IRepository.ExecuteProcedureAsync
        Await _context.Database.ExecuteSqlCommandAsync(procedureCommand, parameters)
    End Sub

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

    Private _unitOfWork As Infrastructure.IUnitOfWork


    Private Shared Function MarkSqlString(sql As String, path As String, lineNumber As Integer, comment As String) As String
        If String.IsNullOrEmpty(path) OrElse lineNumber = 0 Then Return sql

        Dim commentWrap = " "
        Dim i = sql.IndexOf(Environment.NewLine)

        ' if we didn't find \n, or it was the very end, go to the first space method
        If i < 0 OrElse i = sql.Length - 1 Then
            i = sql.IndexOf(" "c)
            commentWrap = Environment.NewLine
        End If

        If i < 0 Then Return sql

        ' Grab one directory and the file name worth of the path
        '   this dodges problems with the build server using temp dirs
        '   but also gives us enough info to uniquely identify a queries location
        Dim split = path.LastIndexOf("\"c) - 1
        If split < 0 Then Return sql

        split = path.LastIndexOf("\"c, split)

        If split < 0 Then Return sql

        split += 1
        ' just for Craver
        Dim sqlComment = " /* " & path.Substring(split) & "@" & lineNumber & (If(Not String.IsNullOrWhiteSpace(comment), Convert.ToString(" - ") & comment, "")) & " */" & commentWrap

        Return sqlComment & sql 'sql.Substring(0, i) + sqlComment + sql.Substring(i)
    End Function

End Class
