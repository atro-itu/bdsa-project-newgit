namespace NEWgIT.Core.Data;

/// <summary>
/// Represents a frequency analysis object.
/// </summary>
/// <param name="Frequencies">A map of key-value pairs from date to commmit frequency.</param>
public record FrequenciesDTO(Dictionary<DateOnly, int> Frequencies);
