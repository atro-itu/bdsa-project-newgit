namespace NEWgIT.Core.Services;

public interface IForkFetcherService
{
    Task<ICollection<string>> FetchForks(string repoIdentifier);
    Task<ICollection<string>> FetchForks(string repoOwner, string repoName);
}

