namespace NEWgIT.Tests.Controllers;

using Xunit;
using System.Collections.Generic;
using NEWgIT.Controllers;
using NSubstitute;
using NEWgIT.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

public class AnalysisControllerTests
{
    private readonly AnalysisController _controller;
    private IAnalysisRepository _mockRepository;
    public AnalysisControllerTests()
    {
        _mockRepository = Substitute.For<IAnalysisRepository>();
        _controller = new AnalysisController(_mockRepository);
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
        var actual = _controller.Get("duckth", "testrepo").Result;

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
        var actual = _controller.Get("duckth", "testrepo", "frequency").Result;

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
        var actual = _controller.Get("duckth", "testrepo").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Get_Should_Return_BadRequestObjectResult_Given_IncorrectString()
    {
        // Arrange
        List<CommitDTO> commitDTO = new List<CommitDTO>()
        {
            new CommitDTO(1, "Frepe", new System.DateTime(2021, 1, 1), "1234567890"),
            new CommitDTO(2, "Banksy", new System.DateTime(2021, 5, 2), "1234567891"),
        };
        var analysisDTO = new AnalysisDTO(1, "duckth/testrepo", commitDTO, "1234567891");
        _mockRepository.FindByIdentifier("duckth/testrepo").Returns<AnalysisDTO>(analysisDTO);
        var expected = new BadRequestObjectResult("Invalid mode");

        // Act
        var actual = _controller.Get("duckth", "testrepo", "definetly incorrect").Result;

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    
}
