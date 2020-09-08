using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sal7lyAdmin
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers = "", string actions = "", string cssClass = "active")
        {
            string currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            string currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            if (string.IsNullOrEmpty(actions))
                actions = currentAction;

            if (string.IsNullOrEmpty(controllers))
                controllers = currentController;

            string[] acceptedActions = actions.Trim().Split(',').Distinct().ToArray();
            string[] acceptedControllers = controllers.Trim().Split(',').Distinct().ToArray();

            if (acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController))
            { return cssClass; }

            return string.Empty;
        }
    }
}