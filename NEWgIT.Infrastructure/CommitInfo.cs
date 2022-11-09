namespace NEWgIT.Infrastructure;

public class CommitInfo
{
    public int Id { get; set; }

    [StringLength(100)]
    [Required]
    public string Author { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Analysis Analysis { get; set; } = null!;

    public CommitInfo(string author, DateTime date)
    {
        Author = author;
        Date = date;
    }

    // public CommitInfo(LibGit2Sharp.Commit commit)
    // {
    //     Author = commit.Author.Name;
    //     Date = commit.Committer.When.DateTime;
    // }
}

