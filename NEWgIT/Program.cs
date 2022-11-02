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
                    string repo = o.RepositoryPath is null ? Directory.GetCurrentDirectory() : o.RepositoryPath;
                    if (o.AnalysisMode == "frequency")
                    {
                        Console.WriteLine($"You chose frequency mode on repo: {repo}");

                    }
                    else if (o.AnalysisMode == "author")
                    {
                        Console.WriteLine($"You chose author mode on repo: {repo}");
                    }
                });
    }
}
