namespace NEWgIT.Infrastructure;

public class Analysis
{
    public int Id { get; set; }

    public string RepoIdentifier { get; set; }

    public string LatestCommitHash { get; set; }

    public ICollection<CommitInfo> Commits { get; set; }

    public Analysis(string repoIdentifier, ICollection<CommitInfo> commits, string latestCommitHash)
    {
        RepoIdentifier = repoIdentifier;
        Commits = commits;
        LatestCommitHash = latestCommitHash;
    }
}
