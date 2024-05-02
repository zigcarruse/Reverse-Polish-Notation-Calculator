using Core.Exceptions;
using Core.Services;
using Moq;
using ReversePolish;

namespace Tests;

public class MainPageViewModelTests
{
    private readonly Mock<IPlatformUtil> _mockPlatformUtil = new Mock<IPlatformUtil>();
    private readonly Mock<IReversePolishService> _mockReversePolishService = new Mock<IReversePolishService>();

    private MainPageViewModel CreateMainPageViewModel()
    {
        return new MainPageViewModel(
            _mockPlatformUtil.Object,
            _mockReversePolishService.Object);
    }

    [Fact]
    public void When_User_Enters_String_Missing_Left_Parenthesis_Should_Show_Error_Alert()
    {
        _ = _mockReversePolishService.Setup(x => x.ConvertFormulaIntoReversePolish(It.IsAny<string>())).Throws(new NoMatchingLeftParenthesisException());

        MainPageViewModel viewModel = CreateMainPageViewModel();
        viewModel.Formula = "1 + 2 )";

        viewModel.ConvertToReversePolishCommand.Execute(null);

        _mockPlatformUtil.Verify(x =>
            x.ShowDisplayAlert("Syntax Error", "No matching left parenthesis, please fix and try again.", "Okay"),
            Times.Once,
            "Should show error message saying no matching left parenthesis");
    }


    [Fact]
    public void When_User_Enters_String_Missing_Right_Parenthesis_Should_Show_Error_Alert()
    {
        _ = _mockReversePolishService.Setup(x => x.ConvertFormulaIntoReversePolish(It.IsAny<string>())).Throws(new NoMatchingRightParenthesisException());

        MainPageViewModel viewModel = CreateMainPageViewModel();
        viewModel.Formula = "1 + 2 ( 2 + 3";

        viewModel.ConvertToReversePolishCommand.Execute(null);

        _mockPlatformUtil.Verify(x =>
            x.ShowDisplayAlert("Syntax Error", "No matching right parenthesis, please fix and try again.", "Okay"),
            Times.Once,
            "Should show error message saying no matching right parenthesis");
    }

    [Fact]
    public void When_User_Enters_String_With_Unknown_Character_Should_Show_Error_Alert()
    {
        _ = _mockReversePolishService.Setup(x => x.ConvertFormulaIntoReversePolish(It.IsAny<string>())).Throws(new UnknownSymbolException()
        {
            Symbole = "a"
        });

        MainPageViewModel viewModel = CreateMainPageViewModel();
        viewModel.Formula = "1 + a";

        viewModel.ConvertToReversePolishCommand.Execute(null);

        _mockPlatformUtil.Verify(x =>
            x.ShowDisplayAlert("Syntax Error", "Unknown Symbol a, Please Remove.", "Okay"),
            Times.Once,
            "Should show error message saying unknown symbol");
    }

    [Fact]
    public void When_User_Enters_String_With_Only_White_Space_Should_Show_Error_Alert()
    {
        _ = _mockReversePolishService.Setup(x => x.ConvertFormulaIntoReversePolish(It.IsAny<string>())).Throws(new InvalidFormulaException());

        MainPageViewModel viewModel = CreateMainPageViewModel();
        viewModel.Formula = "      ";

        viewModel.ConvertToReversePolishCommand.Execute(null);

        _mockPlatformUtil.Verify(x =>
            x.ShowDisplayAlert("Invalid Formula", "What you have entered is invalid, Please try again.", "Okay"),
            Times.Once,
            "Should show error message saying Invalid Formula");
    }

    [Fact]
    public void When_A_Generic_Exception_Is_Thrown_Should_Show_Error_Alert()
    {
        _ = _mockReversePolishService.Setup(x => x.ConvertFormulaIntoReversePolish(It.IsAny<string>())).Throws(new Exception());

        MainPageViewModel viewModel = CreateMainPageViewModel();
        viewModel.Formula = "      ";

        viewModel.ConvertToReversePolishCommand.Execute(null);

        _mockPlatformUtil.Verify(x =>
            x.ShowDisplayAlert("Something went wrong", It.IsAny<string>(), "Okay"),
            Times.Once,
            "Should show error message saying Invalid Formula");
    }

    [Fact]
    public void When_User_Enters_A_Valid_Formula_Should_Display_Result()
    {
        _ = _mockReversePolishService.Setup(x => x.ConvertFormulaIntoReversePolish(It.IsAny<string>())).Returns("1 2 +");

        MainPageViewModel viewModel = CreateMainPageViewModel();
        viewModel.Formula = "1 + 2";

        viewModel.ConvertToReversePolishCommand.Execute(null);

        Assert.True(viewModel.ResultReversePolishText.Equals("Result: 1 2 +"), "Should show correct formula");
    }
}
