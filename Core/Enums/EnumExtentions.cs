using System.Reflection;

namespace Core.Enums;

public static class EnumExtentions
{
    public static string StringValue<T>(this T value) where T : Enum
    {
        string fieldName = value.ToString();
        FieldInfo? field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        return field?.GetCustomAttribute<StringValueAttribute>()?.Value ?? fieldName;
    }

    public static int PrecedenceValue<T>(this T value) where T : Enum
    {
        string fieldName = value.ToString();
        FieldInfo? field = typeof(T).GetField(fieldName, BindingFlags.Public | BindingFlags.Static);
        return field?.GetCustomAttribute<PrecedenceValueAttribute>()?.Value ?? -1;
    }

    public static string GetSymboleString(this Symboles symbole)
    {
        Type type = symbole.GetType();
        MemberInfo[] memInfo = type.GetMember(symbole.ToString());
        object[] attributes = memInfo[0].GetCustomAttributes(typeof(StringValueAttribute), false);
        return (attributes.Length > 0) ? ((StringValueAttribute)attributes[0]).Value : string.Empty;
    }

    public static int GetSymbolePrecedence(this Symboles symbole)
    {
        Type type = symbole.GetType();
        MemberInfo[] memInfo = type.GetMember(symbole.ToString());
        object[] attributes = memInfo[0].GetCustomAttributes(typeof(PrecedenceValueAttribute), false);
        return (attributes.Length > 0) ? ((PrecedenceValueAttribute)attributes[0]).Value : -1;
    }
}
