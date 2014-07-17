@ModelType Models.ViewFormSimpleZero
@Code
    ViewData("Title") = "Data Entry: Create Item "
End Code
<div class="page-header">
    <h1>@ViewData("Title")</h1>
</div>
<div class="row-fluid">
    <div class="span9">
        <div class="well">
            @Using Html.BeginForm("Create", "Form", FormMethod.Post, New With {.class = "form", .role = "form"})
                @Html.AntiForgeryToken()
                @Html.ValidationSummary(True)

                @<fieldset>
                    <legend>Form Zero</legend>
                    <div class="form-group">
                        @Html.LabelFor(Function(model) model.FirstName, New With {.class = ""})
                        @Html.TextBoxFor(Function(model) model.FirstName, New With {.class = "form-control", .placeholder = Extensions.HtmlHelperExtensions.GetDisplayName(Model,Function(x) x.FirstName)})
                        @Html.ValidationMessageFor(Function(model) model.FirstName)
                    </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.LastName, New With {.class = ""})
                         @Html.TextBoxFor(Function(model) model.LastName, New With {.class = "form-control", .placeholder = Extensions.HtmlHelperExtensions.GetDisplayName(Model, Function(x) x.LastName)})
                         @Html.ValidationMessageFor(Function(model) model.LastName)
                     </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.GenderId)
                        @Html.DropDownListFor(Function(model) model.GenderId, Model.GenderList, "", New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.GenderId)
                    </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.RaceId)
                        @Html.DropDownListFor(Function(model) model.RaceId, Model.RaceList, "", New With {.class = "form-control"})
                        @Html.ValidationMessageFor(Function(model) model.RaceId)
                    </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.RegionId)
                         @Html.DropDownListFor(Function(model) model.RegionId, Model.RegionList, "", New With {.class = "form-control"})
                         @Html.ValidationMessageFor(Function(model) model.RegionId)
                     </div>
                     <div class="form-group">
                         @Html.LabelFor(Function(model) model.EthnicityId)
                         @Html.DropDownListFor(Function(model) model.EthnicityId, Model.EthnicityList, "", New With {.class = "form-control"})
                         @Html.ValidationMessageFor(Function(model) model.EthnicityId)
                     </div>
                    <p />
                    <div class="form-actions">
                        <input type="submit" value="Create" class="btn btn-primary" />
                        @Html.ActionLink("Back to List", "Index", Nothing, New With {.class = "btn pull-right"})
                    </div>
                </fieldset>
            End Using
        </div>
    </div>
</div>
