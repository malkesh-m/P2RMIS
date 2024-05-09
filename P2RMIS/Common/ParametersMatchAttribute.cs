﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

//Code from http://blog.abodit.com/2010/02/asp-net-mvc-ambiguous-match/
namespace Sra.P2rmis.Web.Common
{
    
    //And finally, here’s the code that makes that possible: an attribute you can apply to a method parameter to indicate that you want 
    //it in the posted form, and an action filter that filters out any action methods that don’t match.

    //With this in place you can (i) avoid an unnecessary redirect and (ii) have actions with the same name but with different parameters.
    //Create a new ActionFilter like this …
    
     /// <summary>
     /// This attribute can be placed on a parameter of an action method that should be present on the URL in route data
     /// </summary>
     [AttributeUsage(AttributeTargets.Parameter, AllowMultiple=false)]
     public sealed class RouteValueAttribute : Attribute
     {
        public RouteValueAttribute() { }
     }

     /// <summary>
     /// This attribute can be placed on a parameter of an action method that should be present in FormData
     /// </summary>
     [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
     public sealed class FormValueAttribute : Attribute
     {
        public FormValueAttribute() { }
     }


     /// <summary>
     /// Parameters Match Attribute allows you to specify that an action is only valid
     /// if it has the right number of parameters marked [RouteValue] or [FormValue] that match with the form data or route data
     /// </summary>
     /// <remarks>
     /// This attribute allows you to have two actions with the SAME name distinguished by the values they accept according to the
     /// name of those values.  Does NOT handle complex types and bindings yet but could be easily adapted to do so.
     /// </remarks>
     [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
     public sealed class ParametersMatchAttribute : ActionMethodSelectorAttribute
     {
         public ParametersMatchAttribute() { }

         public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
         {
             // The Route values
             List<string> requestRouteValuesKeys = controllerContext.RouteData.Values.Where(v => !(v.Key == "controller" || v.Key == "action" || v.Key == "area")).Select(rv => rv.Key).ToList();

             // The Form values
             var form = controllerContext.HttpContext.Request.Form;
             List<string> requestFormValuesKeys = form.AllKeys.ToList();

             // The parameters this method expects
             var parameters = methodInfo.GetParameters();

             // Parameters from the method that we haven’t matched up against yet
             var parametersNotMatched = parameters.ToList();

             // each parameter of the method can be marked as a [RouteValue] or [FormValue] or both or nothing
             foreach (var param in parameters)
             {
                 string name = param.Name;

                 bool isRouteParam = param.GetCustomAttributes(true).Any(a => a is RouteValueAttribute);
                 bool isFormParam = param.GetCustomAttributes(true).Any(a => a is FormValueAttribute);

                 if (isRouteParam && requestRouteValuesKeys.Contains(name))
                 {
                     // Route value matches parameter
                     requestRouteValuesKeys.Remove(name);
                     parametersNotMatched.Remove(param);
                 }
                 else if (isFormParam && requestFormValuesKeys.Contains(name))
                 {
                     // Form value matches method parameter
                     requestFormValuesKeys.Remove(name);
                     parametersNotMatched.Remove(param);
                 }
                 else
                 {
                     // methodInfo parameter does not match a route value or a form value
                     Debug.WriteLine(methodInfo + " failed to match " + param + "against either a RouteValue or a FormValue");
                     return false;
                 }
             }

             // Having removed all the parameters of the method that are matched by either a route value or a form value
             // we are now left with all the parameters that do not match and all the route and form values that were not used

             if (parametersNotMatched.Count >0)
             {
                 Debug.WriteLine(methodInfo + " – FAIL: has parameters left over not matched by route or form values");
                 return false;
             }

             if (requestRouteValuesKeys.Count >0)
             {
                 Debug.WriteLine(methodInfo + " – FAIL: Request has route values left that aren’t consumed");
                 return false;
             }

             if (requestFormValuesKeys.Count >1)
             {
                 Debug.WriteLine(methodInfo + " – FAIL : unmatched form values "+ string.Join(",",requestFormValuesKeys.ToArray()));
                 return false;
             }

             Debug.WriteLine(methodInfo + " – PASS – unmatched form values " + string.Join(", ", requestFormValuesKeys.ToArray()));
             return true;
         }
     }
}
