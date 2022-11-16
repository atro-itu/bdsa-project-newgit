namespace NEWgIT.Core;

public interface ICommitFetcherService
{
    public (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(string sourceUrl);
    public (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(Repository repo);
}