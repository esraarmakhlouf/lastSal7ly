var map = null;
var myLocation = null;
var marker = null;
var input = null;
var autocomplete = null;
var infowindow = null;
var infowindowContent = null;
var geocoder = null;
var myOptions = {
    zoom: 17,
    zoomControl: true,
    center: null,
    mapTypeId: null
};
$(function () {
    $("#CityId").change(function () {
        let districts = $("#DistrictId");
        districts.empty();
        if ($(this).val() != "") {
            let result = jsonDistricts.filter(ent => ent.cityId == $(this).val());
            result.unshift({ id: "", text: districts.attr("placeholder") });
            districts.select2({ data: result });
        }
    });
});

function ModalCustomerAddress($this) {
    $("#AddressForm .form-control").val('').trigger('change');
    let $form = $("#AddressForm");
    let ModalDistricts = $("#ModalCustomerAddress");
    if ($this == undefined) {
        $form.attr("data-index", "-1");
        ModalDistricts.modal("show");
    }
    else {
        let $row = $($this).closest("tr");
        let $index = $row.index();
        let obj = jsonCustomerAddress[$index];
        $form.attr("data-index", $index);
        $form.find("[name='IsActive']").prop("checked", obj["isActive"]);
        $form.find("[name='IsMain']").prop("checked", obj["isMain"]);
        $form.find("[name='Location']").val(obj["location"]);
        $form.find("[name='AddressArabicName']").val(obj["addressArabicName"]);
        $form.find("[name='AddressEnglishName']").val(obj["addressEnglishName"]);
        $form.find("[name='Address']").val(obj["address"]);
        $form.find("[name='CityId']").val(obj["cityId"]).trigger("change");
        //fill dist ddl
        let districts = $form.find("[name='DistrictId']");
        let result = jsonDistricts.filter(ent => ent.cityId == obj["cityId"]);
        result.unshift({ id: "", text: districts.attr("placeholder") });
        districts.select2({ data: result });
        districts.val(obj["districtId"]).trigger("change");
        ModalDistricts.modal("show");
    }
    initMap();
}
function SaveCustomerAddress() {
    var $form = $("#AddressForm");
    if ($form.valid()) {
        var data = ConvertFormToJson($form);
        data["IsActive"] = $form.find("#IsActive").prop("checked");
        data["IsMain"] = $form.find("#IsMain").prop("checked");
        let name = IsArabic == 'True' ? data["AddressArabicName"] : data["AddressEnglishName"];
        let cityName = $("#CityId option:selected").text();
        let districtName = $("#DistrictId option:selected").text();
        let isActive = data["IsActive"] ? 'checked' : '';
        let $index = parseInt($form.attr("data-index"));
        let isMain = data["IsMain"] ? 'checked' : '';
        let $tbody = $(".addressTB");
        if ($index >= 0) {
            jsonCustomerAddress[$index].addressArabicName = data["AddressArabicName"];
            jsonCustomerAddress[$index].addressEnglishName = data["AddressEnglishName"];
            jsonCustomerAddress[$index].cityId = data["CityId"];
            jsonCustomerAddress[$index].districtId = data["DistrictId"];
            jsonCustomerAddress[$index].location = data["Location"];
            jsonCustomerAddress[$index].isActive = data["IsActive"];
            jsonCustomerAddress[$index].isMain = data["IsMain"];
            jsonCustomerAddress[$index].address = data["Address"];
            jsonCustomerAddress[$index].isChanged = true;
            let $row = $tbody.find("tr:eq(" + $index + ")");
            $row.find("#name").text(name);
            $row.find("#cityName").text(cityName);
            $row.find("#districtName").text(districtName);
            $row.find("#address").text(data["Address"]);
            $row.find("#isActive").prop("checked", data["IsActive"]);
            $row.find("#isMain").prop("checked", data["IsMain"]);
        }
        else {
            let $row = `<tr> 
            <td id="name">` + name + `</td> 
            <td  id="cityName">` + cityName + `</td> 
            <td  id="districtName">` + districtName + `</td>
            <td  id="address">` + data["Address"] + `</td>
            <td> <input class="styled-checkbox" type="checkbox" id="isMain" disabled ` + isMain + ` />
            <label for="styled-checkbox-2"></label> </td>
            <td class="button">
            <button type="button" class="btn btn-sp btn-primary" onclick="ModalCustomerAddress(this)"> 
            <i class="fas fa-pencil-alt"></i><span class="text"> ` + Edit + ` </span></button>
            <button type="button" class="btn btn-sp btn-danger" onclick="deleteAddress(this)">
            <i class="far fa-trash-alt"></i></button>
            </td> 
            </tr>`;
            $tbody.append($row);
            var obj = { id: 0, addressArabicName: data["AddressArabicName"], addressEnglishName: data["AddressEnglishName"], cityId: data["CityId"], districtId: data["DistrictId"], location: data["Location"], isActive: data["IsActive"], isMain: data["IsMain"], address: data["Address"], isDeleted: false, isChanged: true };
            jsonCustomerAddress.push(obj);
        }
        let ModalDistricts = $("#ModalCustomerAddress");
        ModalDistricts.modal('hide');
    }
}
function deleteAddress($this) {
    let $row = $($this).closest("tr");
    let $index = $row.index();
    let obj = jsonCustomerAddress[$index];
    if (obj["id"] == 0) {
        jsonCustomerAddress.splice($index, 1);
    }
    else {
        jsonCustomerAddress[$index].isDeleted = true;
        jsonCustomerAddress[$index].isChanged = true;   
    }
    $row.remove();
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
            infowindowContent.children['place-name'].textContent = place.name;
            infowindowContent.children['place-address'].textContent =
                place.formatted_address;
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
    debugger;
    $(".XY_Position").val(myLocation.lat() + "," + myLocation.lng());
}
