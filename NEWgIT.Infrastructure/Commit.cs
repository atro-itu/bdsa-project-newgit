namespace NEWgIT.Infrastructure;

public class Commit
{
    public int Id;

    [StringLength(100)]
    public string Author;

    public DateTime Date;

    public Analysis Analysis;

    public Commit(string author, DateTime date, Analysis analysis)
    {
        Author = author;
        Date = date;
        Analysis = analysis;
    }
}

