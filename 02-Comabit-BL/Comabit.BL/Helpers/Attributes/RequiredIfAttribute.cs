// <copyright file="RequiredIfAttribute.cs" company="mission-one">
//      Copyright (c) mission-one. All rights reserved.
// </copyright>

namespace Comabit.BL.Helpers.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RequiredIfAttribute : RequiredAttribute
    {
        private string PropertyName { get; set; }

        public RequiredIfAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            bool.TryParse(type.GetProperty(PropertyName).GetValue(instance)?.ToString(), out bool propertyValue);

            if (propertyValue && string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}