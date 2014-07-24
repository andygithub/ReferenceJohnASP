Imports System.Linq.Expressions

Public Enum SortOrder
    Ascending
    Descending
End Enum

Public Interface IRepository
    ''' <summary>
    ''' Gets entity by key.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="keyValue">The key value.</param>
    ''' <returns></returns>
    Function GetByKey(Of TEntity As Class)(keyValue As Object) As TEntity

    ''' <summary>
    ''' Gets the query.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <returns></returns>
    Function GetQuery(Of TEntity As Class)(<Runtime.CompilerServices.CallerFilePath> Optional sourcefilePath As String = Nothing, <Runtime.CompilerServices.CallerLineNumber()> Optional sourceLineNumber As Integer = 0) As IQueryable(Of TEntity)

    ''' <summary>
    ''' Gets the query.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="predicate">The predicate.</param>
    ''' <returns></returns>
    Function GetQuery(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As IQueryable(Of TEntity)


    ''' <summary>
    ''' Gets one entity based on matching criteria
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function [Single](Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As TEntity

    ''' <summary>
    ''' Gets one entity based on matching criteria
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function SingleAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity)

    ''' <summary>
    ''' Firsts the specified predicate.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="predicate">The predicate.</param>
    ''' <returns></returns>
    Function First(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As TEntity

    ''' <summary>
    ''' Firsts the specified predicate.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="predicate">The predicate.</param>
    ''' <returns></returns>
    Function FirstAsync(Of TEntity As Class)(predicate As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity)

    ''' <summary>
    ''' Adds the specified entity.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="entity">The entity.</param>
    Sub Add(Of TEntity As Class)(entity As TEntity)

    ''' <summary>
    ''' Attaches the specified entity.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="entity">The entity.</param>
    Sub Attach(Of TEntity As Class)(entity As TEntity)

    ''' <summary>
    ''' Deletes the specified entity.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="entity">The entity.</param>
    Sub Delete(Of TEntity As Class)(entity As TEntity)

    ''' <summary>
    ''' Deletes one or many entities matching the specified criteria
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    Sub Delete(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean)))

    ''' <summary>
    ''' Updates changes of the existing entity. 
    ''' The caller must later call SaveChanges() on the repository explicitly to save the entity to database
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="entity">The entity.</param>
    Sub Update(Of TEntity As Class)(entity As TEntity)

    ''' <summary>
    ''' Finds entities based on provided criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function Find(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Finds entities based on provided criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function FindAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of IEnumerable(Of TEntity))

    ''' <summary>
    ''' Finds one entity based on provided criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function FindOne(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As TEntity

    ''' <summary>
    ''' Finds one entity based on provided criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function FindOneAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of TEntity)

    ''' <summary>
    ''' Gets all.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <returns></returns>
    Function GetAll(Of TEntity As Class)() As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Gets all.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <returns></returns>
    Function GetAllAsync(Of TEntity As Class)() As Task(Of IEnumerable(Of TEntity))

    ''' <summary>
    ''' Gets the specified order by.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <typeparam name="TOrderBy">The type of the order by.</typeparam>
    ''' <param name="orderBy">The order by.</param>
    ''' <param name="pageIndex">Index of the page.</param>
    ''' <param name="pageSize">Size of the page.</param>
    ''' <param name="sortOrder">The sort order.</param>
    ''' <returns></returns>
    Function [Get](Of TEntity As Class, TOrderBy)(orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional sortOrder As SortOrder = SortOrder.Ascending) As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Gets the specified order by.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <typeparam name="TOrderBy">The type of the order by.</typeparam>
    ''' <param name="orderBy">The order by.</param>
    ''' <param name="pageIndex">Index of the page.</param>
    ''' <param name="pageSize">Size of the page.</param>
    ''' <param name="sortOrder">The sort order.</param>
    ''' <returns></returns>
    Function GetAsync(Of TEntity As Class, TOrderBy)(orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional sortOrder As SortOrder = SortOrder.Ascending) As Task(Of IEnumerable(Of TEntity))

    ''' <summary>
    ''' Gets the specified criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <typeparam name="TOrderBy">The type of the order by.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <param name="orderBy">The order by.</param>
    ''' <param name="pageIndex">Index of the page.</param>
    ''' <param name="pageSize">Size of the page.</param>
    ''' <param name="sortOrder">The sort order.</param>
    ''' <returns></returns>
    Function [Get](Of TEntity As Class, TOrderBy)(criteria As Expression(Of Func(Of TEntity, Boolean)), orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional sortOrder As SortOrder = SortOrder.Ascending) As IEnumerable(Of TEntity)

    ''' <summary>
    ''' Gets the specified criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <typeparam name="TOrderBy">The type of the order by.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <param name="orderBy">The order by.</param>
    ''' <param name="pageIndex">Index of the page.</param>
    ''' <param name="pageSize">Size of the page.</param>
    ''' <param name="sortOrder">The sort order.</param>
    ''' <returns></returns>
    Function GetAsync(Of TEntity As Class, TOrderBy)(criteria As Expression(Of Func(Of TEntity, Boolean)), orderBy As Expression(Of Func(Of TEntity, TOrderBy)), pageIndex As Integer, pageSize As Integer, Optional sortOrder As SortOrder = SortOrder.Ascending) As Task(Of IEnumerable(Of TEntity))

    ''' <summary>
    ''' Counts the specified entities.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <returns></returns>
    Function Count(Of TEntity As Class)() As Integer

    ''' <summary>
    ''' Counts the specified entities.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <returns></returns>
    Function CountAsync(Of TEntity As Class)() As Task(Of Integer)

    ''' <summary>
    ''' Counts entities with the specified criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function Count(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Integer

    ''' <summary>
    ''' Counts entities with the specified criteria.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="criteria">The criteria.</param>
    ''' <returns></returns>
    Function CountAsync(Of TEntity As Class)(criteria As Expression(Of Func(Of TEntity, Boolean))) As Task(Of Integer)

    ''' <summary>
    ''' Determines if an entity is present within the defined entity set.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="value">The value passed to be checked for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Contains(Of TEntity As Class)(value As TEntity) As Boolean

    ''' <summary>
    ''' Determines if an entity is present within the defined entity set.
    ''' </summary>
    ''' <typeparam name="TEntity">The type of the entity.</typeparam>
    ''' <param name="value">The value passed to be checked for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ContainsAsync(Of TEntity As Class)(value As TEntity) As Task(Of Boolean)

    ''' <summary>
    ''' Method to execute a passed procedure on the open entity framework context.
    ''' </summary>
    ''' <param name="procedureCommand"></param>
    ''' <param name="parameters"></param>
    ''' <remarks></remarks>
    Sub ExecuteProcedure(procedureCommand As String, parameters As SqlClient.SqlParameter())

    ''' <summary>
    ''' Method to execute a passed procedure async on the open entity framework context.
    ''' </summary>
    ''' <param name="procedureCommand"></param>
    ''' <param name="parameters"></param>
    ''' <remarks></remarks>
    Sub ExecuteProcedureAsync(procedureCommand As String, parameters As SqlClient.SqlParameter())

    ''' <summary>
    ''' Gets the unit of work.
    ''' </summary>
    ''' <value>The unit of work.</value>
    ReadOnly Property UnitOfWork() As Infrastructure.IUnitOfWork
End Interface
