$(function () {
    $("#ApplyThroughCoupon").change(function () {
        if ($(this).prop("checked")) {
            $("#haveCouponCode").show();
        }
        else {
            $("#haveCouponCode").hide();
        }
    });


    if ($("#ImagePath").val() != "") {
        $('.image-upload-wrap').hide();
        $('.file-upload-content').show();
    }
    else {
        $('.image-upload-wrap').show();
        $('.file-upload-content').hide();
    }
});
$(function () {
    $(".select2").select2({ width: null });
    $('.StartDate , .EndDate').bootstrapMaterialDatePicker({
            // clearButton: true,
            format: 'DD/MM/YYYY HH:mm',
            minDate: new Date()
        });
   
    $(document).on("change", "#type-select", function () {
        let selectedValue = $(this).val();
        
        let mainCatId = $("#MainCategoryId");
        mainCatId.closest(".col").hide();
        mainCatId.attr("data-val", "false");
        let mainItemId = $("#MainItemId");
        mainItemId.attr("data-val", "false");

        let itemMainDiv = $(".itemMainDiv");
        itemMainDiv.hide();
        itemMainDiv.find(".form.control").val('').trigger("change");
        let freeCatId = $("#FreeCategoryId");
        freeCatId.closest(".col").hide();
        freeCatId.attr("data-val", "false");
        let freeItemId = $("#FreeItemId");
        freeItemId.attr("data-val", "false");

        switch (selectedValue) {
            case "0":
                itemMainDiv.hide();
            case "1":
                itemMainDiv.show();
                break;
            default:
                break;
        }

        $("#OffersForm").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("#OffersForm");
    });

    $("#ApplyThroughCoupon").change(function () {
        let code = $("#CouponCode");
        let parent = code.closest(".col");
        parent.hide();
        code.val('');
        code.attr("data-val", "false");
        if ($(this).prop("checked")) {
            parent.show();
            code.attr("data-val", "true");
        }
        $("#DiscountsForm").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("#DiscountsForm");
    });
    $("#ApplyThroughCoupon").each(function () {
        let code = $("#CouponCode");
        let parent = code.closest(".col");
        parent.hide();
        code.attr("data-val", "false");
        if ($(this).prop("checked")) {
            parent.show();
            code.attr("data-val", "true");
        }
        $("#DiscountsForm").removeData("validator").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse("#DiscountsForm");
    });
    $("#MainCategoryId ").change(function () {
        debugger
        let value = $(this).val();
        $("#FreeCategoryId option[value='" + value + "']").remove();
    });
    //$("#MainItemId ").change(function () {
    //    debugger
    //    let value = $(this).val();
    //    $("#FreeItemId option[value='" + value + "']").remove();
    //});


});
function Save() {
    var $form = $("#OffersForm");
    if (ValidateActiveDates() && ValidateImage()) {
        var data = ConvertFormToJson($form);
        data["IsActive"] = $("#IsActive").prop("checked");
        data["ApplyThroughCoupon"] = $("#ApplyThroughCoupon").prop("checked");
        $.post("/Offers/Save", { entity: data, activeDates: jsonActiveDates }, function (response) {
            var strStatus = response.status;
            if (strStatus == "success") {
                SaveOfferImage(response.id);
            }
            else {
                
                ShowNotification('error', 'ادخل البيانات المطلوبة');
            }
        });
    }
}
// Image
function SaveOfferImage(id) {
    var formData = new FormData();
    var fileInput = $('#Image')[0];
    formData.append("id", id);
    //Iterating through each files selected in fileInput
    for (i = 0; i < fileInput.files.length; i++) {
        //Appending each file to FormData object
        formData.append(fileInput.files[i].name, fileInput.files[i]);
    }
    //Creating an XMLHttpRequest and sending
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Offers/SaveOfferImage');
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            ShowNotification('success', DataSavedSuccessfully);
            window.location.href = "/Offers/Index";
        }
    }
}
function DeleteOfferImage(id) {
    $("#ImagePath").val('');
    $("#Image").val('');
    var id = $("#Id").val();
    if (id != null && id != "" && id != "0") {
        $.get("/Offers/DeleteOfferImage", { id: id }, function (response) {
            ShowNotification('success', DataSavedSuccessfully);
            removeUpload();
        });
    }
    else { removeUpload(); }
}
function ValidateImage() {
    let isValid = true;
    let span = $("span.img-reuired");
    span.hide();
    if ($("#ShowInSlider").prop("checked")) {
        if ($("#ImagePath").val() == "" && $('#Image').val() == "") {
            span.show();
            isValid = false;
        }
    }
    return isValid;
}
//ActiveDates
$(document).on("change", ".ActiveDates .form-control", function () {
    let $row = $(this).closest("tr");
    let $index = $row.index();
    jsonActiveDates[$index].strStartDate = $row.find(".StartDate").val();
    jsonActiveDates[$index].strEndDate = $row.find(".EndDate").val();
    ValidateActiveDates();
});
function AddActiveDate() {
    let $tbody = $(".ActiveDates");
    $tbody.append(`
             <tr>
                <td data-title="StartDate">
                    <input type="text" name="StartDate" class="form-control StartDate" value="" placeholder="تاريخ البداية" />
                </td>
                <td data-title="EndDate">
                    <input type="text" name="EndDate" class="form-control EndDate" value="" placeholder="تاريخ النهاية" />
                </td>
                <td class="button">
                        <button type="button" class="btn btn-sp btn-outline-danger" onclick="DeleteActiveDate(this)">
                            <i class="far fa-trash-alt"></i>
                        </button>
                </td>
            </tr>
                    `);
    jsonActiveDates.push({ id: null, strStartDate: "", strEndDate: "" });
    $('.StartDate , .EndDate').bootstrapMaterialDatePicker({
            // clearButton: true,
            format: 'DD/MM/YYYY HH:mm',
            minDate: new Date()
    });
    ValidateActiveDates();
}
function DeleteActiveDate($this) {
    let $row = $($this).closest("tr");
    let $index = $row.index();
    $row.remove();
    jsonActiveDates.splice($index, 1);
}
function ValidateActiveDates() {
    let isValid = true;
    $(".ActiveDates tr").each(function () {
        let $row = $(this);
        $row.find("span.text-danger").remove();
        let startDate = $row.find(".StartDate");
        let startDateTD = startDate.closest("td");
        startDate.removeClass("is-invalid");
        if ($.trim(startDate.val()) == "") {
            startDate.addClass("is-invalid");
            startDateTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }

        let endDate = $row.find(".EndDate");
        let endDateTD = endDate.closest("td");
        endDate.removeClass("is-invalid");
        if ($.trim(endDate.val()) == "") {
            endDate.addClass("is-invalid");
            endDateTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }
    });
    return isValid;
}