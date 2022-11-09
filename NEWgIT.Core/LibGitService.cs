namespace NEWgIT.Core;

using LibGit2Sharp;

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

    public (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(string sourceUrl)
    {
        Repository.Clone(sourceUrl, "./temp/target");

        var libgitRepository = new Repository("./temp/target");
        var res = GetRepoCommits(libgitRepository);
        Directory.Delete("./temp", recursive: true);
        return res;
    }

    public (HashSet<CommitCreateDTO> commits, string latestCommitHash) GetRepoCommits(Repository repo)
    {
        var commitLog = repo.Commits;
        var latestCommitHash = commitLog.First().Sha;
        var commitDTOs = commitLog.Select(commit =>
            {
                return new CommitCreateDTO(commit.Author.Name, commit.Committer.When.DateTime);
            }).ToHashSet();

        return (commitDTOs, latestCommitHash);
    }

}
