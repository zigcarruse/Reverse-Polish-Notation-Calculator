namespace Core.Enums;

public enum Symboles
{
    [StringValue("+")]
    [PrecedenceValue(1)]
    Add,
    [StringValue("-")]
    [PrecedenceValue(1)]
    Subtract,
    [StringValue("*")]
    [PrecedenceValue(2)]
    Multiply,
    [StringValue("/")]
    [PrecedenceValue(2)]
    Divide,
    [StringValue("^")]
    [PrecedenceValue(3)]
    Exponent,
    [StringValue(")")]
    RightParenthesis,
    [StringValue("(")]
    LeftParenthesis,
    Unknown
}
