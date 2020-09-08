
$(function () {
    // open current active nav item
    let $li = $(".active");
    $li.closest("ul.sub-menu").addClass("show");
    $li.closest("li.menu-item-has-children").addClass("show");
    if ($(".file-upload-image").attr("src") != "" && $(".file-upload-image").attr("src") != "#") {
        $('.file-upload-content').show();
        $('.image-upload-wrap').hide();
    }
});

$('.image-upload-wrap').bind('dragover', function () {
    $('.image-upload-wrap').addClass('image-dropping');
});
$('.image-upload-wrap').bind('dragleave', function () {
    $('.image-upload-wrap').removeClass('image-dropping');
});
$(document).on('change', '.select2', function (e) {
    if ($(this).attr("data-val") == "true") {
        $(this).keyup();
    }
});
$(document).on('click', '.searchkey', function () {
    var parentForm = $(this).closest(".grid-container");
    parentForm.find('.searchkey').removeClass("activekey");
    var searchTerm = parentForm.find('.searchTerm');
    searchTerm.val('');
    var displayText = $(this).text();
    var searchkeyDDL = parentForm.find('.searchkeyDDL');
    searchkeyDDL.each(function () {
        $(this).find(".form-control").val('');
        $(this).hide();
    });
    searchTerm.show();
    $(this).addClass("activekey");
    parentForm.find('.selected').text(displayText);
    if ($(this).hasClass("replaceWithDDL")) {
        var DDLParent = $("#" + $(this).attr("data-Searchkey") + "Parent");
        var DDLElement = $("#N" + $(this).attr("data-Searchkey"));
        DDLElement.val('');
        searchTerm.hide();
        DDLParent.show();
    }
    var formObj = parentForm.find('.normal-search');
    var hf = formObj.find('.searchKeyhf');
    if (hf.length > 0) {
        hf.val($(this).attr("data-searchkey"));
    }
    else {
        formObj.append("<input class='searchKeyhf' type='hidden' name='searchKey' value='" + $(this).attr("data-searchkey") + "' />");
    }
    //Search(this);
});
$(document).ajaxStart(function () { ShowLoader(); });
$(document).ajaxComplete(function () {
    $('.data-table').DataTable({
        retrieve: true,
        "searching": false,
        "pageLength": 20,
        "bLengthMenu": false,
        "bLengthChange": false,
        "bInfo": false
    });
    $('.select2').select2({ width: null });
    HideLoader();
});
$(document).on('click', "body", function () {
    //Intailize BS confirmation
    $().confirmation && $("[data-toggle=confirmation]").confirmation({
        btnOkClass: "btn btn-sm btn-success", btnCancelClass: "btn btn-sm btn-danger"
    });
});

