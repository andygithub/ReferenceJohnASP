Namespace Cache
    ''' <summary>
    ''' Class that will contain settings about a command.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CacheCommandDefinition
        ''' <summary>
        ''' Default constructor that sets the properties to default values.
        ''' </summary>
        ''' <remarks></remarks>
        Sub New()
            CacheMethodName = New List(Of String)
            CacheResetMethodName = New List(Of String)
            SlidingExpiration = TimeSpan.Zero
            AbsoluteExpiration = DateTime.MaxValue
            MinCacheableRows = 0
            MaxCacheableRows = 1000
        End Sub

        ''' <summary>
        ''' Logical entity name for the command.  This should be unique and it will be reused in the dependent entites list.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property EntityName As String

        ''' <summary>
        ''' The list of full qualied method names that should be cached and are part of the same logical entity name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CacheMethodName As IEnumerable(Of String)

        ''' <summary>
        ''' The list of full qualied method names that should cause the cache to be cleared and are part of the same logical entity name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property CacheResetMethodName As IEnumerable(Of String)

        ''' <summary>
        ''' The list of dependent entities on this logical entity.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property DependentEntities As IEnumerable(Of String)

        ''' <summary>
        ''' The sliding expiration for items cached in this logical entity name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property SlidingExpiration As TimeSpan

        ''' <summary>
        ''' The absolute expiration for items cached in this logical entity name.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property AbsoluteExpiration As Date

        ''' <summary>
        ''' Gets or sets the minimal number of cacheable rows.
        ''' </summary>
        ''' <value>Minimal number of cacheable rows.</value>
        Public Property MinCacheableRows() As Integer

        ''' <summary>
        ''' Gets or sets the maximum number of cacheable rows.
        ''' </summary>
        ''' <value>Maximum number of cacheable rows.</value>
        Public Property MaxCacheableRows() As Integer

    End Class

End Namespace