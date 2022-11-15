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
    public void Get_Should_Return()
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
}
