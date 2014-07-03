Imports System.Data.Entity

Namespace Infrastructure

    Public Interface IDbContextBuilder(Of T As DbContext)
        Function BuildDbContext(Optional modelType As Type = Nothing) As T
    End Interface

End Namespace
