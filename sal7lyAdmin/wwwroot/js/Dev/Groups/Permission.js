$(document).on("change", ".permission", function () {
    let $row = $(this).closest('.row');
    let $permissions = $row.find(".permission");
    let $selectedPermissions = $row.find(".permission:checked");
    $row.find(".selectall").prop("checked", false);
    if ($selectedPermissions.length == $permissions.length) { $row.find(".selectall").prop("checked", true); }
});
$(document).on("change", ".selectall", function () {
    let $row = $(this).closest('.row');
    let $permissions = $row.find(".permission");
    $permissions.prop("checked", $(this).prop("checked"));
});

function ModulePermissions(groupId,moduleId,$this) {
    $.get("/Groups/ModulePermissions/", { groupId: groupId, moduleId: moduleId, allow: $($this).prop("checked") }, function (response) {
        if (response) { ShowNotification('success', DataSavedSuccessfully); }
        else { ShowNotification('error', Erroroccuredtryagain); }
    });
}
function ResetGroupPermissions(groupId) {
    $.get("/Groups/ResetGroupPermissions/", { groupId: groupId }, function (response) {
        if (response) { ShowNotification('success', DataSavedSuccessfully); $(".modulePermissions").prop("checked",false); }
        else { ShowNotification('error', Erroroccuredtryagain); }
    });
}
function GiveGroupAllPermissions(groupId) {
    $.get("/Groups/GiveGroupAllPermissions/", { groupId: groupId }, function (response) {
        if (response) { ShowNotification('success', DataSavedSuccessfully); $(".modulePermissions").prop("checked",true); }
        else { ShowNotification('error', Erroroccuredtryagain); }
    });
}
function SetAllScreenPermissions(groupId, screenId, $this) {
    $.get("/Groups/SetAllScreenPermissions/", { groupId: groupId, screenId: screenId, allow: $($this).prop("checked") }, function (response) {
        if (response) { ShowNotification('success', DataSavedSuccessfully); }
        else { ShowNotification('error', Erroroccuredtryagain); }
    });
}
function SetPermission(groupId, screenId, permissionId, $this) {
    $.get("/Groups/SetPermission/", { groupId: groupId, screenId: screenId, permissionId: permissionId, allow: $($this).prop("checked") }, function (response) {
        if (response) { ShowNotification('success', DataSavedSuccessfully); }
        else { ShowNotification('error', Erroroccuredtryagain); }
    });
}
function Screens(moduleId, groupId) {
    let $modal = $("#PermissionsModal");
    let $modalBody = $modal.find(".modal-body");
    $modalBody.html("");
    $.get("/Groups/Screens/", { groupId: groupId, moduleId: moduleId }, function (response) {
        $modalBody.html(response);
        $modal.modal("show");
    });
}
