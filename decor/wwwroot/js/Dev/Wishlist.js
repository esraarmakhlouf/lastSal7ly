function RemoveWishlistItem($this, itemId) {
    $.get("/Account/RemoveWishlistItem", { itemId: itemId }, function (response) {
        var msg = response.msg;
        if (response.isOk) {
            ShowNotification('success', msg);
            $($this).closest("tr").remove();
        }
        else { ShowNotification('error', msg); }
    });
}