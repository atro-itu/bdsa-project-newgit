using CommandLine;

namespace NEWgIT;

public class Program
{

    public class Options
    {
        [Option("mode", Required = true, HelpText = "Mode of commit analyis 'frequency' / 'author'")]
        public string? AnalysisMode { get; set; }

        [Option("repo",
            Required = false,
            HelpText = "Path to the repository for analysis. Defaults to current directory")]
        public string? RepositoryPath { get; set; }
    }

    public static void Main(string[] args)
    {
        var factory = new GitContextFactory();
        var context = factory.CreateDbContext(null!);
        var repository = new AnalysisRepository(context);

        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
                {
                    string repoPath = o.RepositoryPath is null ? Directory.GetCurrentDirectory() : o.RepositoryPath;
                    var gitRepo = new Repository(repoPath);
                    var (commitDTOs, hash) = CommitFetcherService.Instance.GetRepoCommits(gitRepo);
                    repository.Create(new AnalysisCreateDTO("test/repo", commitDTOs, hash));

                    if (o.AnalysisMode == "frequency")
                    {
                        Console.WriteLine("freq!");
                        // Console.WriteLine(Stringify.FrequencyMode(CommitCounter.FrequencyMode(somethingFromDatabase)));

                    }
                    else if (o.AnalysisMode == "author")
                    {
                        Console.WriteLine("author!");
                        // Console.WriteLine(Stringify.AuthorMode(CommitCounter.AuthorMode(log)));
                    }
                });
    }
}
