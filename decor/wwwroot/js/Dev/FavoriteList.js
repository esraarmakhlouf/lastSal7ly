function RemoveFavoriteListItem($this, itemId) {
    $.get("/Account/RemoveFavoriteListItem", { itemId: itemId }, function (response) {
        var msg = response.msg;
        if (response.isOk) {
            ShowNotification('success', msg);
            $($this).closest("tr").remove();
        }
        else { ShowNotification('error', msg); }
    });
}