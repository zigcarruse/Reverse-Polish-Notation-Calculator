namespace ReversePolish;

public class PlatformUtil : IPlatformUtil
{
    public async Task ShowDisplayAlert(string title, string body, string cancelText)
    {
        try
        {
            await Application.Current.MainPage.DisplayAlert(title, body, cancelText);
        }
        catch (Exception e)
        {
            //Would add to logging platform
            Console.WriteLine(e.ToString());
        }
    }
}
