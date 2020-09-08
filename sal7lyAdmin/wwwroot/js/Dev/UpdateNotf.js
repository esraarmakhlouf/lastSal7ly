"use strict";
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();
connection.start().then(function () {
    //startNotf();
}).catch(err => console.error(err.toString()));

connection.on("UpdateNotf", function (alerts) {

   // updateNotf(alerts);

});


function updateNotf(alerts) {

    var count = alerts.length;
    var ul = $("#ul-div");
    ul.empty();
    $('.toast').hide();
    var divider = `<li class="divider"></li>`;
    var li = ` <span class="dropdown-item dropdown-header"> الاشعارات</span>
        <div class="dropdown-divider" ></div >`;
    ul.append(li);
    ul.append(divider);
    $('#notif-count').text(alerts.filter(obj => !obj.isAlert).length);
    
    $.each(alerts.filter(obj => !obj.isAlert), function (key, item) {


        var date = dateToHowManyAgo(`${item.creationDate}`);
        li = `     <div class="media notification-list-item-maintenance" data-id='${item.id}' data-url='${item.uRl}'>
                            <img src="/img/notifCart.jpg" alt="User Avatar" class="img-size-50 img-circle mr-3">
                            <div class="media-body">

                                <p class="text-sm">${item.text}</p>
                                <p class="text-sm text-muted"><i class="far fa-clock mr-1"></i> ${date}</p>
                            </div>
                        </div>`
        ul.append(li);
        ul.append(divider);
    });
    $.each(alerts.filter(obj => obj.isAlert), function (key, item) {

        createAlet("Alert", item.text, item.id, item.uRl)

    });


}
$("#ul-div").on("click", ".notification-list-item-maintenance", function () {
    
    $(this).next().remove();
    $(this).remove();

    var count = $("#noft-count").val();
    count = count - 1;
    if (count == 0) {
        $("#noft-count").css("display", "none");

    } else {
        $("#noft-count").text(count);
        $("#noft-count").css("display", "block");


    }
    let id = $(this).attr("data-id");
    let url = $(this).attr("data-url");
    updateNotifcationListItem(id, url);

});

$(".Alert-body").on("click",  function () {

    let id = $(this).attr("data-id");
    let url = $(this).attr("data-url");
    updateNotifcationListItem(id, url);

});
function updateNotifcationListItem(id, uRl) {
    $.get("/SendData/updateNotf/" + id, function (data) { 
        window.location.href = `${uRl}`

    });

}
function startNotf() {
    $.get("/SendData/Index", function () {
       
    });
    }

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


function   bvrrvfc(date) {
    let seconds = Math.floor((new Date() - date) / 1000);
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