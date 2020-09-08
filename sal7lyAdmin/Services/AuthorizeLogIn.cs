using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace sal7lyAdmin.Services
{
    /// <summary>
    ///  Custom  Authorize
    /// </summary>
    public class AuthorizeLogIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AppSession.CurrentUser == null)
                filterContext.Result = new RedirectResult("~/Account/LogIn");
        }
    }
}
