@ModelType Reference.John.Domain.FormSimpleZero

@Code
    ViewData("Title") = "Data Entry: Delete Item "
End Code

         <div class="page-header">
            <h1>@ViewData("Title")</h1>
        </div>

<div class="row-fluid">
    <div class="span9">
        <div class="alert alert-warning">
            <button class="close" data-dismiss="alert">
                ×</button>
            <strong>Warning!</strong> Are you sure you want to delete this?
        </div>
        <div class="well">
            <dl>
                <dt>@Html.DisplayNameFor(Function(model) model.id)</dt>
                <dd>@Html.DisplayFor(Function(model) model.id)</dd>
                <dt>@Html.DisplayNameFor(Function(model) model.Firstname)</dt>
                <dd>@Html.DisplayFor(Function(model) model.firstname)</dd>
                <dt>@Html.DisplayNameFor(Function(model) model.Lastname)</dt>
                <dd>@Html.DisplayFor(Function(model) model.LastName)</dd>
                <dt>@Html.DisplayNameFor(Function(model) model.DateCreated)</dt>
                <dd>@Html.DisplayFor(Function(model) model.DateCreated)</dd>
            </dl>
            <div class="form-actions">
                @Using Html.BeginForm()
                    @Html.AntiForgeryToken()
                    @<p>
                        <input type="submit" value="Delete" class="btn btn-warning" />
                        @Html.ActionLink("Back to List", "Index", Nothing, New With {.class = "btn pull-right"})
                    </p>
                End Using
            </div>
        </div>
    </div>
</div>
