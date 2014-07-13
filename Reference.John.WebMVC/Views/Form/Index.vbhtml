@Code
    ViewData("Title") = "Data Entry: Form Zero "
End Code
<div class="page-header">
    <h1>@ViewData("Title")</h1>
</div>
<div class="row-fluid">
    <div class="span6">
        <a class="btn" href="@Url.Action("Create")">Create New</a> <a class="btn btn-inverse" href="@Url.Action("Download")">
            <i class="icon-download icon-white"></i>Download</a>
        <p />
    </div>
</div>
<div class="row-fluid">
    <div class="span12">
        <div id="formzeroGrid">
            @Html.Action(Reference.John.Resources.MVCConstants.GridAction)
        </div>
    </div>
</div>
<script type="text/javascript">

    $(document).ready(function () {
        $(document).on("click", '#formzeroGrid a[href*="sort="]', function (event) {
            event.preventDefault();
            var href = $(this).attr("href");
            var queryString = href.substring(href.indexOf('?'), href.length);
            var requestUrl = '@Url.Action(Reference.John.Resources.MVCConstants.GridAction)' + queryString;
            $("#formzeroGrid").load(requestUrl);
        });
    });
</script>