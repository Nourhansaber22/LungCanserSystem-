using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string ToFriendlyString(this Enum value)
    {
        var attribute = value.GetType()
            .GetField(value.ToString())?
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault() as DescriptionAttribute;

        return attribute?.Description ?? value.ToString();
    }
}