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
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
                {
                    string repoPath = o.RepositoryPath is null ? Directory.GetCurrentDirectory() : o.RepositoryPath;
                    if (o.AnalysisMode == "frequency")
                    {
                        var repository = new Repository(repoPath);
                        var log = repository.Commits;
                        Console.WriteLine(Stringify.FrequencyMode(CommitCounter.FrequencyMode(log)));

                    }
                    else if (o.AnalysisMode == "author")
                    {
                        var repository = new Repository(repoPath);
                        var log = repository.Commits;
                        Console.WriteLine(Stringify.AuthorMode(CommitCounter.AuthorMode(log)));
                    }
                });
    }
}
