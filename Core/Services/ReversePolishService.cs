using Core.Enums;
using Core.Exceptions;

namespace Core.Services;

public class ReversePolishService : IReversePolishService
{
    public string ConvertFormulaIntoReversePolish(string formula)
    {
        string reversePolish = string.Empty;

        if (!string.IsNullOrWhiteSpace(formula))
        {
            string[] symboles = formula.Trim().Split(' ');
            Stack<Symboles> symboleStack = new Stack<Symboles>();

            foreach (string symbole in symboles)
            {
                bool isNumber = int.TryParse(symbole, out int result);
                if (isNumber)
                {
                    reversePolish += symbole + " ";
                }
                else if (symbole.GetSymboleFromString().Equals(Symboles.Add)
                    || symbole.GetSymboleFromString().Equals(Symboles.Subtract)
                    || symbole.GetSymboleFromString().Equals(Symboles.Multiply)
                    || symbole.GetSymboleFromString().Equals(Symboles.Divide)
                    || symbole.GetSymboleFromString().Equals(Symboles.Exponent))
                {
                    reversePolish += GetSymbolesWithLowerOrEqualPrecedence(ref symboleStack, symbole);
                    symboleStack.Push(symbole.GetSymboleFromString());
                }
                else if (symbole.GetSymboleFromString().Equals(Symboles.LeftParenthesis))
                {
                    symboleStack.Push(Symboles.LeftParenthesis);
                }
                else if (symbole.GetSymboleFromString().Equals(Symboles.RightParenthesis) && symboleStack.Any())
                {
                    reversePolish += GetSymbolesBetweenParenthesis(ref symboleStack);
                }
                else
                {
                    throw new UnknownSymbolException()
                    {
                        Symbole = symbole
                    };
                }
            }

            reversePolish += GetRemainingSymboles(ref symboleStack);
        }
        else
        {
            throw new InvalidFormulaException();
        }

        return reversePolish.Trim();
    }

    public string GetSymbolesWithLowerOrEqualPrecedence(ref Stack<Symboles> symboleStack, string symbole)
    {
        string symbolesWithLowerOrEqualPrecedence = string.Empty;

        Symboles currentSymbole = symbole.GetSymboleFromString();

        while (symboleStack.Any())
        {
            Symboles previousSymbole = symboleStack.Peek();

            if (previousSymbole.Equals(Symboles.RightParenthesis) || previousSymbole.Equals(Symboles.LeftParenthesis))
            {
                break;
            }

            bool currentSymboleHasPrecedence = currentSymbole.GetSymbolePrecedence() < previousSymbole.GetSymbolePrecedence();
            bool currentSymboleIsExponent = currentSymbole.Equals(Symboles.Exponent);
            bool hasSamePrecedence = currentSymbole.GetSymbolePrecedence() == previousSymbole.GetSymbolePrecedence();

            if (currentSymboleHasPrecedence || (!currentSymboleIsExponent && (currentSymboleHasPrecedence || hasSamePrecedence)))
            {
                symbolesWithLowerOrEqualPrecedence += symboleStack.Pop().GetSymboleString() + " ";
            }
            else
            {
                break;
            }
        }

        return symbolesWithLowerOrEqualPrecedence;
    }

    private string GetSymbolesBetweenParenthesis(ref Stack<Symboles> symboleStack)
    {
        string symbolesBetweenParenthesis = string.Empty;

        Symboles currentSymbole = symboleStack.Pop();
        while (symboleStack.Any() && !currentSymbole.Equals(Symboles.LeftParenthesis))
        {
            symbolesBetweenParenthesis += currentSymbole.GetSymboleString() + " ";

            if (symboleStack.Any())
            {
                currentSymbole = symboleStack.Pop();
            }
        }

        if (!currentSymbole.Equals(Symboles.LeftParenthesis))
        {
            throw new NoMatchingLeftParenthesisException();
        }

        return symbolesBetweenParenthesis;
    }

    private string GetRemainingSymboles(ref Stack<Symboles> symboleStack)
    {
        string remainingSymboles = string.Empty;

        while (symboleStack.Any())
        {
            Symboles currentSymbole = symboleStack.Pop();

            if (currentSymbole.Equals(Symboles.Add)
                || currentSymbole.Equals(Symboles.Subtract)
                || currentSymbole.Equals(Symboles.Multiply)
                || currentSymbole.Equals(Symboles.Divide)
                || currentSymbole.Equals(Symboles.Exponent))
            {
                remainingSymboles += currentSymbole.GetSymboleString() + " ";
            }
            else
            {
                throw new NoMatchingRightParenthesisException();
            }
        }

        return remainingSymboles;
    }
}
