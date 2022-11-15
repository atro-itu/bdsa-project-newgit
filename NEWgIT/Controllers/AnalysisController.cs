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

    [HttpPut]
    [Route("analysis/{repoOwner}/{repoName}")]
    public string Update(string repoOwner, string repoName)
    {
        return "not implemented";
    }
}
