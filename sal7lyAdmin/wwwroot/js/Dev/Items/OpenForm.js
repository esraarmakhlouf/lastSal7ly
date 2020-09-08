var deletedImagesIds = [];
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
    $(".addfile").click(function () {
        $(".fileinput").trigger('click');
    });
    $(".fileinput").change(function () {
        let fileInput = $(this)[0];
        let fileInputCopy = $(this).clone(false);
        fileInputCopy.removeClass("fileinput");
        fileInputCopy.addClass("selectedFileinput");
        let files = fileInput.files;
        if (files.length > 0) {
            var reader = new FileReader();
            let imagesContainer = $(".imagesContainer");
            for (var i = 0; i < files.length; i++) {
                let file = files[i];
                reader.onload = function (e) {
                    imagesContainer.append(`
                                        <div class="col-md-2">
                                            <div class="card">
                                                <div class="card-image">
                                                    <img src="`+ e.target.result + `" class="card-img-top" Width="100" Height="100" />
                                                    <span class="delete-image" onclick="DeleteImage(this,'')">
                                                            <i class="fas fa-times"></i>
                                                    </span>
                                                </div>
                                            </div>                                               
                                        </div>
                                        `);
                    $('.card:last').append(fileInputCopy);
                };
                reader.readAsDataURL(file);
            }
        }
    });
  
    $("#HasVariant").change(function () {
        let parentDiv = $(".VariantDiv");
        parentDiv.hide();
        parentDiv.find(".form-control").val('').trigger('change');
        if ($(this).prop("checked")) {
            parentDiv.show();
        }
    });
    $("#HasVariant").each(function () {
        let parentDiv = $(".VariantDiv");
        parentDiv.hide();
        jsonVariants = [];
        if ($(this).prop("checked")) {
            parentDiv.show();
        }
    });
    $("#IsHasNutritionfact").change(function () {
        let parentDiv = $(".NutritionFactDiv");
        parentDiv.hide();
        parentDiv.find(".form-control").val('').trigger('change');
        if ($(this).prop("checked")) {
            parentDiv.show();
        }
    });
    $("#IsHasNutritionfact").each(function () {
        let parentDiv = $(".NutritionFactDiv");
        parentDiv.hide();
        if ($(this).prop("checked")) {
            parentDiv.show();
        }
    });
    $("#Sizes , #Colors ").change(function () {
        let sizes = $("#Sizes").val() || [];
        let colors = $("#Colors").val() || [];
        jsonVariants = [];
        if (sizes.length > 0) {
            for (var i = 0; i < sizes.length; i++) {
                let sizeId = parseInt(sizes[i]);
                if (colors.length > 0) {
                    for (var j = 0; j < colors.length; j++) {
                        let colorId = parseInt(colors[j]);
                       
                            jsonVariants.push({
                                itemId: null,
                                sizeId: sizeId,
                                colorId: colorId,
                                price: 0,
                                cost: 0,
                                qty: 0,
                                weight: 0,
                                demandLimitinStock: 0,
                                notes: ""
                            });
                        
                    }
                }
                else {
                    jsonVariants.push({
                        itemId: null,
                        sizeId: sizeId,
                        colorId: null,
                        price: 0,
                        cost: 0,
                        qty: 0,
                        weight: 0,
                        demandLimitinStock: 0,
                        notes: ""
                    });
                }
            }
        }
        DrawVariants();
        ValidateVariants();
    });
});

