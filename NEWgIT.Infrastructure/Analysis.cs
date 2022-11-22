namespace NEWgIT.Infrastructure;

public class Analysis
{
    public int Id { get; set; }

    public string RepoOwner { get; set; }

    public string RepoName { get; set; }

    public string LatestCommitHash { get; set; }

    public ICollection<CommitInfo> Commits { get; set; }

    public Analysis(string repoOwner, string repoName, string latestCommitHash)
    {
        RepoOwner = repoOwner;
        RepoName = repoName;
        Commits = new HashSet<CommitInfo>();
        LatestCommitHash = latestCommitHash;
    }
}
