
function UserOrders(UserId) {
    debugger
    $.get("/Order/UserOrders", { UserId: UserId }, function (response) {
        var strStatus = response.status;
        
        if (strStatus == "success") {
            var orders = response.orders;
            var body = `
     <table class="table table-striped table-bordered data-table">
        <thead>
            <tr>
                <th>الكود</th>
                <th>Customer</th>
                <th class="no-sort">نشط</th>
                <th class="no-sort"></th>
            </tr>
        </thead>
        <tbody>
            @foreach (var item in orders)
            {
            <tr>
                <td>@item.Code</td>
                <td>@item.Customer.ArabicName</td>

                <td>
                    <input class="styled-checkbox" type="checkbox" id="styled-checkbox-2" disabled @(item.IsActive ? "checked" : "") />
                    <label for="styled-checkbox-2"></label>
                </td>

                <td class="button">
                    @if (UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.Edit))
                    {
                        <a class="btn btn-outline-primary" href="/Users/OpenForm/@item.Id">
                            <i class="fas fa-pencil-alt"></i>
                            <span class="text"> Accept </span>
                        </a>
                    }

                    @if (UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.Delete))
                    {
                        <button type="button" class="btn btn-sp btn-outline-danger" onclick="ShowDeleteAlert('Users','@item.Id')">
                            <i class="far fa-trash-alt"></i>
                            <span class="text"> Reject </span>
                        </button>
                    }
                    @if (UserAccountMannager.HasPermission(_uow, _security, AppSession.CurrentUser.Id, EN_Screens.Users, EN_Permissions.View) && AppSession.CurrentUser.Id == 2)
                    {
                        <a class="btn btn-outline-primary" href="/Users/Orders/@item.Id">
                            <i class="fas fa-pencil-alt"></i>
                            <span class="text"> Complete </span>
                        </a>
                    }
                </td>
            </tr>
            }
        </tbody>
    </table>`;
            ShowModalForm("lg", UserOrders, body);
        }
        else {
            ShowNotification('error', Erroroccuredtryagain);
        }
    });
}
