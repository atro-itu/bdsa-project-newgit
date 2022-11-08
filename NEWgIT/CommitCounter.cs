namespace NEWgIT;

public static class CommitCounter
{
    public static Dictionary<DateOnly, int> FrequencyMode(ICommitLog log) =>
        log.GroupBy(c => DateOnly.FromDateTime(c.Committer.When.Date))
           .ToDictionary(g => g.Key, g => g.Count());

    public static Dictionary<string, Dictionary<DateOnly, int>> AuthorMode(ICommitLog log) =>
        log.DistinctBy(c => c.Author.Name)
           .Select(c => c.Author.Name)
           .ToDictionary(author => author, author => FrequencyByAuthor(log, author));

    public static Dictionary<DateOnly, int> FrequencyByAuthor(ICommitLog log, string author) =>
       log.Where(c => c.Author.Name == author)
          .GroupBy(c => DateOnly.FromDateTime(c.Committer.When.Date))
          .ToDictionary(g => g.Key, g => g.Count());
}
