function SearchItems($this) {
    let $form = $($this).closest("form");
    let searchTerm = $.trim($form.find("[name='searchTerm']").val().toLowerCase() || "");
    let categoryId = $form.find("[name='category_id']").val() || null;
    if (searchTerm != "" || categoryId != "" || categoryId != null) {
        $.get("/Items/SearchItems", { searchTerm: searchTerm, categoryId: categoryId }, function (reponse) {
            $("#allitemssection").html(reponse);
        });
    }
}
function ChangeCulture(culture) {
    $.get("/Home/SetLanguage", { culture: culture }, function (response) {
        window.location.reload();
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
    ModalForm.modal("show");
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
function removeUpload() {
    //https://codepen.io/aaronvanston/pen/yNYOXR
    $('.file-upload-input').replaceWith($('.file-upload-input').clone());
    $('.file-upload-content').hide();
    $('.image-upload-wrap').show();
}

function ConvertFormToJson($form) {
    var unindexed_array = $form.serializeArray();
    var indexed_array = {};

    $.map(unindexed_array, function (n, i) {
        indexed_array[n['name']] = n['value'];
    });

    return indexed_array;
}