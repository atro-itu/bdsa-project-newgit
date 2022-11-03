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
    public void FrequencyMode_Should_Return_Collective_Commit_Frequency()
    {
        //Arrange
        var log = _repository.Commits;

        //Act
        var result = CommitCounter.FrequencyMode(log);

        //Assert
        result.Count().Should().Be(5);
        result[new DateOnly(2010, 05, 25)].Should().Be(1);
        result[new DateOnly(2010, 05, 26)].Should().Be(1);
        result[new DateOnly(2019, 5, 26)].Should().Be(2);
        result[new DateOnly(2019, 5, 25)].Should().Be(3);
        result[DateOnly.FromDateTime(DateTimeOffset.Now.Date)].Should().Be(3);
    }

    [Theory]
    [InlineData("Lucas", new int[] { 2, 0, 0, 2, 0 })]
    [InlineData("Bank", new int[] { 1, 0, 0, 1, 2 })]
    [InlineData("Trøstrup", new int[] { 0, 1, 1, 0, 0 })]
    public void FrequencyByAuthor_Should_Return_Commit_Frequency_By_Author(string author, int[] expected)
    {
        //Arrange
        var log = _repository.Commits;
        var Dates = new DateOnly[] { DateOnly.FromDateTime(DateTimeOffset.Now.Date), new DateOnly(2010, 5, 25), new DateOnly(2010, 5, 26), new DateOnly(2019, 5, 25), new DateOnly(2019, 5, 26) };

        //Act
        var result = CommitCounter.FrequencyByAuthor(log, author);

        //Assert
        for (int i = 0; i < Dates.Length; i++)
        {
            if (expected[i] == 0) continue;
            result[Dates[i]].Should().Be(expected[i]);
        }
    }

    [Fact]
    public void AuthorMode_Should_Return_Commit_Frequency_For_Authors()
    {
        //Arrange
        var log = _repository.Commits;

        //Act
        var result = CommitCounter.AuthorMode(log);

        //Assert
        result.Count().Should().Be(3);
        result["Lucas"].Count().Should().Be(2);
        result["Lucas"][DateOnly.FromDateTime(DateTimeOffset.Now.Date)].Should().Be(2);
        result["Lucas"][new DateOnly(2019, 5, 25)].Should().Be(2);

        result["Bank"].Count().Should().Be(3);
        result["Bank"][DateOnly.FromDateTime(DateTimeOffset.Now.Date)].Should().Be(1);
        result["Bank"][new DateOnly(2019, 5, 25)].Should().Be(1);
        result["Bank"][new DateOnly(2019, 5, 26)].Should().Be(2);

        result["Trøstrup"].Count().Should().Be(2);
        result["Trøstrup"][new DateOnly(2010, 5, 25)].Should().Be(1);
        result["Trøstrup"][new DateOnly(2010, 5, 26)].Should().Be(1);
    }

    public void Dispose()
    {
        _repository.Dispose();
        Directory.Delete(_path, recursive: true);
    }
}
