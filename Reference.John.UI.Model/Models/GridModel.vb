''' <summary>
''' Wrapper class for passing around a structure that can be used by grid type user interface controls.
''' </summary>
''' <typeparam name="T"></typeparam>
''' <remarks></remarks>
Public Class GridModel(Of T)
    ''' <summary>
    ''' This property gives a count of the total number of rows available from the data source.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TotalRows As Integer
    ''' <summary>
    ''' This property give the number of the current page in the data source.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentPage As Integer
    ''' <summary>
    ''' This property will contain this set of data to be displayed by the grid type user interface.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Data As IEnumerable(Of T)

End Class
