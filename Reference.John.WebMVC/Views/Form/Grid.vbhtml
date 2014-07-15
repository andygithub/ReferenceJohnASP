@ModelType Models.GridModel(Of Models.SearchResult)
@Code
    Dim _grid As New WebGrid(rowsPerPage:=Reference.John.Resources.MVCConstants.PageSize)

    _grid.Bind(Model.Data, autoSortAndPage:=False, rowCount:=Model.TotalRows)
    _grid.PageIndex = Model.CurrentPage
    
    Dim _cols As New List(Of WebGridColumn)
    _cols.Add(New WebGridColumn() With {.ColumnName = "FormId", .Header = "Id", .CanSort = True})
    _cols.Add(New WebGridColumn() With {.ColumnName = "FirstName", .Header = "First Name", .CanSort = True})
    _cols.Add(New WebGridColumn() With {.ColumnName = "LastName", .Header = "Last Name", .CanSort = True})
    _cols.Add(New WebGridColumn() With {.ColumnName = "GenderName", .Header = "Gender", .CanSort = True})
    _cols.Add(New WebGridColumn() With {.ColumnName = "EthnicityName", .Header = "Ethnicity", .CanSort = True})
    _cols.Add(New WebGridColumn() With {.ColumnName = "DateCreated", .Header = "Date Created", .CanSort = True, .Format = (Function(item) item.DateCreated.ToShortDateString)})
    _cols.Add(New WebGridColumn() With {.ColumnName = "ClientToken", .Header = "", .Format = (Function(item)
                                                                                                      Return Html.ActionLink("Edit", "Edit", New With {.ClientToken = item.ClientToken}, New With {.class = "btn"})
                                                                                     End Function)})
    _cols.Add(New WebGridColumn() With {.ColumnName = "ClientToken", .Header = "", .Format = (Function(item)
                                                                                                      Return Html.ActionLink("Delete", "Delete", New With {.ClientToken = item.ClientToken}, New With {.class = "btn btn-warning"})
                                                                                              End Function)})
End code
@_grid.GetHtml(tableStyle:="table table-striped table-condensed", columns:=_cols)
