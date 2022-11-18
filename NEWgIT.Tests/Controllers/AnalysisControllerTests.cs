namespace NEWgIT.Tests.Controllers;

using Xunit;
using System.Collections.Generic;
using NEWgIT.Controllers;
using NSubstitute;
using NEWgIT.Core;
using NEWgIT.Core.Services;
using NEWgIT.Core.Data;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

public class AnalysisControllerTests
{
    private readonly AnalysisController _controller;
    private readonly IAnalysisRepository _mockRepository;
    private readonly ICommitFetcherService _mockCommitFetcherService;
    private readonly IForkFetcherService _mockForkFetcherService;

    public AnalysisControllerTests()
    {
        _mockRepository = Substitute.For<IAnalysisRepository>();
        _mockCommitFetcherService = Substitute.For<ICommitFetcherService>();
        _mockForkFetcherService = Substitute.For<IForkFetcherService>();
        _controller = new AnalysisController(_mockRepository, _mockCommitFetcherService, _mockForkFetcherService);
    }

    [Fact]
    public void GetAuthorMode_Should_Return_Repo_As_AuthorMode_Given_RepoIdentifier()
    {
        // Arrange
        List<CommitDTO> commitDTO = new List<CommitDTO>()
        {
            new CommitDTO(1, "Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitDTO(2, "Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };
        var analysisDTO = new AnalysisDTO(1, "duckth/testrepo", commitDTO, "1234567891");
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(analysisDTO);
        var expectedDictionary = new Dictionary<string, Dictionary<DateOnly, int>>()
        {
            { "Frepe", new Dictionary<DateOnly, int>() { { new DateOnly(2021, 1, 1), 1 } } },
            { "Banksy", new Dictionary<DateOnly, int>() { { new DateOnly(2021, 5, 2), 1 } } }
        };
        var expected = new OkObjectResult(expectedDictionary)
        {
            ContentTypes = { "application/json" }
        };

        // Act
        var actual = _controller.GetAuthorMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);

    }

    [Fact]
    public void GetFrequencyMode_Should_Return_Repo_As_FrequencyMode_Given_RepoIdentifier()
    {
        // Arrange
        List<CommitDTO> commitDTO = new List<CommitDTO>()
        {
            new CommitDTO(1, "Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitDTO(2, "Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };
        var analysisDTO = new AnalysisDTO(1, "duckth/testrepo", commitDTO, "1234567891");
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(analysisDTO);
        var expectedDictionary = new Dictionary<DateOnly, int>()
        {
            { new DateOnly(2021, 1, 1), 1 },
            { new DateOnly(2021, 5, 2), 1 }
        };
        var expected = new OkObjectResult(expectedDictionary)
        {
            ContentTypes = { "application/json" }
        };

        // Act
        var actual = _controller.GetFrequencyMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetForkMode_Should_Return_Forks_Of_Repo()
    {
        // Arrange
        var forks = new HashSet<string>() { "someone/testrepo", "sometwo/testrepo" };
        _mockForkFetcherService.FetchForks("duckth", "testrepo").Returns(forks);
        var expectedForks = new ForksDTO(new HashSet<String> { "someone/testrepo", "sometwo/testrepo" });
        var expected = new OkObjectResult(expectedForks);

        // Act
        var actual = _controller.GetForkMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetAuthor_Should_Return_NotFoundObjectResult_Given_None_Existing_Repo()
    {
        // Arrange
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(i => null!); // (i => null!) is a hack to make it compile
        var expected = new NotFoundObjectResult(null);

        // Act
        var actual = _controller.GetAuthorMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void GetFrequency_Should_Return_NotFoundObjectResult_Given_None_Existing_Repo()
    {
        // Arrange
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(i => null!); // (i => null!) is a hack to make it compile
        var expected = new NotFoundObjectResult(null);

        // Act
        var actual = _controller.GetFrequencyMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Create_Should_Return_ConflictObject_Given_Already_Existing_Entity()
    {
        // Arrange
        List<CommitDTO> commitDTO = new List<CommitDTO>()
        {
            new CommitDTO(1, "Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitDTO(2, "Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };
        var analysisDTO = new AnalysisDTO(1, "duckth/testrepo", commitDTO, "1234567891");
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(analysisDTO);
        var expected = new ConflictObjectResult(new { message = "Analysis already exists" });

        // Act
        var actual = _controller.Create("duckth", "testrepo");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Create_Should_Return_CreatedResult_Given_Not_Existing_Entity()
    {
        // Arrange
        var analysisCreateDTO = new AnalysisCreateDTO("duckth/testrepo", null!, "1234567891");
        _mockRepository.Create(analysisCreateDTO).Returns<(NEWgIT.Core.Response, int)>((Response.Created, 1));
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(i => null!); // (i => null!) is a hack to make it compile

        var commitCreateDTO = new HashSet<CommitCreateDTO>()
        {
            new CommitCreateDTO("Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitCreateDTO("Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };

        _mockCommitFetcherService.GetRepoCommits("duckth/testrepo").Returns<(HashSet<CommitCreateDTO>, string)>((commitCreateDTO, "1234567891"));
        var expected = new CreatedResult("duckth/testrepo", null);

        // Act
        var actual = _controller.Create("duckth", "testrepo");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Update_Should_Return_NotFoundObjectResult_Given_None_Existing_Repo()
    {
        // Arrange
        var analysisUpdateDTO = new AnalysisUpdateDTO("duckth/testrepo", null!, null!);
        _mockRepository.Update(analysisUpdateDTO).Returns<Response>(Response.NotFound);
        _mockCommitFetcherService.GetRepoCommits("duckth/testrepo").Returns<(HashSet<CommitCreateDTO>, string)>((null!, null!));
        var expected = new NotFoundObjectResult(null);

        // Act
        var actual = _controller.Update("duckth", "testrepo");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Update_Should_Return_Ok_Object_Given_Existing_Repo()
    {
        // Arrange
        var analysisUpdateDTO = new AnalysisUpdateDTO("duckth/testrepo", null!, null!);
        _mockRepository.Update(analysisUpdateDTO).Returns<Response>(Response.Updated);
        _mockCommitFetcherService.GetRepoCommits("duckth/testrepo").Returns<(HashSet<CommitCreateDTO>, string)>((null!, null!));
        var expected = new OkObjectResult(null);

        // Act
        var actual = _controller.Update("duckth", "testrepo");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Delete_Should_Return_NotFound_Given_None_Existing_Repo()
    {
        // Arrange
        var analysisDeleteDTO = new AnalysisDeleteDTO("duckth/testrepo");
        _mockRepository.Delete(analysisDeleteDTO).Returns(Response.NotFound);
        var expected = new NotFoundObjectResult(null);

        // Act
        var actual = _controller.Delete("duckth", "testrepo");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Delete_Should_Return_NoContentResult_Given_Existing_Repo()
    {
        // Arrange
        var analysisDeleteDTO = new AnalysisDeleteDTO("duckth/testrepo");
        _mockRepository.Delete(analysisDeleteDTO).Returns(Response.Deleted);
        var expected = new NoContentResult();

        // Act
        var actual = _controller.Delete("duckth", "testrepo");

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}
