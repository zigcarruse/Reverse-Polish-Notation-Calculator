using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Exceptions;
using Core.Services;
using ReversePolish.Resources;

namespace ReversePolish
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _formula = string.Empty;
        [ObservableProperty]
        private string _resultReversePolishText = string.Empty;

        private readonly IPlatformUtil _platformUtil;
        private readonly IReversePolishService _reversePolishService;

        public MainPageViewModel(
            IPlatformUtil platformUtil,
            IReversePolishService reversePolishService)
        {
            _platformUtil = platformUtil;
            _reversePolishService = reversePolishService;
        }

        [RelayCommand]
        private async Task ConvertToReversePolish()
        {
            try
            {
                ResultReversePolishText = string.Format(LocalizedStrings.Result_PARM, _reversePolishService.ConvertFormulaIntoReversePolish(Formula));
            }
            catch (NoMatchingLeftParenthesisException)
            {
                await _platformUtil.ShowDisplayAlert(
                    LocalizedStrings.Syntax_Error,
                    LocalizedStrings.No_matching_left_parenthesis_please_fix_and_try_again,
                    LocalizedStrings.Okay);
            }
            catch (NoMatchingRightParenthesisException)
            {
                await _platformUtil.ShowDisplayAlert(
                    LocalizedStrings.Syntax_Error,
                    LocalizedStrings.No_matching_right_parenthesis_please_fix_and_try_again,
                    LocalizedStrings.Okay);
            }
            catch (UnknownSymbolException e)
            {
                await _platformUtil.ShowDisplayAlert(
                    LocalizedStrings.Syntax_Error,
                    string.Format(LocalizedStrings.Unknown_Symbol_PARAM_Please_Remove, e.Symbole),
                    LocalizedStrings.Okay);
            }
            catch (InvalidFormulaException)
            {
                await _platformUtil.ShowDisplayAlert(
                    LocalizedStrings.Invalid_Formula,
                    LocalizedStrings.What_you_have_entered_is_invalid_please_try_again,
                    LocalizedStrings.Okay);
            }
            catch (Exception e)
            {
                await _platformUtil.ShowDisplayAlert(
                    LocalizedStrings.Something_went_wrong,
                    string.Format(LocalizedStrings.Something_went_wrong_PARAM, e.InnerException),
                    LocalizedStrings.Okay);
            }
        }
    }
}
