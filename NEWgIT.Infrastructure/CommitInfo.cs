namespace NEWgIT.Infrastructure;

public class CommitInfo
{
    public int Id { get; set; }

    [StringLength(100)]
    [Required]
    public string Author { get; set; }

    [StringLength(40)]
    [Required]
    public string Hash { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Analysis Analysis { get; set; } = null!;

    public CommitInfo(string author, DateTime date, string hash)
    {
        Author = author;
        Date = date;
        Hash = hash;
    }
}

