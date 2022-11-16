namespace NEWgIT.Controllers;

using NEWgIT.Core;
using NEWgIT.Core.Services;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IAnalysisRepository _repository;
    private readonly ICommitFetcherService _commitFetcherService;
    private readonly IForkFetcherService _forkFetcherService;

    public AnalysisController(
        IAnalysisRepository repository,
        ICommitFetcherService commitFetcherService,
        IForkFetcherService forkFetcherService
    )
    {
        _repository = repository;
        _commitFetcherService = commitFetcherService;
        _forkFetcherService = forkFetcherService;
    }

    [HttpGet]
    [Route("{repoOwner}/{repoName}")]
    [Route("{repoOwner}/{repoName}/frequency")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<string> GetFrequencyMode(string repoOwner, string repoName)
    {
        var analysis = _repository.FindByIdentifier($"{repoOwner.ToLower()}/{repoName.ToLower()}");
        if (analysis == null) return new NotFoundObjectResult(null);

        var output = JsonConvert.SerializeObject(CommitCounter.FrequencyMode(analysis.commits));

        return new OkObjectResult(output)
        {
            ContentTypes = { "application/json" }
        };
    }

    [HttpGet]
    [Route("{repoOwner}/{repoName}/author")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<string> GetAuthorMode(string repoOwner, string repoName)
    {
        var analysis = _repository.FindByIdentifier($"{repoOwner.ToLower()}/{repoName.ToLower()}");
        if (analysis == null) return new NotFoundObjectResult(null);

        var output = JsonConvert.SerializeObject(CommitCounter.AuthorMode(analysis.commits));

        return new OkObjectResult(output)
        {
            ContentTypes = { "application/json" }
        };
    }

    [HttpGet]
    [Route("{repoOwner}/{repoName}/forks")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public ActionResult<string> GetForkMode(string repoOwner, string repoName)
    {
        var output = JsonConvert.SerializeObject(_forkFetcherService.FetchForks(repoOwner, repoName).Result);

        return new OkObjectResult(output)
        {
            ContentTypes = { "application/json" }
        };
    }

    [HttpGet(Name = "ReadAnalysis")]
    public ActionResult<string> Read(string? mode)
    {
        throw new NotImplementedException();
        // IReadOnlyCollection<AnalysisDTO> analysis = _repository.Read();

        // if (!(mode == "author" || mode == "frequency")) return new BadRequestObjectResult("Invalid mode");

        // var output = analysis.Select(a => new
        // {
        //     identifier = a.repoIdentifier,
        //     commits = mode switch
        //     {
        //         "author" => CommitCounter.AuthorMode(a.commits),
        //         "frequency" => CommitCounter.FrequencyMode(a.commits),
        //         _ => throw new NotImplementedException()
        //     }
        // });


        // return new OkObjectResult(JsonConvert.SerializeObject(output))
        // {
        //     ContentTypes = { "application/json" }
        // };
    }

    [HttpPost]
    [Route("{repoOwner}/{repoName}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult Create(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";
        var analysis = _repository.FindByIdentifier(repoIdentifier);
        if (analysis != null) return new ConflictObjectResult(new { message = "Analysis already exists" });

        var sourceUrl = GetSourceUrl(repoIdentifier);
        var (commits, hash) = _commitFetcherService.GetRepoCommits(sourceUrl);

        var (response, analysisId) = _repository.Create(new AnalysisCreateDTO(repoIdentifier, commits, hash));

        return new CreatedResult(repoIdentifier, null);
    }

    [HttpPut]
    [Route("{repoOwner}/{repoName}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";

        var sourceUrl = GetSourceUrl(repoIdentifier);
        var (commits, hash) = _commitFetcherService.GetRepoCommits(sourceUrl);

        var updateDTO = new AnalysisUpdateDTO(repoIdentifier, commits, hash);
        var response = _repository.Update(updateDTO);

        if (response == Core.Response.NotFound) return new NotFoundObjectResult(null);
        return new OkObjectResult(null);
    }

    [HttpDelete]
    [Route("{repoOwner}/{repoName}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";
        var deleteDTO = new AnalysisDeleteDTO(repoIdentifier);
        var response = _repository.Delete(deleteDTO);
        if (response == Core.Response.NotFound) return new NotFoundObjectResult(null);
        return new NoContentResult();
    }

    private string GetSourceUrl(string repoIdentifier) => $"https://github.com/{repoIdentifier}";
}
