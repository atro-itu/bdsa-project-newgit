namespace NEWgIT.Tests;

[Collection("Extensions")]
public class ExtensionsTests : IDisposable
{
    Repository _repository;
    string _path;

    public ExtensionsTests()
    {
        _path = "./repo2";

        Repository.Init(_path);
        _repository = new Repository(_path);

        _repository.Seed();
    }
    [Fact]
    public void ToString_Should_Return_Frequency_String()
    {
        var commitLog = _repository.Commits;
        string expected = @"
        3       02.11.2022
        2       26.05.2019
        3       25.05.2019
        1       26.05.2010
        1       25.05.2010
        ";
        var result = CommitCounter.FrequencyMode(commitLog).FrequencyPrint();
        result.Should().Be(expected);
    }

    public void Dispose()
    {
        _repository.Dispose();
        Directory.Delete(_path, recursive: true);
    }
}