namespace NEWgIT.Controllers;

using NEWgIT.Core;

[ApiController]
[Route("[controller]")]
public class AnalysisController : ControllerBase
{
    private readonly IAnalysisRepository _repository;

    public AnalysisController(IAnalysisRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [Route("{repoOwner}/{repoName}")]
    public ActionResult<string> Get(string repoOwner, string repoName, string? mode = "author")
    {
        var analysis = _repository.FindByIdentifier($"{repoOwner.ToLower()}/{repoName.ToLower()}");
        if (analysis == null) return new NotFoundObjectResult(null);
        if (!(mode == "author" || mode == "frequency")) return new BadRequestObjectResult("Invalid mode");
        var output = mode switch
        {
            "author" => JsonConvert.SerializeObject(CommitCounter.AuthorMode(analysis.commits)),
            "frequency" => JsonConvert.SerializeObject(CommitCounter.FrequencyMode(analysis.commits)),
            _ => throw new NotImplementedException()
        };

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
    public ActionResult Create(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";
        var analysis = _repository.FindByIdentifier(repoIdentifier);
        if (analysis != null) return new ConflictObjectResult(new { message = "Analysis already exists" });

        var sourceUrl = GetSourceUrl(repoIdentifier);
        var (commits, hash) = CommitFetcherService.Instance.GetRepoCommits(sourceUrl);

        var (response, analysisId) = _repository.Create(new AnalysisCreateDTO(repoIdentifier, commits, hash));

        return new CreatedResult(repoIdentifier, null);
    }

    [HttpPut]
    [Route("{repoOwner}/{repoName}")]
    public IActionResult Update(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";

        var sourceUrl = GetSourceUrl(repoIdentifier);
        var (commits, hash) = CommitFetcherService.Instance.GetRepoCommits(sourceUrl);

        var updateDTO = new AnalysisUpdateDTO(repoIdentifier, commits, hash);
        var response = _repository.Update(updateDTO);

        if (response == Core.Response.NotFound) return new NotFoundObjectResult(null);
        if (response == Core.Response.NotModified) return new NoContentResult();
        return new OkObjectResult(null);

    }

    [HttpDelete]
    [Route("{repoOwner}/{repoName}")]
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
