using System.Text.RegularExpressions;

namespace NEWgIT.Tests;

[Collection("Stringify")]
public class StringifyTests : IDisposable
{
    Repository _repository;
    string _path;

    string _remove;
    public StringifyTests()
    {
        _path = "./repo2";

        Repository.Init(_path);
        _repository = new Repository(_path);

        _repository.Seed();

        var chars = new List<char> () { '\t', ' ' };
        _remove = "[" + String.Concat(chars) + "]";
    }
    [Fact]
    public void Stringify_FrequencyMode_Should_Return_Frequency_String()
    {
        var commitLog = _repository.Commits;
        string expected = 
@"
303.11.2022
226.05.2019
325.05.2019
126.05.2010
125.05.2010";
        var result = Regex.Replace(Stringify.FrequencyMode(CommitCounter.FrequencyMode(commitLog)), _remove, String.Empty);
        result.Should().Be(expected);
    }

    [Fact]
    public void Stringify_AuthorMode_Should_Return_Author_String()
    {
        var commitLog = _repository.Commits;
        string expected =
@"
Bank
103.11.2022
226.05.2019
125.05.2019
Lucas
203.11.2022
225.05.2019
Trøstrup
126.05.2010
125.05.2010";
        var result = Regex.Replace(Stringify.AuthorMode(CommitCounter.AuthorMode(commitLog)), _remove , String.Empty);
        result.Should().BeEquivalentTo(expected);
    }

    public void Dispose()
    {
        _repository.Dispose();
        Directory.Delete(_path, recursive: true);
    }
}