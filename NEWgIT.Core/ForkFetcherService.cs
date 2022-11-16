using Octokit;

namespace NEWgIT.Core;

public class ForkFetcherService
{
    private static ForkFetcherService instance = null!;
    private readonly RepositoryForksClient client;

    private ForkFetcherService()
    {
        client = new RepositoryForksClient(new ApiConnection(new Connection(new ProductHeaderValue("NEWgIT"))));
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
        var forkRepositories = await client.GetAll(repoOwner, repoName);
        return forkRepositories.Select(repo => repo.FullName).ToHashSet();
    }
}
