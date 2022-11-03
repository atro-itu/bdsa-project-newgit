using System.Text;
using System.Globalization;

namespace NEWgIT;

public static class Stringify 
{
    public static string AuthorMode(Dictionary<string, Dictionary<DateOnly, int>> dic)
    {
        StringBuilder result = new StringBuilder();
        var sortedDic = new SortedDictionary<string, Dictionary<DateOnly, int>>(dic);
        foreach (var author in sortedDic)
        {
            result.Append("\n").Append(author.Key);
            result.Append(FrequencyMode(author.Value));
        }
        return result.ToString();
    }

    public static string FrequencyMode(Dictionary<DateOnly, int> dic)
    {
        StringBuilder result = new StringBuilder();
        var sortedDic = new SortedDictionary<DateOnly, int>(dic);
        foreach (var date in sortedDic.Reverse())
        {
            result.Append($"\n\t{date.Value}\t{date.Key.ToString(GetFormat())}");
        }
        return result.ToString();
    }

    public static CultureInfo GetFormat() => CultureInfo.CreateSpecificCulture("da-DK");
}