function Delete($this) {

    let controllerName = $($this).attr("data-controllername");
    let id = $($this).attr("data-id");
    $.post("/" + controllerName + "/Delete", { id: id }, function (data) {

        $("#DeleteAlert").modal("hide");
        if (data.status === "success") {
            $('.grid-container').find('#searchButton').trigger('click');
            ShowNotification('success', DataSavedSuccessfully);
        }
        else { ShowNotification('error', data.errorMessage); }
    });
}
function ShowDeleteAlert(controllerName, id) {

    let deleteAlert = $("#DeleteAlert");
    let deleteConfirmedBtn = deleteAlert.find(".deleteconfirmedbtn");
    deleteConfirmedBtn.attr("data-controllername", controllerName);
    deleteConfirmedBtn.attr("data-id", id);
    deleteAlert.modal("show");
}
function readURL(input) {
    //https://codepen.io/aaronvanston/pen/yNYOXR
    if (input.files && input.files[0]) {

        var reader = new FileReader();
        reader.onload = function (e) {
            $('.image-upload-wrap').hide();
            $('.file-upload-image').attr('src', e.target.result);
            $('.file-upload-content').show();
            $('.image-title').html(input.files[0].name);
        };
        reader.readAsDataURL(input.files[0]);
    } else { removeUpload(); }
}
function readAllURL(input) {
    //https://codepen.io/aaronvanston/pen/yNYOXR
    if (input.files) {
        for (var i = 0; i < input.files.length; i++) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('.image-upload-wrap').hide();
                $('.file-upload-image').attr('src', e.target.result);
                $('.file-upload-content').show();
                $('.image-title').html(input.files[i].name);
            };
            reader.readAsDataURL(input.files[0]);
        }

    } else { removeUpload(); }
}
function removeUpload() {
    //https://codepen.io/aaronvanston/pen/yNYOXR
    $('.file-upload-input').replaceWith($('.file-upload-input').clone());
    $('.file-upload-content').hide();
    $('.image-upload-wrap').show();
}
function GenerateUniqueId() {
    return Math.floor(Math.random() * 26) + Date.now();
}
function GoAdvancedSearch(element) {
    var parentForm = $(element).closest('form');
    var gridContainer = $(element).closest('.grid-container');
    gridContainer.find('.normal-search').hide();
    gridContainer.find(".customHeight").removeClass("normal-search-customHight");
    gridContainer.find(".customHeight").addClass("advanced-search-customHight");
    gridContainer.find('.advanced-search').show();
}
function GoNormalSearch(element) {
    var parentForm = $(element).closest('form');
    var gridContainer = $(element).closest('.grid-container');
    gridContainer.find('.advanced-search').hide();
    gridContainer.find('.normal-search').show();
    gridContainer.find(".customHeight").removeClass("advanced-search-customHight");
    gridContainer.find(".customHeight").addClass("normal-search-customHight");
}
var SearchComplete = function SearchComplete() {
    $('.select2').select2({ width: null });
    //Intailize BS confirmation
    $().confirmation && $("[data-toggle=confirmation]").confirmation({
        btnOkClass: "btn btn-sm btn-success", btnCancelClass: "btn btn-sm btn-danger"
    });
}
function SetSearchTerm(element) {
    var parentForm = $(element).closest('form');
    parentForm.find('.searchTerm').val($(element).val());
}
function Search(element) {
    var parentForm = $(element).closest('form');
    var gridContainer = $(element).closest('.grid-container');
    var gridElement = gridContainer.find("#listDiv");
    var strValue = $.trim(parentForm.find(".searchTerm").val());
    var strSource = $.trim(parentForm.find("#source").val());
    var searchkey = parentForm.find('.searchkey.activekey').attr("data-searchkey") || "";
    var url = parentForm.attr("action");
    var formData;
    if (parentForm.hasClass("advanced-search")) { formData = parentForm.serializeArray(); }
    else { formData = { searchTerm: strValue, searchKey: searchkey, source: strSource }; }
    //get search result
    $.post(url, formData, function (data) {
        gridElement.html(data);
        SearchComplete();
    });
}
function ShowNotification(_Type, message) {
    if (_Type == 'success') {
        $.notify({
            title: '<strong><i class="fas fa-check"></i></strong>',
            message: message
        }, {
            type: 'success'
        });
    }

    else if (_Type == 'warning') {
        $.notify({
            title: '<strong><i class="fas fa-exclamation-triangle"></i></strong>',
            message: message
        }, {
            type: 'warning'
        });
    }

    else if (_Type == 'error') {
        $.notify({
            title: '<strong><i class="fas fa-ban"></i></strong>',
            message: message
        }, {
            type: 'danger'
        });
    }
}
function ChangeCulture(culture) {
    $.get("/Home/SetLanguage", { culture: culture }, function (response) {
        window.location.reload();
    });
}
function ShowLoader() {
    $('.loadingContainer').show();
}
function HideLoader() {
    $('.loadingContainer').hide();
}
function ConvertFormToJson($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}
function ShowModalForm(size, header, body) {

    var ModalForm = $("#ModalForm");
    var ModalHeaderTittle = ModalForm.find('#ModalHeaderTittle');
    var ModalBody = ModalForm.find('.modal-body');
    var modalDialog = ModalForm.find('.modal-dialog');
    ModalHeaderTittle.html(header);
    ModalBody.html(body);
    modalDialog.removeClass("modal-sm");
    modalDialog.removeClass("modal-lg");
    if (size == "lg") {
        modalDialog.addClass("modal-lg");
    }
    else if (size == "sm") {
        modalDialog.addClass("modal-sm");
    }
    $("#SubmitModalBtn").prop("disabled", false);
    ModalForm.modal("show");
}



