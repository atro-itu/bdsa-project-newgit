using System.Text.RegularExpressions;

namespace NEWgIT.Tests;

[Collection("Stringify")]
public class StringifyTests
{
    [Fact]
    public void Stringify_FrequencyMode_Should_Return_Frequency_String()
    {
        //Arrange
        var frequencyDictionary = new Dictionary<DateOnly, int>()
        {
            {new DateOnly (2022, 11, 03), 1},
            {new DateOnly (2019, 05, 26), 2},
            {new DateOnly (2019, 05, 25), 3},
            {new DateOnly (2010, 05, 25), 1},
            {new DateOnly (2010, 05, 26), 1},
        };
        string expected =   "\n1\t03.11.2022\n" + 
                            "2\t26.05.2019\n" + 
                            "3\t25.05.2019\n" + 
                            "1\t26.05.2010\n" + 
                            "1\t25.05.2010";
        var result = Stringify.FrequencyMode(frequencyDictionary);
        result.Should().Be(expected);
    }

    [Fact]
    public void Stringify_AuthorMode_Should_Return_Author_String()
    {
        //Arrange
        var frequencyDictionary0 = new Dictionary<DateOnly, int>()
        {
            {new DateOnly (2022, 11, 03), 1},
            {new DateOnly (2019, 05, 26), 2},
            {new DateOnly (2019, 05, 25), 3},
            {new DateOnly (2010, 05, 25), 1},
            {new DateOnly (2010, 05, 26), 1},
        };
        var frequencyDictionary1 = new Dictionary<DateOnly, int>()
        {
            {new DateOnly (2022, 11, 03), 7},
            {new DateOnly (2019, 07, 26), 2},
            {new DateOnly (2017, 05, 29), 5},
            {new DateOnly (2010, 02, 25), 1},
            {new DateOnly (2010, 05, 26), 2},
        };
        var authorDictionary = new Dictionary<string, Dictionary<DateOnly, int>> ()
        {
            { "Frepe", frequencyDictionary0},
            { "Bank", frequencyDictionary1},
            { "Trøstrup", frequencyDictionary0},
        };

        var expected =  "\nBank\n" + 
                        "\t7\t03.11.2022\n" +
                        "\t2\t26.07.2019\n" +
                        "\t5\t29.05.2017\n" +
                        "\t2\t26.05.2010\n" +
                        "\t1\t25.02.2010\n" +
                        "Frepe\n" +
                        "\t1\t03.11.2022\n" +
                        "\t2\t26.05.2019\n" +
                        "\t3\t25.05.2019\n" +
                        "\t1\t26.05.2010\n" +
                        "\t1\t25.05.2010\n" +
                        "Trøstrup\n" +
                        "\t1\t03.11.2022\n" +
                        "\t2\t26.05.2019\n" +
                        "\t3\t25.05.2019\n" +
                        "\t1\t26.05.2010\n" +
                        "\t1\t25.05.2010";

        // Act
        var result = Stringify.AuthorMode(authorDictionary);
        result.Should().BeEquivalentTo(expected);
    }
}
