using System;
using System.Reflection;
using System.Web.Mvc;

namespace Sra.P2rmis.Web.Common
{
    /// <summary>
    /// Used to vary an action method based on which button in a form was pressed. This
    /// is useful but is an anti-pattern because it couples the controller to names
    /// used in the form elements. 
    /// </summary>
    /// <remarks>
    /// See the example at http://weblogs.asp.net/dfindley/archive/2009/05/31/asp-net-mvc-multiple-buttons-in-the-same-form.aspx
    /// Copied from http://stackoverflow.com/questions/5702549/asp-net-mvc3-razor-with-multiple-submit-buttons
    /// </remarks>
    public class AcceptButtonAttribute : ActionMethodSelectorAttribute
    {
        public string ButtonName { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var req = controllerContext.RequestContext.HttpContext.Request;
            return !string.IsNullOrEmpty(req.Form[this.ButtonName]);
        }
    }

    //This class fixes form posts when remote attribute is used to validate data
    //http://stackoverflow.com/questions/6172262/mvc3-remoteattribute-and-muliple-submit-buttons
    public class OnlyIfPostedFromButtonAttribute : ActionMethodSelectorAttribute
    {
        public String SubmitButton { get; set; }
        public String ViewModelSubmitButton { get; set; }

        public override Boolean IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var buttonName = controllerContext.HttpContext.Request[SubmitButton];
            if (buttonName == null)
            {
                //This is neccessary to support the RemoteAttribute that appears to intercepted the form post
                //and removes the submit button from the Request (normally detected in the code above)
                var viewModelSubmitButton = controllerContext.HttpContext.Request[ViewModelSubmitButton];
                if ((viewModelSubmitButton == null) || (viewModelSubmitButton != SubmitButton))
                    return false;
            }

            // Modify the requested action to the name of the method the attribute is attached to
            controllerContext.RouteData.Values["action"] = methodInfo.Name;
            return true;
        }
    }

}