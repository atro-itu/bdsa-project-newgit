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
        Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
        {
            string repoPath = o.RepositoryPath is null ? Directory.GetCurrentDirectory() : o.RepositoryPath;
            var repository = new Repository(repoPath);
            var log = repository.Commits;
            string output = "";
            if (o.AnalysisMode == "frequency")
            {
                output = Stringify.FrequencyMode(CommitCounter.FrequencyMode(log));
            }
            else if (o.AnalysisMode == "author")
            {
                output = Stringify.AuthorMode(CommitCounter.AuthorMode(log));
            }
            Console.WriteLine(output);
        });
    }
}
