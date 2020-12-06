
$(document).ready(function () {
    $("#searchInput").autocomplete({
        source: '@Url.Action("GetSearchValue", "Home")',
        minLength: 2
    })
    console.log("ready!");
});