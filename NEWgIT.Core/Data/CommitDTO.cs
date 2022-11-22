namespace NEWgIT.Core.Data;
public sealed record CommitDTO
{
    public string Author { get; init; } = null!;
    public DateTime Date { get; init; }
    public string Hash { get; init; } = null!;
}
