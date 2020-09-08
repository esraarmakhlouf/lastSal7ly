$(document).on("change", "#DateRange", function () {
    
    let $this = $("#DateRange");
    console.log($this.val());
    if ($this.val() === "previous x days") {
        $("#previous").css("display", "block");
        console.log("previous block is displayed");
        $("#custom").hide();
    }
    else if ($this.val() === "Custom") {
        $("#custom").show();
        $("#previous").hide();
    }
    else {
        $("#previous").hide();
        $("#custom").hide();
    }
});


$(document).ready(function () {


    $('#searchButton').click(function () {
        $('.showSearch').toggle();
        //$('.showSearch').css("display", "block");
        //$('#searchButton').css("display", "none");
    });
  
    function selectAll(checkId, listId) {
        if ($(`#${checkId}`).is(':checked')) {
            $(`#${listId} > option`).prop("selected", "selected");// Select All Options
            $(`#${listId}`).trigger("change");// Trigger change to select 2
        } else {
            $(`#${listId} > option`).removeAttr("selected");
            $(`#${listId}`).trigger("change");// Trigger change to select 2
        }
    }
});
