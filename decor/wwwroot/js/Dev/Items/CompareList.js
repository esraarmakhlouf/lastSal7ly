function RemoveCompareListItem($this, itemId) {
    $.get("/Items/RemoveCompareItem", { itemId: itemId },
        function (response) {
            var msg = response.msg;
            if (response.isOk) {
                ShowNotification('success', msg);
                window.location.reload();
            }
            else { ShowNotification('error', msg); }
        });
}