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