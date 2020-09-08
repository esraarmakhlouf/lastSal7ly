function OpenForm(id) {
    $.get("/Items/OpenForm/", { id: id }, function (response) {
        ShowModalForm("lg", Items, response);
    });
}
function ShowDuplicateModal(id) {
    
    var DuplicateModal = $("#DuplicateModal");
    var Duplicatebody = DuplicateModal.find('.modal-body');
    Duplicatebody.find("[name='itemId']").val(id);
    Duplicatebody.find("[name='ItemName']").val('');
    DuplicateModal.modal("show");
}
function SaveDuplicateItem($this) {
    var DuplicateModal = $("#DuplicateModal");
    var $form = $("#DuplicateForm");
    if ($form.valid()) {
        var Duplicatebody = DuplicateModal.find('.modal-body');
        let id = Duplicatebody.find("[name='itemId']").val();
        let name = Duplicatebody.find("[name='ItemName']").val();
            $.post("/Items/SaveDuplicateItem", { itemId: id, itemName: name }, function (data) {
                if (data.status === "success") {
                    $('.grid-container').find('#searchButton').trigger('click');
                    ShowNotification('success', DataSavedSuccessfully);
                $("#DuplicateModal").modal("hide");
                }
                else { ShowNotification('error', Erroroccuredtryagain); }
            });
    }

}
$(function () {
    $(".select2").select2({ width: null });
    $("#CategoryId").change(function () {
        let subCats = $("#SubCategoryId");
        subCats.empty();
        if ($(this).val() != "") {
            $.get("/items/FillSubCategories", { id: $(this).val() }, function (response) {
                response.unshift({ id: '', text: '' });
                subCats.select2({ data: response }).trigger("change");
            });
        }
    });
});
