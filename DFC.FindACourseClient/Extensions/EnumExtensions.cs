using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DFC.FindACourseClient.Extensions
{
    public static class EnumExtensions
    {
        public static string GetFriendlyName(this Enum value)
        {
            return value
                       .GetType()
                       .GetMember(value.ToString())
                       .FirstOrDefault()
                       ?.GetCustomAttribute<DescriptionAttribute>()
                       ?.Description
                   ?? value.ToString();
        }
    }
}