function Save(e) {
    
    var $form = $("#ItemsForm");
    //if ($form.valid()) {
        debugger
        $("#submitItems").attr("disabled", true);
        var data = ConvertFormToJson($form);
        data["IsActive"] = $("#IsActive").prop("checked");
        data["IsSpecial"] = $("#IsSpecial").prop("checked");
        data["IsNew"] = $("#IsNew").prop("checked");
        data["IsSale"] = $("#IsSale").prop("checked");

        data["IsTopSell"] = $("#IsTopSell").prop("checked");
        data["IsSALEWEEK"] = $("#IsSALEWEEK").prop("checked");
        data["HasVariant"] = $("#HasVariant").prop("checked");
        $.post("/Items/Save", { entity: data }, function (response) {
            var strStatus = response.status;
            if (strStatus == "success") {
                SaveItemImages(response.id);
            }
            else {
                ShowNotification('error', Erroroccuredtryagain);
                $("#submitItems").attr("disabled", false);
            }
        });
    //}
}
//Item Images
function SaveItemImages(id) {
    var formData = new FormData();
    formData.append("id", id);
    formData.append("deletedids", deletedImagesIds);
    let selectedFileinputs = $(".selectedFileinput");
    if (selectedFileinputs.length) {
        for (let k = 0; k < selectedFileinputs.length; k++) {
            let fileInput = selectedFileinputs[k];
            //Iterating through each files selected in fileInput
            for (i = 0; i < fileInput.files.length; i++) {
                //Appending each file to FormData object
                formData.append(fileInput.files[i].name, fileInput.files[i]);
            }
        }
    }
    //Creating an XMLHttpRequest and sending
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/Items/SaveItemImage');
    xhr.send(formData);
   
    xhr.onreadystatechange = function () {
        if (xhr.status == 200 && xhr.readyState == 4) {
            ShowNotification('success', DataSavedSuccessfully);
            window.location.href = "/Items/Index";
        }
    }
}
function DeleteImage($this, id) {
    if (id != null && id != "" && id != "0") { deletedImagesIds.push(id); }
    $($this).closest(".col-md-2").remove();
}
//Item Units
$(document).on("keyup", ".unitsTB input", function () {
    Validateunits();
});
$(document).on("keyup", ".NutritionFactTB input", function () {
    ValidatenutritionFacts();
});
$(document).on("change", ".unitsTB input , .unitsTB checkbox", function () {
    

    let $this = this;
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let rate = $tr.find("[name='Rate']").val();
    let isMain = $tr.find("[name='IsMain']").prop("checked");
    jsonunits[$index].rate = rate;
    jsonunits[$index].isMain = isMain;
    Validateunits();
});
$(document).on("change", ".NutritionFactTB input ", function () {
    let $this = this;
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let value = $tr.find("[name='Value']").val();
    jsonnutritionFact[$index].value = value;
    ValidatenutritionFacts();
});
$(document).on("change", ".variantsTB input , .variantsTB select", function () {
    let $this = $(this);
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let unitId = $tr.find("[name='UnitId']").val();
    let price = $tr.find("[name='Price']").val();
    let cost = $tr.find("[name='Cost']").val();
    let qty = $tr.find("[name='Qty']").val();
    let weight = $tr.find("[name='Weight']").val();
    let demandLimitinStock = $tr.find("[name='DemandLimitinStock']").val();
    let notes = $tr.find("[name='Notes']").val();
    jsonVariants[$index].unitId = unitId;
    jsonVariants[$index].price = price;
    jsonVariants[$index].cost = cost;
    jsonVariants[$index].qty = qty;
    jsonVariants[$index].weight = weight;
    jsonVariants[$index].demandLimitinStock = demandLimitinStock;
    jsonVariants[$index].notes = notes;
    ValidateVariants();
});

