namespace NEWgIT.Core;

/// <summary>
/// Represents a collection of forks.
/// </summary>
/// <remarks>
/// This class is used to deserialize the JSON response from the GitHub API.
/// </remarks>
/// <param name="Forks">The forks.</param>
public record ForksDTO(ICollection<string> Forks);