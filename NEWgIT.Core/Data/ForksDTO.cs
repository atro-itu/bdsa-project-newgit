namespace NEWgIT.Core.Data;

/// <summary>
/// Represents a collection of forks.
/// </summary>
/// <param name="Forks">The forks.</param>
public record ForksDTO(ICollection<string> Forks);