function AddUnit() {
    
    let $tbody = $(".unitsTB");
    let selectedUnit = $(".unit");
    if (selectedUnit.val() != "" && $("*[data-unitid='" + selectedUnit.val() + "']").length == 0) {
        let $row = `
                 <tr data-id="" data-unitid="`+ selectedUnit.val() + `">
                <td> `+ selectedUnit.find("option:selected").text
                () + ` </td>
                <td>
                    <input type="number" name="Rate" class="form-control" value="`+ ($tbody.find("tr").length == 0 ? "1" : "") + `" placeholder="` + Rate + `" />
                </td>
                <td>
                    <label class="form-control">
                        <div class="checkbox checkbox-inline checkbox-info peers ai-c">
                            <input type="checkbox"  name="IsMain" class="peer" `+ ($tbody.find("tr").length == 0 ? "checked" : "") + `/>
                            <label for="inputCall1" class="peers peer-greed js-sb ai-c">
                                <span class="peer peer-greed"></span>
                            </label>
                        </div>
                    </label>
                </td>
                <td>
                    <button type="button" class="btn btn-outline-danger" onclick="DeleteUnit(this)">
                        <i class="fas fa-trash-alt"></i>
                    </button>
                </td>
            </tr>
            `;

        let obj = { id: 0, unitId: selectedUnit.val(), isMain: $tbody.find("tr").length == 0, rate: $tbody.find("tr").length == 0 ? 1 : 0 };
        $tbody.append($row);
        jsonunits.push(obj);
        Validateunits();
    }
    Validatemainunit();
}
function DeleteUnit($this) {
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    jsonunits.splice($index, 1);
    $tr.remove();
    Validatemainunit();
}
function Validateunits() {
    let isValid = true;
    let $rows = $('.unitsTB tr');
    let mainUnits = $("*[name='IsMain']:checked");
    $rows.each(function () {
        let $row = $(this);
        let rateInpt = $row.find("[name='Rate']");
        let rateTD = rateInpt.closest("td");
        let rate = parseFloat($.trim(rateInpt.val() || 0));
        rateInpt.removeClass("is-invalid");
        rateTD.find(".text-danger").remove();
        if ($.trim(rateInpt.val()) == "") {
            rateInpt.addClass("is-invalid");
            rateTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }
        if (rate <= 0 && isValid) {
            rateInpt.addClass("is-invalid");
            rateTD.append(`<span class="text-danger">` + PositiveNumberGreaterThanZero + `</span>`);
            isValid = false;
        }
    });

    $("*[name='IsMain']").each(function () {
        let isMainTD = $(this).closest("td");
        $(this).removeClass("is-invalid");
        isMainTD.find(".text-danger").remove();
    });

    if (mainUnits.length > 1) {
        mainUnits.each(function () {
            let isMainTD = $(this).closest("td");
            $(this).addClass("is-invalid");
            isMainTD.append(`<span class="text-danger">` + OnlyOneMainUnitMustBeSelected + `</span>`);
            isValid = false;
        });
    }

    if (mainUnits.length == 0) {
        $("*[name='IsMain']").each(function () {
            let isMainTD = $(this).closest("td");
            $(this).removeClass("is-invalid");
            isMainTD.find(".text-danger").remove();
            $(this).addClass("is-invalid");
            isMainTD.append(`<span class="text-danger">` + OneMainUnitMustBeSelected + `</span>`);
            isValid = false;
        });
    }
    return isValid;
}

function AddNutritionFact() {
    let $tbody = $(".NutritionFactTB");
    let selectedNutritionFact = $(".nutrition-fact");
    if (selectedNutritionFact.val() != "" && $("*[data-nutritionFactid='" + selectedNutritionFact.val() + "']").length == 0) {
        let $row = `
                 <tr data-id="" data-nutritionFactid="`+ selectedNutritionFact.val() + `">
                <td> `+ selectedNutritionFact.find("option:selected").text
                () + ` </td>
                <td>
                    <input type="number" name="Value" class="form-control" value="" placeholder="" />
                </td>
                <td>
                    <button type="button" class="btn btn-outline-danger" onclick="DeleteNutritionFact(this)">
                        <i class="fas fa-trash-alt"></i>
                    </button>
                </td>
            </tr>
            `;

        $tbody.append($row);
        let obj = { id: 0, NutritionFactId: selectedNutritionFact.val(), value: $tbody.find("tr").length == 0 ? 1 : 0 };
        jsonnutritionFact.push(obj);
        ValidatenutritionFacts();
    }
}
function DeleteNutritionFact($this) {
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    jsonnutritionFact.splice($index, 1);
    $tr.remove();
    ValidatenutritionFacts();
}
function ValidatenutritionFacts() {
    let isValid = true;
    let $rows = $('.NutritionFactTB tr');
    $rows.each(function () {
        let $row = $(this);
        let valueInpt = $row.find("[name='Value']");
        let valueTD = valueInpt.closest("td");
        let value = parseFloat($.trim(valueInpt.val() || 0));
        valueInpt.removeClass("is-invalid");
        valueTD.find(".text-danger").remove();
        if ($.trim(valueInpt.val()) == "") {
            valueInpt.addClass("is-invalid");
            valueTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }
        if (value <= 0 && isValid) {
            valueInpt.addClass("is-invalid");
            valueTD.append(`<span class="text-danger">` + PositiveNumberGreaterThanZero + `</span>`);
            isValid = false;
        }
    });


    return isValid;
}

