namespace NEWgIT.Tests;

public class CommitCounterTests : IDisposable
{
    Repository _repository;
    string _path;

    public CommitCounterTests()
    {
        _path = "./repo";

        Repository.Init(_path);
        _repository = new Repository(_path);

        _repository.Seed();
    }

    [Fact]
    public void FrequencyMode_Should_Return_Sum_Of_Commits_Per_Day()
    {
        //Arrange
        var log = _repository.Commits;

        //Act
        var result = CommitCounter.FrequencyMode(log);

        //Assert
        result.Count.Should().Be(3);
        result[new DateOnly(2019, 5, 25)].Should().Be(4);
        result[new DateOnly(2019, 5, 26)].Should().Be(3);
        result[DateOnly.FromDateTime(DateTimeOffset.Now.Date)].Should().Be(3);
    }

    [Theory]
    [InlineData("Lucas", new int[] { 2, 2, 0 })]
    [InlineData("Bank", new int[] { 1, 1, 2 })]
    [InlineData("Trøstrup", new int[] { 0, 1, 1 })]
    public void AuthorMode_Should_Return_Sum_Of_Commits_Per_Author(string author, int[] expected)
    {
        //Arrange
        var log = _repository.Commits;
        var Dates = new DateOnly[] { DateOnly.FromDateTime(DateTimeOffset.Now.Date), new DateOnly(2019, 5, 25), new DateOnly(2019, 5, 26) };

        //Act
        var result = CommitCounter.AuthorMode(log, author);

        //Assert
        for (int i = 0; i < Dates.Length; i++)
        {
            if (expected[i] == 0) continue;
            result[Dates[i]].Should().Be(expected[i]);
        }
    }

    public void Dispose()
    {
        _repository.Dispose();
        Directory.Delete(_path, recursive: true);
    }
}
