function Save(e) {
    debugger
    let $form = $("#CountryForm");
    //if ($form.valid() && ValidateCities()) {
    $(e).attr("disabled", true);
        let data = ConvertFormToJson($form);
        data["IsActive"] = $("#IsActive").prop("checked");
        $.post("/Country/Save", { entity: data, cities: jsonCities.filter(ent => ent.IsChanged || ent.IsDeleted)}, function (response) {
            if (response.status == "success") {
                ShowNotification('success', DataSavedSuccessfully);
                window.location.href = "/Country/Index";
            }
            else {
                ShowNotification('error', Erroroccuredtryagain);
                $(e).attr("disabled", false);
            }
        });
    //}
}

//Cities
$(document).on("keyup", ".citiesTB input", function () {
    ValidateCities();
});
$(document).on("change", ".citiesTB input", function () {
    let $this = this;
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let arName = $.trim($tr.find("[name='CityArabicName']").val());
    let enName = $.trim($tr.find("[name='CityEnglishName']").val());
    jsonCities[$index].IsChanged = true;
    jsonCities[$index].ArabicName = arName;
    jsonCities[$index].EnglishName = enName;
});
function AddCity() {
    let $tbody = $(".citiesTB");
    let $row = `
                  <tr data-id="">
                    <td>
                        <input type="text" name="CityArabicName" class="form-control" placeholder="`+ ArabicName + `" />
                    </td>
                    <td>
                    <input type="text" name="CityEnglishName" class="form-control" placeholder="`+ EnglishName + `" />
                    </td>
                    <td class="button">
                        <button  type="button"  class="btn btn-outline-primary" onclick="OpenFormDistrict(this)">
                        <i class="fas fa-plus"></i>
                        </button>
                    </td>
                    <td>
                        <button type="button" class="btn btn-outline-danger" onclick="DeleteCity(this)">
                            <i class="fas fa-trash-alt"></i>
                        </button>
                    </td>
                </tr>
`;
    $tbody.append($row);
    let obj = { Id: 0, ArabicName: "", EnglishName: "", IsChanged: false, IsDeleted: false,Districts:[] };
    jsonCities.push(obj);
    ValidateCities();
}
function DeleteCity($this) {
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    if ($tr.attr("data-id") != "") {  //remove record from ui & mark it as isDeleted in json
        jsonCities[$index].IsDeleted = true;
        $tr.remove();
    }
    else { //remove record from ui & json
        $tr.remove();
        jsonCities.splice($index, 1);
    }
}
function ValidateCities() {
    
    let isValid = true;
    let $rows = $('.citiesTB tr');
    $rows.each(function () {
        $row = $(this);
        let arNameInpt = $row.find("[name='CityArabicName']");
        let arNameTD = arNameInpt.closest("td");
        let arName = $.trim(arNameInpt.val());
        arNameInpt.removeClass("is-invalid");
        arNameTD.find(".text-danger").remove();
        if (arName == "") {
            arNameInpt.addClass("is-invalid");
            arNameTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }
    });
    return isValid;
}
function ValidateCityRow($tr) {
    let isValid = true;
    let arNameInpt = $tr.find("[name='CityArabicName']");
    let arNameTD = arNameInpt.closest("td");
    let arName = $.trim(arNameInpt.val());
    arNameInpt.removeClass("is-invalid");
    arNameTD.find(".text-danger").remove();
    if (arName == "") {
        arNameInpt.addClass("is-invalid");
        arNameTD.append(`<span class="text-danger">` + Required + `</span>`);
        isValid = false;
    }
    return isValid;
}
//Districts
$(document).on("keyup", ".districtsTB input", function () {
    ValidateDistricts();
});
$(document).on("change", ".districtsTB input", function () {
    let $this = this;
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let cityIndex = $($tr).closest(".districtsTB").attr("id");
    let districtsJson = jsonCities[cityIndex].Districts;
    let arName = $.trim($tr.find("[name='DistrictArabicName']").val());
    let enName = $.trim($tr.find("[name='DistrictEnglishName']").val());
    districtsJson[$index].IsChanged = true;
    districtsJson[$index].ArabicName = arName;
    districtsJson[$index].EnglishName = enName;
    jsonCities[cityIndex].Districts = districtsJson;
    jsonCities[cityIndex].IsChanged = true;
});
function AddDistrict() {
   debugger
    let $tbody = $(".districtsTB");
    let cityIndex = $tbody.attr("id");
    let districtsJson = jsonCities[cityIndex].Districts;
    let $row = `
                  <tr data-id="">
                    <td>
                        <input type="text" name="DistrictArabicName" class="form-control" placeholder="`+ ArabicName + `" />
                    </td>
                    <td>
                    <input type="text" name="DistrictEnglishName" class="form-control" placeholder="`+ EnglishName + `" />
                    </td>

                    <td>
                        <button type="button" class="btn btn-outline-danger" onclick="DeleteDistrict(this)">
                            <i class="fas fa-trash-alt"></i>
                        </button>
                    </td>
                </tr>
`;

    $tbody.append($row);
    let obj = { Id: 0, cityId: 0, ArabicName: "", EnglishName: "", IsChanged: false, IsDeleted: false };
    districtsJson.push(obj);
    jsonCities[cityIndex].Districts = districtsJson;
    jsonCities[cityIndex].IsChanged = true;
    ValidateDistricts();
}
function DeleteDistrict($this) {
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let cityIndex = $tr.closest("tbody").attr("id");
    let districtsJson = jsonCities[cityIndex].Districts;
    if ($tr.attr("data-id") != "") {  //remove record from ui & mark it as isDeleted in json
        districtsJson[$index].IsDeleted = true;
        districtsJson[$index].IsChanged = true;
        $tr.remove();
    }
    else { //remove record from ui & json
        $tr.remove();
        districtsJson.splice($index, 1);
    }
    jsonCities[cityIndex].Districts = districtsJson;
    jsonCities[cityIndex].IsChanged = true;
}
function ValidateDistricts() {
    let isValid = true;
    let $rows = $('.districtsTB tr');
    $rows.each(function () {
        $row = $(this);
        let arNameInpt = $row.find("[name='DistrictArabicName']");
        let arNameTD = arNameInpt.closest("td");
        let arName = $.trim(arNameInpt.val());
        arNameInpt.removeClass("is-invalid");
        arNameTD.find(".text-danger").remove();
        if (arName == "") {
            arNameInpt.addClass("is-invalid");
            arNameTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }
    });
    return isValid;
}
function SaveDistricts() { 
    let ModalDistricts = $("#ModalDistricts");
    ModalDistricts.modal('hide');
}
function OpenFormDistrict($this) {
    
    let ModalForm = $("#ModalDistricts");
    ModalForm.modal('hide');
    let $tr = $($this).closest("tr");
    let cityIndex = $($tr).index();
    jsonCities[cityIndex].Districts = jsonCities[cityIndex].Districts.filter(ent => !ent.IsDeleted);
    let districtsJson = jsonCities[cityIndex].Districts;
    if (ValidateCityRow($tr)) {
        let html = `<table class="table table-striped table-bordered">
    <thead>
        <tr>
            <th>`+ ArabicName+`</th>
            <th>`+ EnglishName+`</th>
            <th>
                <button type="button" class="btn btn-outline-warning" onclick="AddDistrict()">
                    <i class="fas fa-plus"></i>
                </button>
            </th>
        </tr>
    </thead>
    <tbody class="districtsTB">`;
        if (districtsJson != null) {
        for (var i = 0; i < districtsJson.length; i++) {
            let obj = districtsJson[i];
            let tr = `
            <tr data-id="`+ (obj.Id == undefined ? "" : obj.Id) +`">
                <td>
                    <input type="text" name="DistrictArabicName" class="form-control" value="`+ obj.ArabicName + `" placeholder="` + ArabicName+`" />
                </td>
                <td>
                    <input type="text" name="DistrictEnglishName" class="form-control" value="`+ obj.EnglishName + `" placeholder="` + EnglishName+`" />
                </td> 
                <td>
                    <button type="button" class="btn btn-outline-danger" onclick="DeleteDistrict(this)">
                        <i class="fas fa-trash-alt"></i>
                    </button>
                </td>
            </tr>
  
`;
            html += tr;
            }
            html += "  </tbody> </table >";
        }
        ShowModalDistricts(Districts, html, cityIndex);
    }
}
function ShowModalDistricts(header, body, cityIndex) {
    let ModalDistricts = $("#ModalDistricts");
    let ModalHeaderTittle = ModalDistricts.find('#ModalHeaderTittle');
    let ModalBody = ModalDistricts.find('.modal-body');
    let modalDialog = ModalDistricts.find('.modal-dialog');
    ModalHeaderTittle.html(header);
    ModalBody.html(body);
    $(".districtsTB").attr("id", cityIndex);
    modalDialog.addClass("modal-lg");
    ModalDistricts.modal("show");
}
