namespace NEWgIT;

public static class CommitCounter
{
    public static Dictionary<DateOnly, int> FrequencyMode(ICommitLog log) =>
        log.GroupBy(c => DateOnly.FromDateTime(c.Committer.When.Date)).ToDictionary(g => g.Key, g => g.Count());

    public static Dictionary<string, Dictionary<DateOnly, int>> AuthorMode(ICommitLog log)
    {
        var authors = log.DistinctBy(c => c.Author.Name);
        var result = new Dictionary<string, Dictionary<DateOnly, int>>();
        foreach (var author in authors)
        {
            result.Add(author.Author.Name, FrequencyByAuthor(log, author.Author.Name));
        }
        return result;
    }

    public static Dictionary<DateOnly, int> FrequencyByAuthor(ICommitLog log, string author) =>
       log.Where(c => c.Author.Name == author).GroupBy(c => DateOnly.FromDateTime(c.Committer.When.Date)).ToDictionary(g => g.Key, g => g.Count());

}
