$(document).ready(function () {
    $('#home_slider').owlCarousel({
        loop:true,
        margin:30,
        nav:false,
        dots: true,
        items: 1,
    });
});
$(document).ready(function () {
    $('#client_slider').owlCarousel({
        loop:true,
        margin:30,
        nav:false,
        dots: true,
        items: 1,
    });
});

function navToggle() {
    var element = document.getElementById("myNav");
    element.classList.toggle("nav_bar_open");
    var element = document.getElementById("navbar_toggle");
    element.classList.toggle("fixed_bu");
};

$(window).on('load', function () {

    "use strict";
    
    $(".loading-overlay .all-spi").fadeOut(1000, function () {
    
        $("body").css("overflow-y", "auto");
    
        $(this).parent().fadeOut(1000, function () {
            $(this).remove();
        });
    });
});