$(function () {
    $("#CityId").change(function () {
        let district = $("#DistrictId");
        district.empty();
        if ($(this).val() != "") {
            $.get("/account/FillDistricts", { id: $(this).val() }, function (response) {
                response.unshift({ id: '', text: '' });
                district.select2({ data: response }).trigger("change");
            });
        }
    });
});
function SignUp() {
    $('#SignUpForm').submit();
}

var AfterSave = function (data) {
    var strStatus = data.responseJSON.status;
    if (strStatus == "success") {
        ShowNotification('success', DataSavedSuccessfully);
        window.location.href = "/Home/Index";
    }
    else { ShowNotification('error', Erroroccuredtryagain); }
}
