
$(function () {
    $('.select2').select2();
    $('fieldset .select2').prop("disabled", true);
});

function OpenForm(id) {
    $.get("/SystemOptions/OpenForm/", { id: id }, function (response) {
        ShowModalForm("lg", SystemOptions, response);
    });
}
