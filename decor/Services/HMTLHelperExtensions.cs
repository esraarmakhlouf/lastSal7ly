using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Decor
{
    public static class HMTLHelperExtensions
    {
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers = "", string actions = "", string cssClass = " active open ")
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