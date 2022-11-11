namespace NEWgIT.Api.Controllers;
using NEWgIT.Core;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly ILogger<AnalysisController> _logger;
    private readonly IAnalysisRepository _repository;

    public AnalysisController(ILogger<AnalysisController> logger, IAnalysisRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    [Route("analysis/{repoOwner}/{repoName}")]
    public string Get(string repoOwner, string repoName)
    {
        string sourceUrl = $"https://github.com/{repoOwner}/{repoName}";
        (HashSet<CommitCreateDTO> commits, string latestCommitHash) = CommitFetcherService.Instance.GetRepoCommits(sourceUrl);
        return JsonConvert.SerializeObject(commits);
    }

    [HttpGet(Name = "ReadAnalysis")]
    public string Read()
    {
        IReadOnlyCollection<AnalysisDTO> analysis = _repository.Read();
        return JsonConvert.SerializeObject(analysis);
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
    [HttpPut]
    [Route("analysis/{repoOwner}/{repoName}")]
    public string Update(string repoOwner, string repoName)
    {
        // AnalysisUpdateDTO update = new AnalysisUpdateDTO{};
        // Response response = _repository.Update(id: $"{repoOwner}/{repoName}");
        return "not implemented";
    }

    // [HttpDelete("{analysis}")]
    // [Route("analysis/{repoOwner}/{repoName}")]
    // public string Delete(string repoOwner, string repoName) 
    //          => _repository.Delete(analysis);
    //     return "not implemented yet";
}
