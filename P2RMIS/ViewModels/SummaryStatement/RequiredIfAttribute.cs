using System;
using System.ComponentModel.DataAnnotations;


namespace Sra.P2rmis.Web.ViewModels.SummaryStatement
{
    public class RequiredIfAttribute : RequiredAttribute
    {
        private String PropertyName { get; set; }
        private Object DesiredValue { get; set; }

        public RequiredIfAttribute(String propertyName, Object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString().ToLower() == DesiredValue.ToString().ToLower())
            {
                ValidationResult result = base.IsValid(value, context);
                return result;
            }
            return ValidationResult.Success;
        }
    }
}