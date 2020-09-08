function AssignResponsible(id) {
    $.get("/Order/AssignResponsible/", { id: id }, function (response) {
        ShowModalForm("lg", Order, response);
    });
}

function reportSearch() {

    var $form = $("#SearchForm");
    var data = ConvertFormToJson($form);
    $.post("/Order/Search", { model: data }, function (response) {
        $("#listDiv").html(response);
    });
}

function CompleteOrder(id) {

    $.post("/Order/CompleteOrder", { id: id }, function (response) {
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