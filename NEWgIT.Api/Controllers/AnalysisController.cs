using Microsoft.AspNetCore.Mvc;

namespace NEWgIT.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly ILogger<AnalysisController> _logger;

    public AnalysisController(ILogger<AnalysisController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public string CreateAnalysis([FromBody] string repoPath)
    {
        var repository = new Repository(repoPath);
        var log = repository.Commits;
        return Stringify.FrequencyMode(CommitCounter.FrequencyMode(log));
    }

    // [HttpGet($"{repoName}", Name = "GetFrequencyAnalysis")]
    // public IEnumerable<(DateOnly, int)> GetFrequency()
    // {
    //     return repoName;
    // }

    // [HttpGet(Name = "GetAuthorAnalysis")]
    // public IEnumerable<(string, IEnumerable<(DateOnly, int)>)> GetAuthor()
    // {
    //     return null;
    // }
}
