namespace NEWgIT;

public static class CommitCounter
{
    public static Dictionary<DateOnly, int> FrequencyMode(ICommitLog log) =>
        log.GroupBy(c => DateOnly.FromDateTime(c.Committer.When.Date)).ToDictionary(g => g.Key, g => g.Count());

    public static Dictionary<DateOnly, int> AuthorMode(ICommitLog log, string author) => log.Where(c => c.Author.Name == author).GroupBy(c => DateOnly.FromDateTime(c.Committer.When.Date)).ToDictionary(g => g.Key, g => g.Count());
}
