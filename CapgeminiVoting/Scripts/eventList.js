function lockOrUnlockEvent(id, locked)
{
    $.post(viewBag.lockLink, { id: id, lockEvent: locked }, function (result) {
        if (result == true)
        {
            if (locked == true) {
                $("#lockImage" + id).attr("src", "/Images/locked.png");
                $("#lockImage" + id).attr("onclick", "lockOrUnlockEvent(" + id + ", false)");
            }

            else {
                $("#lockImage" + id).attr("src", "/Images/unlocked.png");
                $("#lockImage" + id).attr("onclick", "lockOrUnlockEvent(" + id + ", true)");
            }
        }
    });
}

function deleteEventDialog(id) {
    var dialog = $("#deleteDialog");
    dialog.html("Are you sure you want to delete this event?");

    dialog.dialog({
        dialogClass: "noclose",
        resizable: false,
        modal: true,
        title: "Delete event",
        height: 150,
        width: 400,
        buttons: {
            "Yes": function () {
                $(this).dialog('close');
                $.ajax({
                    url: "/Admin/DeleteEvent/" + id,
                    method: 'POST',
                    success: function () {
                        location.reload();
                    }
                });
            },
            "No": function () {
                $(this).dialog('close');
            }
        }
    });
}