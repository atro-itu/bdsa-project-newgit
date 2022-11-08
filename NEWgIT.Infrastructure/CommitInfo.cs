namespace NEWgIT.Infrastructure;

public class CommitInfo
{
    public int Id;

    [StringLength(100)]
    public string Author;

    public DateTime Date;

    public Analysis Analysis = null!;

    public CommitInfo(string author, DateTime date)
    {
        Author = author;
        Date = date;
    }
}

