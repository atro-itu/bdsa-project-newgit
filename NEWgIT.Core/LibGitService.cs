namespace NEWgIT.Core;

using LibGit2Sharp;

/// <summary>
/// A service that handles fetching the commit data from a git repository,
/// and converting it into data transfer objects for storage in the database.
/// </summary>
public sealed class LibGitService
{
    private static LibGitService instance = null!;

    private LibGitService() { }

    public static LibGitService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LibGitService();
            }
            return instance;
        }
    }

    /// <summary>
    /// Clones the repository at the given URL sourceUrl into the given directory,
    /// and gets the commit data from the repository.
    /// Deletes the repository after getting the commit data.
    /// </summary>
    /// <param name="path">The URL of the git repository.</param>
    public (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(string sourceUrl)
    {
        Repository.Clone(sourceUrl, "./temp/target");

        var libgitRepository = new Repository("./temp/target");
        var res = GetRepoCommits(libgitRepository);
        Directory.Delete("./temp", recursive: true);
        return res;
    }

    /// <summary>
    /// Gets the commit data from the given repository,
    /// and converts it into data transfer objects for storage in the database.
    /// </summary>
    /// <param name="repo">The repository as an instance of LibGit2Sharp.Repository</param>
    public (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(Repository repo)
    {
        var commitLog = repo.Commits;
        var latestCommitHash = commitLog.First().Sha;
        var commitDTOs = commitLog.Select(commit =>
            {
                return new CommitCreateDTO(commit.Author.Name, commit.Committer.When.DateTime, commit.Sha);
            }).ToHashSet();

        return (commitDTOs, latestCommitHash);
    }

}
