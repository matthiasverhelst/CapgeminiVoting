$("#Email").focusout(function () {
    if ($("#Email").val().indexOf("@") == -1)
    {
        $("#Email").val($("#Email").val() + "@capgemini.com");
    }
});