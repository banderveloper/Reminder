using System.Text;

namespace Reminder.Application.Extensions;

public static class StringExtensions
{
    public static string ToFirstLetterUpperCase(this string str) =>
        char.ToUpper(str[0]) + str.Substring(1);
}