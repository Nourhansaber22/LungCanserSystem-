using System;
using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string ToFriendlyString(this Enum value)
    {
        // نجلب FieldInfo الخاص بالقيمة
        FieldInfo fi = value.GetType().GetField(value.ToString());

        // نشوف لو في DescriptionAttribute
        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        // لو موجود Description نرجعه، وإلا نرجع الاسم نفسه
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}