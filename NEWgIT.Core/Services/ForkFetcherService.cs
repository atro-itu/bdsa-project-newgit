using Octokit;
using Microsoft.Extensions.Configuration;

namespace NEWgIT.Core.Services;

public class ForkFetcherService : IForkFetcherService
{
    private static ForkFetcherService instance = null!;
    private readonly GitHubClient _client;

    private ForkFetcherService()
    {
        _client = new GitHubClient(new ProductHeaderValue("NEWgIT")) { Credentials = new Octokit.Credentials("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") };
    }

    public static ForkFetcherService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ForkFetcherService();
            }
            return instance;
        }
    }
    public async Task<ICollection<string>> FetchForks(string repoIdentifier)
    {
        var repoParts = repoIdentifier.Split('/');
        var repoOwner = repoParts[0];
        var repoName = repoParts[1];
        return await FetchForks(repoOwner, repoName);

    }

    public async Task<ICollection<string>> FetchForks(string repoOwner, string repoName)
    {
        var forkRepositories = await _client.Repository.Forks.GetAll(repoOwner, repoName);
        return forkRepositories.Select(repo => repo.FullName).ToHashSet();
    }
}
