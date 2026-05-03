using System;
using System.ComponentModel;
using System.Reflection;

namespace Domain.Enums
{
    public enum Gender
    {
        [Description("Male Gender")]
        Male,

        [Description("Female Gender")]
        Female,

        [Description("Other Gender")]
        Other
    }

    //public static class GenderExtensions
    //{
    //    public static string GetDescription(this Gender gender)
    //    {
    //        FieldInfo fi = gender.GetType().GetField(gender.ToString());
    //        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
    //        return attributes.Length > 0 ? attributes[0].Description : gender.ToString();
    //    }
    //}
}