namespace NEWgIT.Infrastructure;

public class Analysis
{
    public int Id { get; set; }
    public string RepoName { get; set; }

    public string LatestCommitHash { get; set; }

    public ICollection<Commit> Commits { get; set; }

    public Analysis(string repoName, ICollection<Commit> commits, string latestCommitHash)
    {
        RepoName = repoName;
        Commits = commits;
        LatestCommitHash = latestCommitHash;
    }
}
