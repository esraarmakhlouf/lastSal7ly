function Save(e) {
    let $form = $("#OrderTrackActionForm");
    if ($form.valid()) {
        $(e).attr("disabled", true);
        var data = ConvertFormToJson($form);
        data["IsActive"] = $("#IsActive").prop("checked");
        $.post("/OrderTrackAction/Save", { entity: data }, function (response) {
            var strStatus = response.status;
            if (strStatus == "success") {
                ShowNotification('success', DataSavedSuccessfully);
                $("#SearchForm").closest('.grid-container').find('#searchButton').trigger('click');
                var ModalForm = $("#ModalForm");
                ModalForm.modal('hide');
            }
            else {
                ShowNotification('error', Erroroccuredtryagain);
                $(e).attr("disabled", false);
            }
        });
    }
}