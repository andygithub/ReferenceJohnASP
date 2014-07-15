<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Reference John Asp.Net MVC", "Index", "Home", New With {.area = ""}, New With {.class = "navbar-brand"})
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("Create", "Create", "Form")</li>
                    <li>@Html.ActionLink("Edit", "Edit", "Form", New With {.ClientToken = "85DEB7BC-100A-E411-BE7D-C81F6607333C"}, Nothing)</li>
                    <li>@Html.ActionLink("List", "Index", "Form")</li>
                    <li>@Html.ActionLink("Throw Error", "ThrowError", "Form")</li>
                </ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>

    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")
    @Scripts.Render("~/bundles/jqueryval")
    <script>
    //$.ajaxSetup({
    //error: function(XMLHttpRequest, textStatus, errorThrown) {
    //alert('global ajax error handler - this should never get hit');
    //}
        //});
    $(document).ajaxError(function (e, jqxhr, settings, exception) {
        e.stopPropagation();
        if (jqxhr != null)
            alert(jqxhr.responseText + '-global message');
    });
    </script>    
    @RenderSection("scripts", required:=False)
</body>
</html>
