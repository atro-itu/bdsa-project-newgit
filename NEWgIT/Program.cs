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
        // create context from factory
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
                {
                    string repoPath = o.RepositoryPath is null ? Directory.GetCurrentDirectory() : o.RepositoryPath;
                    // var context = GitContextFactory.CreateDbContext();

                    /*
                    (response, id) = AnalysisRepo.find(new DTO(repoPath))
                    if resposnse = not found
                        AnalysisRepo.Create(new CreateDTO(repoPath))
                    */
                    /*
                    AnalysisRepo.UpdateCommits(new UpdateDTO(repoPath))
                    commits = AnalysisRepo.ReadCommits(new DTO(RepoPath))
                    */

                    // var repository = new Repository(repoPath);
                    // var log = repository.Commits;
                    // to some repo

                    if (o.AnalysisMode == "frequency")
                    {
                        // Console.WriteLine(Stringify.FrequencyMode(CommitCounter.FrequencyMode(somethingFromDatabase)));

                    }
                    else if (o.AnalysisMode == "author")
                    {
                        // Console.WriteLine(Stringify.AuthorMode(CommitCounter.AuthorMode(log)));
                    }
                });
    }
}
