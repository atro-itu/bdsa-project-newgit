namespace NEWgIT.Controllers;

using NEWgIT.Core;
using NEWgIT.Core.Services;
using NEWgIT.Core.Data;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]
    public ActionResult<FrequenciesDTO> GetFrequencyMode(string repoOwner, string repoName)
    {
        var analysis = _repository.FindByIdentifier($"{repoOwner.ToLower()}/{repoName.ToLower()}");
        if (analysis == null) return NotFound();

        var output = new FrequenciesDTO(CommitCounter.FrequencyMode(analysis.commits));

        return Ok(output);
    }

    [HttpGet]
    [Route("{repoOwner}/{repoName}/author")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Produces("application/json")]

    public ActionResult<AuthorsDTO> GetAuthorMode(string repoOwner, string repoName)
    {
        var analysis = _repository.FindByIdentifier($"{repoOwner.ToLower()}/{repoName.ToLower()}");
        if (analysis == null) return NotFound();

        var output = new AuthorsDTO(CommitCounter.AuthorMode(analysis.commits));

        return Ok(output);
    }

    [HttpGet]
    [Route("{repoOwner}/{repoName}/forks")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public ActionResult<ForksDTO> GetForkMode(string repoOwner, string repoName)
    {
        var output = new ForksDTO(_forkFetcherService.FetchForks(repoOwner, repoName).Result);

        return Ok(output);
    }

    [HttpPost]
    [Route("{repoOwner}/{repoName}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public ActionResult<string> Create(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";
        var analysis = _repository.FindByIdentifier(repoIdentifier);
        if (analysis != null) return Conflict(new { message = "Analysis already exists" });

        var sourceUrl = GetSourceUrl(repoIdentifier);
        var (commits, hash) = _commitFetcherService.GetRepoCommits(sourceUrl);

        var (response, analysisId) = _repository.Create(new AnalysisCreateDTO(repoIdentifier, commits, hash));

        return Created(repoIdentifier, new { id = analysisId });
    }

    [HttpPut]
    [Route("{repoOwner}/{repoName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";

        var sourceUrl = GetSourceUrl(repoIdentifier);
        var (commits, hash) = _commitFetcherService.GetRepoCommits(sourceUrl);

        var updateDTO = new AnalysisUpdateDTO(repoIdentifier, commits, hash);
        var response = _repository.Update(updateDTO);

        if (response == Core.Response.NotFound) return NotFound();
        return Ok(null);
    }

    [HttpDelete]
    [Route("{repoOwner}/{repoName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(string repoOwner, string repoName)
    {
        var repoIdentifier = $"{repoOwner.ToLower()}/{repoName.ToLower()}";
        var deleteDTO = new AnalysisDeleteDTO(repoIdentifier);
        var response = _repository.Delete(deleteDTO);
        if (response == Core.Response.NotFound) return NotFound();
        return NoContent();
    }

    private static string GetSourceUrl(string repoIdentifier) => $"https://github.com/{repoIdentifier}";
}
