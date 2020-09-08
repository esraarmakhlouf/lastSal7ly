var map = null;
var myLocation = null;
var marker = null;
var input = null;
var autocomplete = null;
var infowindow = null;
var infowindowContent = null;
var geocoder = null;
var myOptions = {
    zoom: 4,
    zoomControl: true,
    center: null,
    mapTypeId: null
};

$(function () {
    $("#CityId").change(function () {

        let districts = $("#Dist");
        $("#Dist").html("");
        if ($(this).val() != "") {
            $.get("/Users/FillDistricts", { id: $(this).val() }, function (response) {
                //response.unshift({ id: "", text: districts.attr("placeholder") });
                districts.select2({ data: response });
            });
        }
    });
});

function Save(e) {
    let $form = $("#UsersForm");
    $("#submitUsers").attr("disabled", true);
    var data = ConvertFormToJson($form);
    data["IsActive"] = $("#IsActive").prop("checked");
    data["IsManager"] = $("#IsManager").prop("checked");
    data["IsMaster"] = $("#IsMaster").prop("checked");
    data["IsParentage"] = $("#IsParentage").prop("checked");
    data["IsCompanyManager"] = $("#IsCompanyManager").prop("checked");
    $form.submit();
}

var AfterSave = function (data) {
    var strStatus = data.responseJSON.status;
    if (strStatus == "success") {
        SaveUsersImage(data.responseJSON.id);
    }
    else {
        ShowNotification('error', Erroroccuredtryagain);
        $("#submitUsers").attr("disabled", false);
    }
}

function fileinputchange(e) {
    let fileInput = $("#Image")[0];

    let fileInputCopy = $("#Image").clone(false);
    //fileInputCopy.removeClass("fileinput");
    //fileInputCopy.addClass("selectedFileinput");

    //let files = fileInput.files;
    var UsersImagecontainer = $("#UsersImagecontainer");
    var fileImagecontainer = $("#fileImagecontainer");
    if (fileInput.files.length > 0) {
        var reader = new FileReader();
        let file = fileInput.files[0];
        reader.onload = function (e) {
            fileImagecontainer.html(`
                     <img src="`+ e.target.result + `" class="img-circle" width="60" height="60" />
                                  `)
            //$('.imageContainer:last').append(fileInputCopy);
        };
        reader.readAsDataURL(file);
        UsersImagecontainer.hide();
        fileImagecontainer.show();
    }
    else {
        UsersImagecontainer.show();
        fileImagecontainer.html('');
    }
}
$(function () {
    $(".addfile").click(function () {
        $(".fileinput").trigger('click');
        console.log("load");
    });
});

//Item Images
function SaveUsersImage(id) {

    var formData = new FormData();
    formData.append("id", id);
    var fileInput = $('#Image')[0];
    if (fileInput.files.length > 0) {
        formData.append(fileInput.files[0].name, fileInput.files[0]);
        //Creating an XMLHttpRequest and sending
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Users/SaveItemImage');
        xhr.send(formData);
        xhr.onreadystatechange = function () {
            if (xhr.status == 200 && xhr.readyState == 4) {
                ShowNotification('success', DataSavedSuccessfully);
                window.location.href = "/Users/Index";
            }
        }
    }
    else {
        ShowNotification('success', DataSavedSuccessfully);
        window.location.href = "/Users/Index";
    }

}

//Map
function initMap() {

    myOptions.mapTypeId = google.maps.MapTypeId.ROADMAP;
    let lat = 0;
    let lng = 0;
    let location = $('.XY_Position').val() || "0,0";
    lat = location.split(',')[0];
    lng = location.split(',')[1];
    myLocation = new google.maps.LatLng(lat, lng);
    myOptions.center = myLocation;
    if (map == null) {
        map = new google.maps.Map(document.getElementById('map'), myOptions);
        google.maps.event.addListener(map, 'click', function (event) {
            let clickedLocation = event.latLng;
            if (marker == null) {
                marker = new google.maps.Marker({
                    position: clickedLocation,
                    map: map,
                    draggable: true //make it draggable
                });
                google.maps.event.addListener(marker, 'dragend', function (event) {
                    MarkerLocation();
                });
            }
            else { marker.setPosition(clickedLocation); }
            myLocation = clickedLocation;
            MarkerLocation();
            geocoder.geocode({
                'latLng': event.latLng
            }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        infowindowContent.children['place-address'].textContent =
                            results[0].formatted_address;
                        $("#Address").val(results[0].formatted_address);

                    }
                }
            });

        });
    }
    if (input == null) {
        input = document.getElementById('pac-input');
        map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    }
    input.value = "";
    if (autocomplete == null) {
        autocomplete = new google.maps.places.Autocomplete(input);
        autocomplete.bindTo('bounds', map);
        autocomplete.setFields(['place_id', 'geometry', 'name']);
        autocomplete.addListener('place_changed', function () {
            infowindow.close();
            var place = autocomplete.getPlace();
            if (!place.geometry) { return; }
            if (place.geometry.viewport) { map.fitBounds(place.geometry.viewport); }
            else {
                map.setCenter(place.geometry.location);
                map.setZoom(17);
            }
            marker.setPosition(place.geometry.location);
            marker.setVisible(true);
            $('.XY_Position').val(place.geometry.location.lat() + "," + place.geometry.location.lng());
            $('#Lat').val(place.geometry.location.lat());
            $('#Long').val(place.geometry.location.lng());
            infowindowContent.children['place-name'].textContent = place.name;
            infowindowContent.children['place-address'].textContent =
                place.formatted_address;
            geocoder.geocode({
                'latLng': place.geometry.location
            }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        infowindowContent.children['place-address'].textContent =
                            results[0].formatted_address;
                        $("#Address").val(results[0].formatted_address);

                    }
                }
            });

            infowindow.open(map, marker);

        });
    }
    if (infowindow == null) { infowindow = new google.maps.InfoWindow(); }
    if (infowindowContent == null) {

        infowindowContent = document.getElementById('infowindow-content');
        infowindow.setContent(infowindowContent);
    }
    if (marker == null) {
        marker = new google.maps.Marker({ map: map });
        //attach map events
        marker.addListener('click', function () {
            infowindow.open(map, marker);
        });
    }
    marker.setPosition(myLocation);
    map.setCenter(myLocation);

    if (geocoder == null) { geocoder = new google.maps.Geocoder; }
    geocoder.geocode({ 'latLng': myLocation }, function (results, status) {
        if (status === 'OK') {
            if (results[0]) {
                map.setZoom(17);
                infowindowContent.children['place-name'].textContent = "";
                infowindowContent.children['place-address'].textContent =
                    results[0].formatted_address;

                infowindow.open(map, marker);
            }
        }
    });

}
function MarkerLocation() {

    $(".XY_Position").val(myLocation.lat() + "," + myLocation.lng());
    $('#Lat').val(myLocation.lat());
    $('#Long').val(myLocation.lng());
}
$(document).ready(function () {
    $("#JobTitleId").change(function () {

        if ($(this).val() == 2 || $(this).val() == 3) {
            $("#commission-div").show();
        } else {
            $("#commission-div").hide();
        }
        if ($(this).val() == 2) {
            $("#service-div").show();
        } else {
            $("#service-div").hide();
        }
    });
});

