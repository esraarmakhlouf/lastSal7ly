
function Save(e) {
    
    let $form = $("#AssignResponsibleForm");
    if ($form.valid()) {
        debugger
        $(e).attr("disabled", true);
        
        var data = ConvertFormToJson($form);
        $.post("/ServicesReport/SaveAssignResponsible", { orderId: data["Id"], responsibleUserId:data["ResponsibleUserId"] }, function (response) {
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