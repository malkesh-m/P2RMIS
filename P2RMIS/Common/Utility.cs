using System;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Collections.Generic;

namespace Sra.P2rmis.Web.Common
{
    public class Utility
    {
        public const string SYSTEM_DATE_FORMAT = "dd MMM yyyy  hh:mm:ss tt";
        /// <summary>
        /// Gets the current server information from the web.config and builds a URL
        /// <scheme>://<host>/<path>
        /// </summary>
        /// <returns>appPath - URL of current server</returns>
        public static string GetHostURL()
        {
            //get the host name of the server
            string host = ConfigurationManager.AppSettings["url-host"];
            //get the first part of url (http or https)
            string scheme = ConfigurationManager.AppSettings["url-scheme"];
            //get the port
            string port = ConfigurationManager.AppSettings["url-port"];


            var appPath = string.Empty;
            //build the URL format
            appPath = string.Format("{0}{1}", scheme, host);

            return appPath;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userident"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        /// <remarks>This does not appear to be referenced</remarks>
        public static string GetFormattedDateTime(DateTime? dt)
        {
            string retStr = "";
            if (dt != null)
            {
                DateTime dateVal = (DateTime)dt;
                retStr = dateVal.ToString(SYSTEM_DATE_FORMAT);
            }
            return retStr;
        }
        #region Razor Generic Dropdown Lists
        /// <summary>
        /// Generic dropdown containing Yes/No values
        /// </summary>
        /// <returns></returns>
        public static SelectList YesNoDropdown => new SelectList(new List<SelectListItem>
        {
                new SelectListItem{ Text="Yes", Value="true" },
                new SelectListItem{ Text="No", Value="false" }
        });
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>This class does not appear to be referenced</remarks>
    public class IntArrayModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null || string.IsNullOrEmpty(value.AttemptedValue))
            {
                return null;
            }

            return value
                .AttemptedValue
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>This class does not appear to be referenced</remarks>
    public static class HtmlExtensions
    {
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression
        ) //where TModel : IEnumConstraint  //Enum constraint - only can be used with Enums //If this needs to be constrained to Enum use UnconstrainedMelody - will need source code
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (var name in names)
            {

                var description = name;

                var memInfo = metaData.ModelType.GetMember(name);
                if (memInfo != null)
                {
                    var attributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                        description = ((DisplayAttribute)attributes[0]).Name;
                }
                var id = string.Format(
                    "{0}_{1}_{2}",
                    htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                    metaData.PropertyName,
                    name
                );

                var radio = htmlHelper.RadioButtonFor(expression, name, new { id = id }).ToHtmlString();
                sb.AppendFormat(
                    "<label for=\"{0}\">{1}</label> {2}",
                    id,
                    radio,
                    HttpUtility.HtmlEncode(description)
                );
                sb.Append("<br/>");
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }

}
