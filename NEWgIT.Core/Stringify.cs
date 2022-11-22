using System.Text;
using System.Globalization;

namespace NEWgIT;

public static class Stringify
{
    public static string AuthorMode(Dictionary<string, Dictionary<DateOnly, int>> dictionary)
    {
        StringBuilder result = new StringBuilder();
        var sortedDictionary = new SortedDictionary<string, Dictionary<DateOnly, int>>(dictionary);
        foreach (var author in sortedDictionary)
        {
            result.Append('\n').Append(author.Key);
            result.Append(FrequencyMode(author.Value, "\t"));
        }
        return result.ToString();
    }

    public static string FrequencyMode(Dictionary<DateOnly, int> dictionary, string indent = "")
    {
        StringBuilder result = new StringBuilder();
        var sortedDictionary = new SortedDictionary<DateOnly, int>(dictionary);
        foreach (var date in sortedDictionary.Reverse())
        {
            result.Append("\n" + indent + $"{date.Value}\t{date.Key.ToString(GetFormat())}");
        }
        return result.ToString();
    }

    public static CultureInfo GetFormat() => CultureInfo.CreateSpecificCulture("da-DK");

}
