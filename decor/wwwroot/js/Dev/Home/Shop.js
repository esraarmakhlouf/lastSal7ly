$(function () {
    //$("[name='category_id']").hide();
    $("#search .btn-search").attr("onclick", "ShopSearch()");
});
$(document).on("click", ".unselectedItem", function () {
    if ($(this).hasClass("SelectItem")) { $(this).removeClass("SelectItem"); }
    else { $(this).addClass("SelectItem"); }
});

function ShopSearch(page) {
    debugger;
    let currentPage = page||$(".page.active").text();
    let pageLength = $(".pagelength").val();
    let sortBy = $(".sortby").val();
    let searchTerm = ($("#search").find("input").val() || "").toLowerCase()||"";
    let cat = [];
    cat.push($("#category_id").val());

    let searchModel = {
        CurrentPage: currentPage,
        PageLength: pageLength,
        SortBy: sortBy,
        SearchTerm: searchTerm,
        SelectedCategories: cat,
        SelectedSubCategories: cat
    };
    $.post("/Home/ShopSearch", searchModel, function (response) {
        debugger;
        $("#ShopProducts").html(response);
        $(".view-mode li.active a").trigger("click");
    });
}


