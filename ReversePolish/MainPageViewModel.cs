using System.Windows.Input;

namespace ReversePolish
{
    public partial class MainPageViewModel : BindableObject
    {
        public string Formula { get; set; }
        public string ResultReversePolishText { get; set; }
        public ICommand ConvertToReversePolishTapped => new Command(async () =>
        {
            await ConvertToReversePolishTappedCommand();
        });

        private readonly IPlatformUtil _platformUtil;
        private readonly IReversePolishService _reversePolishService;

        public MainPageViewModel(
            IPlatformUtil platformUtil,
            IReversePolishService reversePolishService)
        {
            _platformUtil = platformUtil;
            _reversePolishService = reversePolishService;
        }

        private async Task ConvertToReversePolishTappedCommand()
        {
            try
            {
                ResultReversePolishText = string.Format(LocalizedStrings.Result_PARM, _reversePolishService.ConvertFormulaIntoReversePolish(Formula));
                OnPropertyChanged(nameof(ResultReversePolishText));
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
