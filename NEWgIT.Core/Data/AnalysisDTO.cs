namespace NEWgIT.Core.Data;

public sealed record AnalysisDTO
{
    public int ID { get; init; }
    public required string RepoOwner { get; init; }
    public required string RepoName { get; init; }
    public List<CommitDTO> Commits { get; init; } = null!;
    public string LatestCommit { get; init; } = null!;
    public string RepoIdentifier() => $"{RepoOwner}/{RepoName}";
}
