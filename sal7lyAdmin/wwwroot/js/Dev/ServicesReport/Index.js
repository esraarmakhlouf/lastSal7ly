function reportSearch() {

    var $form = $("#SearchForm");
    var data = ConvertFormToJson($form);
    $.post("/ServicesReport/Search", { model: data }, function (response) {
        $("#listDiv").html(response);
    });
}

function AssignResponsible(id) {
    $.get("/ServicesReport/AssignResponsible/", { id: id }, function (response) {
        ShowModalForm("lg", Order, response);
    });
}

function CompleteOrder(id) {

    $.post("/ServicesReport/CompleteOrder", { id: id }, function (response) {
        var strStatus = response.status;
        if (strStatus == "success") {
            ShowNotification('success', DataSavedSuccessfully);
            $("#SearchForm").closest('.grid-container').find('#searchButton').trigger('click');
        }
        else {
            ShowNotification('error', Erroroccuredtryagain);
        }
    });
}