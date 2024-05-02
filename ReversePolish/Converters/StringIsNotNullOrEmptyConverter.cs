using System.Globalization;

namespace ReversePolish.Converters;

public class StringIsNotNullOrEmptyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        string? inputString = value as string;

        return !string.IsNullOrEmpty(inputString);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
