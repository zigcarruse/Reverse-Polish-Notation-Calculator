namespace ReversePolish;

public interface IPlatformUtil
{
    Task ShowDisplayAlert(string title, string body, string cancelText);
}
