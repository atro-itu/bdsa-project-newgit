using System.Text;

namespace NEWgIT;

public static class Extensions 
{
    public static string AuthorPrint(this Dictionary<string, Dictionary<DateOnly, int>> map)
    {
        StringBuilder result = new StringBuilder();
        foreach (var author in map)
        {
            result.Append(author.Key);
            foreach (var date in author.Value)
            {
                result.Append($"\n\t{date.Value}\t{date.Key}");
            }
        }
        return result.ToString();
    }

    public static string FrequencyPrint(this Dictionary<DateOnly, int> map)
    {
        StringBuilder result = new StringBuilder();
        foreach (var date in map)
        {
            result.Append($"\n\t{date.Value}\t{date.Key}");
        }
        return result.ToString();
    }
}