@ModelType Reference.John.Domain.FormSimpleZero
@Code
    ViewData("Title") = "Data Entry: Create Item "
End Code
<div class="page-header">
    <h1>@ViewData("Title")</h1>
</div>
<div class="row-fluid">
    <div class="span9">
        <div class="well">
            @Using Html.BeginForm()
                @Html.AntiForgeryToken()
                @Html.ValidationSummary(True)

                @<fieldset>
                    <legend>Form Zero</legend>
                    <div class="editor-label">
                        @Html.LabelFor(Function(model) model.FirstName)
                    </div>
                    <div class="editor-field">
                        @Html.EditorFor(Function(model) model.FirstName)
                        @Html.ValidationMessageFor(Function(model) model.FirstName)
                    </div>
                    <div class="editor-label">
                        @Html.LabelFor(Function(model) model.LastName)
                    </div>
                    <div class="editor-field">
                        @Html.EditorFor(Function(model) model.LastName)
                        @Html.ValidationMessageFor(Function(model) model.LastName)
                    </div>
                    <div class="editor-label">
                        @Html.LabelFor(Function(model) model.GenderId)
                    </div>
                    <div class="editor-field">
                        @Html.DropDownListFor(Function(model) model.GenderId, ViewData(Reference.John.Resources.MVCConstants.GenderList))
                        @Html.ValidationMessageFor(Function(model) model.GenderId)
                    </div>
                    <div class="editor-label">
                        @Html.LabelFor(Function(model) model.RaceId)
                    </div>
                    <div class="editor-field">
                        @Html.DropDownListFor(Function(model) model.RaceId, ViewData(Reference.John.Resources.MVCConstants.RaceList))
                        @Html.ValidationMessageFor(Function(model) model.RaceId)
                    </div>
                     <div class="editor-label">
                         @Html.LabelFor(Function(model) model.RegionId)
                     </div>
                     <div class="editor-field">
                         @Html.DropDownListFor(Function(model) model.RegionId, ViewData(Reference.John.Resources.MVCConstants.RegionList))
                         @Html.ValidationMessageFor(Function(model) model.RegionId)
                     </div>
                     <div class="editor-label">
                         @Html.LabelFor(Function(model) model.EthnicityId)
                     </div>
                     <div class="editor-field">
                         @Html.DropDownListFor(Function(model) model.EthnicityId, ViewData(Reference.John.Resources.MVCConstants.EthnicityList))
                         @Html.ValidationMessageFor(Function(model) model.EthnicityId)
                     </div>
            <p/>
                    <div class="form-actions">
                        <input type="submit" value="Create" class="btn btn-primary" />
                        @Html.ActionLink("Back to List", "Index", Nothing, New With {.class = "btn pull-right"})
                    </div>
                </fieldset>
            End Using
        </div>
    </div>
</div>