//Item Variants
function DrawVariants() {
    let $body = $('.variantsTB');
    $body.html('');
    let varStr = "";
    for (var i = 0; i < jsonVariants.length; i++) {
        let units = $(".unit").clone();
        units.removeClass("unit");
        units.attr("id", "UnitId");
        units.attr("name", "UnitId");
        varStr = "";
        let size = sizesList.filter(ent => ent.id == jsonVariants[i].sizeId);
        let color = colorsList.filter(ent => ent.id == jsonVariants[i].colorId);
        varStr += (size.length == 0 ? "" : size[0].text) + " " + (color.length == 0 ? "" : color[0].text) + " " ;
        $body.append(`
                        <tr data-id="" data-colorid="`+ jsonVariants[i].colorId + `" data-sizeid="` + jsonVariants[i].sizeId + `" data-matterialid="` + jsonVariants[i].materialId + `">
                        <td> `+ varStr + ` </td>
                       
                        <td>
                            <input type="number" name="Price" class="form-control" value="`+ jsonVariants[i].price + `" placeholder="` + Price + `" />
                        </td>
                        <td>
                            <input type="number" name="Cost" class="form-control" value="`+ jsonVariants[i].cost + `" placeholder="` + Cost + `" />
                        </td>
                        <td>
                            <input type="number" name="Qty" class="form-control" value="`+ jsonVariants[i].qty + `" placeholder="` + Quantity + `" />
                        </td>
                        <td>
                            <input type="number" name="Weight" class="form-control" value="`+ jsonVariants[i].weight + `" placeholder="` + Weight + `" />
                        </td>
                        <td>
                            <input type="number" name="DemandLimitinStock" class="form-control" value="`+ jsonVariants[i].demandLimitinStock + `" placeholder="` + DemandLimitinStock + `" />
                        </td>
                        <td>
                            <input type="text" name="Notes" class="form-control" value="`+ jsonVariants[i].notes + `" placeholder="` + Notes + `" />
                        </td>
                    </tr> `);
        $body.find("tr:last").find(".form-control").trigger("change");
        $(".select2").select2({ width: null });
    }

}
function ValidateVariants() {
    let isValid = true;
    

    let $rows = $('.variantsTB tr');
    $rows.each(function () {
        let $row = $(this);

        let costInpt = $row.find("[name='Cost']");
        let costTD = costInpt.closest("td");
        let cost = parseFloat($.trim(costInpt.val() || 0));
        costInpt.removeClass("is-invalid");
        costTD.find(".text-danger").remove();
        if ($.trim(costInpt.val()) == "") {
            costInpt.addClass("is-invalid");
            costTD.append(`<span class="text-danger">` + Required + `</span>`);
            isValid = false;
        }
        if (cost < 0 && isValid) {
            costInpt.addClass("is-invalid");
            costTD.append(`<span class="text-danger">` + PositiveNumberOnly + `</span>`);
            isValid = false;
        }

        let priceInpt = $row.find("[name='Price']");
        let priceTD = priceInpt.closest("td");
        let price = parseFloat($.trim(priceInpt.val() || 0));
        priceInpt.removeClass("is-invalid");
        priceTD.find(".text-danger").remove();
        if (price < 0) {
            priceInpt.addClass("is-invalid");
            priceTD.append(`<span class="text-danger">` + PositiveNumberOnly + `</span>`);
            isValid = false;
        }

        let qtyInpt = $row.find("[name='Qty']");
        let qtyTD = qtyInpt.closest("td");
        let qty = parseFloat($.trim(qtyInpt.val() || 0));
        qtyInpt.removeClass("is-invalid");
        qtyTD.find(".text-danger").remove();
        if (qty < 0) {
            qtyInpt.addClass("is-invalid");
            qtyTD.append(`<span class="text-danger">` + PositiveNumberOnly + `</span>`);
            isValid = false;
        }

        let weightInpt = $row.find("[name='Weight']");
        let weightTD = weightInpt.closest("td");
        let weight = parseFloat($.trim(weightInpt.val() || 0));
        weightInpt.removeClass("is-invalid");
        weightTD.find(".text-danger").remove();
        if (weight < 0) {
            weightInpt.addClass("is-invalid");
            weightTD.append(`<span class="text-danger">` + PositiveNumberOnly + `</span>`);
            isValid = false;
        }

        let demandLimitinStockInpt = $row.find("[name='DemandLimitinStock']");
        let demandLimitinStockTD = demandLimitinStockInpt.closest("td");
        let demandLimitinStock = parseFloat($.trim(demandLimitinStockInpt.val() || 0));
        demandLimitinStockInpt.removeClass("is-invalid");
        demandLimitinStockTD.find(".text-danger").remove();
        if (demandLimitinStock < 0) {
            demandLimitinStockInpt.addClass("is-invalid");
            demandLimitinStockTD.append(`<span class="text-danger">` + PositiveNumberOnly + `</span>`);
            isValid = false;
        }
    });
    return isValid;
}
function OpenSizeForm() {
    $.get("/Sizes/OpenForm/", {}, function (response) {
        let $modal = $("#SizesModalForm");
        let modalBody = $modal.find(".modal-body");
        modalBody.html(response);
        $modal.modal("show");
    });
}
function SaveSizesForm() {
    $('#SizesForm').submit();
}
function OpenColorsForm() {
    $.get("/Colors/OpenForm/", {}, function (response) {
        let $modal = $("#ColorsModalForm");
        let modalBody = $modal.find(".modal-body");
        modalBody.html(response);
        $modal.modal("show");
    });
}
function SaveColorsForm() {
    $('#ColorsForm').submit();
}


