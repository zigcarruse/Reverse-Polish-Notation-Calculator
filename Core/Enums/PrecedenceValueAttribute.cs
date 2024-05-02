namespace Core.Enums;

[AttributeUsage(AttributeTargets.Field)]
public sealed class PrecedenceValueAttribute : Attribute
{
    public PrecedenceValueAttribute(int value)
    {
        Value = value;
    }

    public int Value { get; }
}
