namespace NEWgIT.Core.Data;

/// <summary>
/// Represents an author mode analysis.
/// </summary>
/// <param name="AuthorCommits">A map containing each authors commit frequencies</param>
public record AuthorsDTO(Dictionary<string, Dictionary<DateOnly, int>> AuthorFrequencies);
