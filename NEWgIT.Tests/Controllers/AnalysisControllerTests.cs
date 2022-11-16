namespace NEWgIT.Tests.Controllers;

using Xunit;
using System.Collections.Generic;
using NEWgIT.Controllers;
using NSubstitute;
using NEWgIT.Core;
using NEWgIT.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

public class AnalysisControllerTests
{
    private readonly AnalysisController _controller;
    private IAnalysisRepository _mockRepository;
    private ICommitFetcherService _mockCommitFetcherService;
    private IForkFetcherService _mockForkFetcherService;

    public AnalysisControllerTests()
    {
        _mockRepository = Substitute.For<IAnalysisRepository>();
        _mockCommitFetcherService = Substitute.For<ICommitFetcherService>();
        _mockForkFetcherService = Substitute.For<IForkFetcherService>();
        _controller = new AnalysisController(_mockRepository, _mockCommitFetcherService, _mockForkFetcherService);
    }

    [Fact]
    public void Get_Should_Return_Repo_As_AuthorMode_Given_RepoIdentifier()
    {
        // Arrange
        List<CommitDTO> commitDTO = new List<CommitDTO>()
        {
            new CommitDTO(1, "Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitDTO(2, "Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };
        var analysisDTO = new AnalysisDTO(1, "duckth/testrepo", commitDTO, "1234567891");
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(analysisDTO);
        var expected = new OkObjectResult("{\"Frepe\":{\"2021-01-01\":1},\"Banksy\":{\"2021-05-02\":1}}")
        {
            ContentTypes = { "application/json" }
        };

        // Act
        var actual = _controller.GetAuthorMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);

    }

    [Fact]
    public void Get_Should_Return_Repo_As_FrequencyMode_Given_RepoIdentifier_And_FrequencyMode_String()
    {
        // Arrange
        List<CommitDTO> commitDTO = new List<CommitDTO>()
        {
            new CommitDTO(1, "Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitDTO(2, "Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };
        var analysisDTO = new AnalysisDTO(1, "duckth/testrepo", commitDTO, "1234567891");
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(analysisDTO);
        var expected = new OkObjectResult("{\"2021-01-01\":1,\"2021-05-02\":1}")
        {
            ContentTypes = { "application/json" }
        };

        // Act
        var actual = _controller.GetFrequencyMode("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Get_Should_Return_NotFoundObjectResult_Given_None_Existing_Repo()
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

    [Fact(Skip = "Unable to mock the static method")]
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
        CommitFetcherService.Instance.Returns(_mockCommitFetcherService);
        var expected = new CreatedResult("duckth/testrepo", null);

        // Act
        var actual = _controller.Create("duckth", "testrepo");

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
