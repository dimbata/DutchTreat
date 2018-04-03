$(document).ready(function() {

    var $theForm = $("#theForm");
    $theForm.hide();

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click",
        function () {
            $popupForm.fadeToggle(700);
        });

});