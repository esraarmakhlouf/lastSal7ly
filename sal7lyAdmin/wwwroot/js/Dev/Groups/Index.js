function OpenForm(id) {
    $.get("/Groups/OpenForm/", { id: id }, function (response) {
        ShowModalForm("lg", Groups, response);
    });
}