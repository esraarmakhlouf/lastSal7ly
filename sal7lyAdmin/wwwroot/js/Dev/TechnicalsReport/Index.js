
function reportSearch() {

    var $form = $("#SearchForm");
    var data = ConvertFormToJson($form);
    $.post("/TechnicalsReport/Search", { model: data }, function (response) {
        $("#listDiv").html(response);
    });
}

function reportOrdersSearch() {

    var $form = $("#SearchForm");
    var data = ConvertFormToJson($form);
    $.post("/TechnicalsReport/SearchOrders", { model: data }, function (response) {
        $("#techOrdersListDiv").html(response);
    });
}