function ReFillVariantFactors(source) {
    let factor = $("#" + source);
    let selectedValues = factor.val() || [];
    factor.empty();
    $.get("/Items/ReFillVariantFactors", { source: source }, function (response) {
        response.unshift({ id: '', text: '' });
        switch (source) {
            case "Colors":
                colorsList = response;
                break;
            case "Sizes":
                sizesList = response;
                break;
          
            default:
                break;
        }
        factor.select2({ data: response }).val(selectedValues).trigger("change");
    });
}

function Validatemainunit() {
    let isValid = true;
    $(".MainUnitMustBeSelected").hide();
    if (jsonunits.length == 0) {
        $(".MainUnitMustBeSelected").show();
    }
    return isValid;
}

// Aditional Item
$("#HasAdditionalItems").change(function () {
    var isApply = $(this).prop("checked");
    if (isApply) {
        $("#AdditionalItemsDiv").removeClass("hideDiv");
        $("#AdditionalItemsDiv").addClass("displayDiv");
    }
    else {
        $("#AdditionalItemsDiv").removeClass("displayDiv");
        $("#AdditionalItemsDiv").addClass("hideDiv");
        $("#AdditionalTbl tbody").html('');
    }
});
//AdditionalTB
$(document).on("keyup", ".AdditionalTB input", function () {
    ValidateAdditional();
});
$(document).on("change", ".AdditionalTB input", function () {
    let $this = this;
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let ItemsId = $.trim($tr.find("[name='ItemsId']").val());
    let PriceItem = $.trim($tr.find("[name='PriceItem']").val());
    let isActive = $tr.find("[name='IsActive']").prop("checked");
    jsonAddItems[$index].itemsId = ItemsId;
    jsonAddItems[$index].priceItem = PriceItem;
    jsonAddItems[$index].isChanged = true;
    jsonAddItems[$index].isActive = isActive;
});
$(document).on("change", ".AdditionalTB select", function () {
    
    let $this = this;
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    let ItemsId = $.trim($tr.find("[name='ItemsId']").val());
    let PriceItem = $.trim($tr.find("[name='PriceItem']").val());
    let isActive = $tr.find("[name='IsActive']").prop("checked");
    jsonAddItems[$index].itemsId = ItemsId;
    jsonAddItems[$index].priceItem = PriceItem;
    jsonAddItems[$index].isChanged = true;
    jsonAddItems[$index].isActive = isActive;
});
function AddAdditional() {
    let $tbody = $(".AdditionalTB");
    let $AddItems = $(".AddItems").clone();
    let tempid = GenerateUniqueId();
    let $row = `
                  <tr data-id="">
                    <td>
                        <select name="ItemsId" class="form-control select2 "> `+ $AddItems.html() + `</select>
                    </td>
                    <td>
                    <input type="number" name="PriceItem" class="form-control"/>
                    </td>
                    <td>
                        <input name="IsActive" class="styled-checkbox" type="checkbox" id="styled-checkbox-`+ tempid + `"  checked="" />
                        <label for="styled-checkbox-`+ tempid + `"></label>
                    </td>
                    <td>
                        <button type="button" class="btn btn-outline-danger" onclick="DeleteAdditional(this)">
                            <i class="fas fa-trash-alt"></i>
                        </button>
                    </td>
                </tr>
`;

    $tbody.append($row);
    let obj = { Id: 0, itemsId: 0, priceItem: "", isActive: true, isChanged: false, isDeleted: false };
    jsonAddItems.push(obj);
    ValidateAdditional();

}
function DeleteAdditional($this) {
    let $tr = $($this).closest("tr");
    let $index = $($tr).index();
    if ($tr.attr("data-id") != "") {
        jsonAddItems[$index].isDeleted = true;
        $tr.remove();
    }
    else {
        $tr.remove();
        jsonAddItems.splice($index, 1);
    }
}
function ValidateAdditional() {
    
    var IsChecked = $("#HasAdditionalItems").prop("checked");
    if (!IsChecked) {
        return true;
    }
    let isValid = true;
    let $rows = $('.AdditionalTB tr');
    if ($rows.length == 0 && !IsChecked) {
        isValid = false;
        $("#AdditionalValidation").removeClass("hideDiv");
        $("#AdditionalValidation").addClass("displayDiv");
    }
    else {
        $("#AdditionalValidation").removeClass("displayDiv");
        $("#AdditionalValidation").addClass("hideDiv");
        $rows.each(function () {
            
            $row = $(this);
            let ItemsIdInpt = $row.find("[name='ItemsId']");
            let ItemsIdInptTD = ItemsIdInpt.closest("td");
            let ItemsId = $.trim(ItemsIdInpt.val());
            ItemsIdInpt.removeClass("is-invalid");
            ItemsIdInptTD.find(".text-danger").remove();
            if (ItemsId == "") {
                ItemsIdInpt.addClass("is-invalid");
                ItemsIdInptTD.append(`<span class="text-danger">` + Required + `</span>`);
                isValid = false;
            }
            let PriceItemInpt = $row.find("[name='PriceItem']");
            let PriceItemInptTD = PriceItemInpt.closest("td");
            let PriceItem = $.trim(PriceItemInpt.val());
            PriceItemInpt.removeClass("is-invalid");
            PriceItemInptTD.find(".text-danger").remove();
            if (PriceItem == "") {
                PriceItemInpt.addClass("is-invalid");
                PriceItemInptTD.append(`<span class="text-danger">` + Required + `</span>`);
                isValid = false;
            }
            else {
                if (PriceItem < 0) {
                    PriceItemInpt.addClass("is-invalid");
                    PriceItemInptTD.append(`<span class="text-danger">` + NotValid + `</span>`);
                    isValid = false;
                }
            }
        });

    }
    return isValid;
}