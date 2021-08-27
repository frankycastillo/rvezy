using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace rvezy.Extensions
{
    public static class EnumExtensions
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T GetRandomValue<T>()
        {
            var values = GetValues<T>().ToArray();
            var rnd = new Random();
            var index = rnd.Next(0, values.Length);
            return values[index];
        }

        public static string GetName<T>(this T value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static int GetHashCode<T>(this T value)
        {
            return GetName(value).GetHashCode();
        }

        public static string ToEnumStringFromAttributes<T>(this T type)
        {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[]) enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        public static T ToEnumFromAttributes<T>(this string value, T defaultValue) where T : struct, IConvertible
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[]) enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == value)
                {
                    return (T) Enum.Parse(enumType, name);
                }
            }

            return defaultValue;
        }

        public static IEnumerable<string> GetAsStrings<T>(this IEnumerable<T> values)
        {
            return values.Select(item => item.GetType().IsEnum ? item.GetName() : item.ToString()).ToList();
        }

        public static T ToEnum<T>(this string value, T defaultValue) where T : struct, IConvertible
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException($"{nameof(T)} must be an enumerated type");
            }

            return Enum.TryParse(value, true, out T response) ? response : defaultValue;
        }

        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
        
        public static string GetDisplayValue(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            // if (descriptionAttributes[0].ResourceType != null)
            //     return lookupResource(descriptionAttributes[0].ResourceType, descriptionAttributes[0].Name);

            if (descriptionAttributes == null) return string.Empty;
            return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
        }
    }
}