using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Comabit.BL.Helpers.Enumerations
{
    public static class EnumerationExtensions
    {
        //checks if the value contains the provided type
        public static bool Has<T>(this System.Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        //checks if the value is only the provided type
        public static bool Is<T>(this System.Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        //appends a value
        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        public static bool IsFlagSet<T>(this System.Enum type, T value)
        {
            try
            {
                return ((int)(object)type & (int)(object)value) != 0;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        //completely removes the value
        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                        ), ex);
            }
        }

        public static string GetDescription(this Enum value)
        {
            if (value.GetType().IsDefined(attributeType: typeof(FlagsAttribute), inherit: false))
            {
                return GetDescriptionForEnumFlags(value);
            }
            else
            {
                return GetDescriptionForEnum(value);
            }
        }
        
        /// <summary>
        /// Gets the description for a normal, non-flag enum.
        /// </summary>
        /// <param name="value">The description value.</param>
        /// <returns></returns>
        public static string GetDescriptionForEnum(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets the description for enum flags.
        /// </summary>
        /// <param name="value">Comma separated List of Enum Values.</param>
        /// <returns></returns>
        public static string GetDescriptionForEnumFlags(this Enum value)
        {
            DescriptionAttribute attr;
            List<string> descriptions = new List<string>();

            foreach (var item in Enum.GetValues(value.GetType()).Cast<Enum>())
            {
                Type type = item.GetType();
                string name = Enum.GetName(type, item);

                if (name != null && value.IsFlagSet(item))
                {
                    FieldInfo field = type.GetField(name);
                    if (field != null)
                    {
                        attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                        if (attr != null)
                        {
                            descriptions.Add(attr.Description);
                        }
                        else
                        {
                            descriptions.Add(name);
                        }
                    }
                }
            }

            return String.Join(", ", descriptions.ToArray());
        }

        public static string GetDisplayName(this Enum value)
        {
            return value.GetAttribute<DisplayAttribute>().GetName();
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting 
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }
    }
}