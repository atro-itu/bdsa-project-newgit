namespace NEWgIT.Core.Data;

/// <summary>
/// Represents a collection of forks.
/// </summary>
/// <param name="Forks">A collection of forks, represented by their repository identifier (<repoOwner>/<repoName>).</param>
public record ForksDTO(ICollection<string> Forks);
