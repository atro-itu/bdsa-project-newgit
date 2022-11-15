namespace NEWgIT.Core;

public record AnalysisDTO(int id, string repoIdentifier, ICollection<CommitDTO> commits, string latestCommitHash);

public record AnalysisCreateDTO(string repoIdentifier, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisUpdateDTO(string repoIdentifier, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisDeleteDTO(string repoIdentifier);

// details DTO - returns collection of commits?
