using Core.Exceptions;
using Core.Services;

namespace Tests;

public class ReversePolishServiceTests
{
    private ReversePolishService CreateReversePolishService()
    {
        return new ReversePolishService();
    }

    [Fact]
    public void When_Formula_Is_Missing_Right_Parenthesis_Should_Throw_Exception()
    {
        bool caughtCorrectException = false;

        try
        {
            ReversePolishService reversePolishService = CreateReversePolishService();
            string result = reversePolishService.ConvertFormulaIntoReversePolish("1 + 2 + ( 3 + 4");
        }
        catch (NoMatchingRightParenthesisException)
        {
            caughtCorrectException = true;
        }
        catch (Exception) { }

        Assert.True(caughtCorrectException, "Should have gotten Right Parenthesis Exception");
    }

    [Fact]
    public void When_Formula_Is_Missing_Left_Parenthesis_Should_Throw_Exception()
    {
        bool caughtCorrectException = false;

        try
        {
            ReversePolishService reversePolishService = CreateReversePolishService();
            string result = reversePolishService.ConvertFormulaIntoReversePolish("1 + 2 + 3 + 4 )");
        }
        catch (NoMatchingLeftParenthesisException)
        {
            caughtCorrectException = true;
        }
        catch (Exception) { }

        Assert.True(caughtCorrectException, "Should have gotten Left Parenthesis Exception");
    }

    [Fact]
    public void When_Formula_Has_A_Unknown_Value_Should_Throw_Exception()
    {
        bool caughtCorrectException = false;

        try
        {
            ReversePolishService reversePolishService = CreateReversePolishService();
            string result = reversePolishService.ConvertFormulaIntoReversePolish("1 + x + 3 + 4 )");
        }
        catch (UnknownSymbolException e)
        {
            Assert.True(e.Symbole.Equals("x"), "Should say unknown symbol is x");
            caughtCorrectException = true;
        }
        catch (Exception) { }

        Assert.True(caughtCorrectException, "Should have gotten Unknown Symbol Exception");
    }

    [Fact]
    public void When_Formula_Is_Only_White_Space_Should_Throw_Exception()
    {
        bool caughtCorrectException = false;

        try
        {
            ReversePolishService reversePolishService = CreateReversePolishService();
            string result = reversePolishService.ConvertFormulaIntoReversePolish("     ");
        }
        catch (InvalidFormulaException)
        {
            caughtCorrectException = true;
        }
        catch (Exception) { }

        Assert.True(caughtCorrectException, "Should have gotten Invalid Formula Exception");
    }

    [Fact]
    public void When_Formula_Has_Last_Symbole_With_Lower_Precedence_Should_Keep_Lowest_Precedence_Symbole_At_End_Of_Result()
    {
        bool caughtCorrectException = false;

        try
        {
            ReversePolishService reversePolishService = CreateReversePolishService();
            string result = reversePolishService.ConvertFormulaIntoReversePolish("2 * 3 + 5");

            Assert.True(result.Equals("2 3 * 5 +"), "Result should be 2 3 * 5 +");
        }
        catch (Exception)
        {
            caughtCorrectException = true;
        }

        Assert.False(caughtCorrectException, "Should not have gotten Exception");
    }

    [Fact]
    public void When_Formula_Has_Parenthesis_Should_Show_That_Result_Alone_Followed_By_Symboles_Before()
    {
        bool caughtCorrectException = false;

        try
        {
            ReversePolishService reversePolishService = CreateReversePolishService();
            string result = reversePolishService.ConvertFormulaIntoReversePolish("2 * ( 2 * 3 + 5 )");

            Assert.True(result.Equals("2 2 3 * 5 + *"), "Result should be 2 2 3 * 5 + *");
        }
        catch (Exception)
        {
            caughtCorrectException = true;
        }

        Assert.False(caughtCorrectException, "Should not have gotten Exception");
    }
}