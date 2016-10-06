alert();
function disable() {
    $(".confirm-password").click(function () { alert(); })
    $(".confirm-password").prop("disabled", true);
}

disable();
