﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace sal7lyAdmin
{
    public static class ViewToStringRenderer
    {
    //    public static string RenderViewToString<TModel>(ControllerContext controllerContext, string viewName, TModel model)
    //    {
    //        ViewEngineResult viewEngineResult = ViewEngines.Engines.FindView(controllerContext, viewName, null);
    //        if (viewEngineResult.View == null)
    //        {
    //            throw new Exception("Could not find the View file. Searched locations:\r\n" + viewEngineResult.SearchedLocations);
    //        }
    //        else
    //        {
    //            IView view = viewEngineResult.View;

    //            using (var stringWriter = new StringWriter())
    //            {
    //                var viewContext = new ViewContext(controllerContext, view, new ViewDataDictionary<TModel>(model), new TempDataDictionary(), stringWriter);
    //                view.Render(viewContext, stringWriter);

    //                return stringWriter.ToString();
    //            }
    //        }
    //    }
    }
}
