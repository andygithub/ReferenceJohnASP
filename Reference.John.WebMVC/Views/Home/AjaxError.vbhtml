@Code
    ViewData("Title") = "Ajax Error"
End Code

<div class="row">
    <div class="col-md-4">
        <h2>ajax post back with simple div update of value</h2>
        <p>
            <div id="updatesection"></div>
            @Using (Ajax.BeginForm("ShowAjaxPost", New AjaxOptions With {.HttpMethod = "POST", .UpdateTargetId = "updatesection"}))
                @<input id="id" name="id" value="Ajax " />
                @<input type="submit" class="btn btn-default" value="Ajax Submit" />
            End Using
        </p>
    </div>
    <div class="col-md-4">
        <h2>ajax post back exception</h2>
        <p>
            <div id="updatesection"></div>
            @Using (Ajax.BeginForm("ShowError", New AjaxOptions With {.HttpMethod = "POST", .UpdateTargetId = "updatesection"}))
                @<input id="id" name="id" value="Ajax asdas" />
                @<input type="submit" class="btn btn-default" value="Ajax Submit" />
            End Using
        </p>
    </div>
    <div class="col-md-4">
        <h2>ajax post back exception json result</h2>
        <p>
            <div id="updatesection"></div>
            @Using (Ajax.BeginForm("ShowErrorJSON", New AjaxOptions With {.HttpMethod = "POST", .UpdateTargetId = "updatesection"}))
                @<input id="id" name="id" value="Ajax 111" />
                @<input type="submit" class="btn btn-default" value="Ajax Submit" />
            End Using
        </p>
    </div>
</div>
@section scripts
    <script>
    $.ajax({
        url: '/Home/ShowErrorJson',
        success: function (result) {
            alert('yeap');
        },
        //expect that any server excepitons would be wrapped and sent back as a messasge to display.  If an unhandled exception is thrown then the setup in layout would be hit.
        //error: function (XMLHttpRequest, textStatus, errorThrown) {
        //    alert('oops, something bad happened');
        //}
    });

    $.ajax({
        url: '/Home/ShowError',
        success: function (result) {
            alert('yeap');
        },
    });

    $.ajax({
        url: '/Home/ShowInvalidJSON',
        success: function (result) {

            alert('yeap /Home/ShowInvalidJSON ' + result.error);

        },
    });

    </script>
end section