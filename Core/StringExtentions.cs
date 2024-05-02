using Core.Enums;

namespace Core;

public static class StringExtentions
{
    public static Symboles GetSymboleFromString(this string symboleString)
    {
        if (Symboles.Add.GetSymboleString().Equals(symboleString))
        {
            return Symboles.Add;
        }

        if (Symboles.Subtract.GetSymboleString().Equals(symboleString))
        {
            return Symboles.Subtract;
        }

        if (Symboles.Multiply.GetSymboleString().Equals(symboleString))
        {
            return Symboles.Multiply;
        }

        if (Symboles.Divide.GetSymboleString().Equals(symboleString))
        {
            return Symboles.Divide;
        }

        if (Symboles.Exponent.GetSymboleString().Equals(symboleString))
        {
            return Symboles.Exponent;
        }

        if (Symboles.RightParenthesis.GetSymboleString().Equals(symboleString))
        {
            return Symboles.RightParenthesis;
        }

        if (Symboles.LeftParenthesis.GetSymboleString().Equals(symboleString))
        {
            return Symboles.LeftParenthesis;
        }


        return Symboles.Unknown;
    }
}
