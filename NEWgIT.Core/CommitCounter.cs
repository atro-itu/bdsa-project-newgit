namespace NEWgIT.Core;

public static class CommitCounter
{
    public static Dictionary<DateOnly, int> FrequencyMode(ICommitLog log) =>
         FrequencyMode(log.Select(c => new CommitDTO(c.Author.Name, c.Committer.When.Date, c.Sha)));

    public static Dictionary<DateOnly, int> FrequencyMode(IEnumerable<CommitDTO> commits) =>
         commits.GroupBy(c => DateOnly.FromDateTime(c.date.Date))
            .ToDictionary(g => g.Key, g => g.Count());

    public static Dictionary<string, Dictionary<DateOnly, int>> AuthorMode(ICommitLog log) =>
      AuthorMode(log.Select(c => new CommitDTO(c.Author.Name, c.Committer.When.Date, c.Sha)));

    public static Dictionary<string, Dictionary<DateOnly, int>> AuthorMode(IEnumerable<CommitDTO> commits) =>
       commits.DistinctBy(c => c.author)
          .Select(c => c.author)
          .ToDictionary(author => author, author => FrequencyByAuthor(commits, author));

    public static Dictionary<DateOnly, int> FrequencyByAuthor(IEnumerable<CommitDTO> commits, string author) =>
       commits.Where(c => c.author == author)
          .GroupBy(c => DateOnly.FromDateTime(c.date.Date))
          .ToDictionary(g => g.Key, g => g.Count());


}
