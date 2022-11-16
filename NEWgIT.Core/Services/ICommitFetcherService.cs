namespace NEWgIT.Core.Services;

public interface ICommitFetcherService
{
    (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(string sourceUrl);
    (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(Repository repo);
}
