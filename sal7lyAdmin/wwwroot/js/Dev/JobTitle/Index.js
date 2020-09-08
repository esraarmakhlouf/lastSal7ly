function OpenForm(id) {
    $.get("/JobTitle/OpenForm/", { id: id }, function (response) {
        ShowModalForm("lg", JobTitle, response);
    });
}