function comparevalues(x, compareoperator, y) {

    if ($.isNumeric(x) && $.isNumeric(y)) {
        x = parseFloat(x);
        y = parseFloat(y);
    }
    else if (x.match(/\//) && y.match(/\//)) {
        x = Date.parse(x);
        y = Date.parse(y);
    }
    else if (x != Date.parse(x, "hh:mm tt") && y != Date.parse(y, "hh:mm tt")) {
        x = Date.parse(x, "hh:mm tt");
        y = Date.parse(y, "hh:mm tt");
    }
    switch (compareoperator) {
        case '<':
            return (x < y);
            break;
        case '<=':
            return (x <= y);
            break;
        case '=':
            return (x == y);
            break;
        case '>=':
            return (x >= y);
            break;
        case '>':
            return (x > y);
            break;
        case '!=':
            return (x != y);
            break;
    }
}


function getDateJs(dateObject) {
    var d = new Date(dateObject);
    var day = d.getDate();
    var month = d.getMonth() + 1;
    var year = d.getFullYear();
    if (day < 10) {
        day = "0" + day;
    }
    if (month < 10) {
        month = "0" + month;
    }
    var date = day + "/" + month + "/" + year;

    return date;
};

function dateToHowManyAgo(stringDate) {
    var currDate = new Date();
    var diffMs = currDate.getTime() - new Date(stringDate).getTime();
    var sec = diffMs / 1000;
    if (sec < 60)
        return parseInt(sec) + ' second' + (parseInt(sec) > 1 ? 's' : '') + ' ago';
    var min = sec / 60;
    if (min < 60)
        return parseInt(min) + ' minute' + (parseInt(min) > 1 ? 's' : '') + ' ago';
    var h = min / 60;
    if (h < 24)
        return parseInt(h) + ' hour' + (parseInt(h) > 1 ? 's' : '') + ' ago';
    var d = h / 24;
    if (d < 30)
        return parseInt(d) + ' day' + (parseInt(d) > 1 ? 's' : '') + ' ago';
    var m = d / 30;
    if (m < 12)
        return parseInt(m) + ' month' + (parseInt(m) > 1 ? 's' : '') + ' ago';
    var y = m / 12;
    return parseInt(y) + ' year' + (parseInt(y) > 1 ? 's' : '') + ' ago';
}


function getTimeInterval(date) {
    let seconds = Math.floor((Date.now() - date) / 1000);
    let unit = "second";
    let direction = "ago";
    if (seconds < 0) {
        seconds = -seconds;
        direction = "from now";
    }
    let value = seconds;
    if (seconds >= 31536000) {
        value = Math.floor(seconds / 31536000);
        unit = "year";
    } else if (seconds >= 86400) {
        value = Math.floor(seconds / 86400);
        unit = "day";
    } else if (seconds >= 3600) {
        value = Math.floor(seconds / 3600);
        unit = "hour";
    } else if (seconds >= 60) {
        value = Math.floor(seconds / 60);
        unit = "minute";
    }
    if (value != 1)
        unit = unit + "s";
    return value + " " + unit + " " + direction;
}

function createAlet(title, text, id,url) {
    $(document).Toasts('create', {
        title: title,
        position: 'bottomRight',
        body: `<div class="Alert-body" onclick="clickAlert(${id}, '${url}')" >${text}</div>`
       
       
    })
}
function clickAlert(id, url) {

    $.get("/SendData/updateNotf/" + id, function (data) {
        window.location.href = `${url}`

    });
};