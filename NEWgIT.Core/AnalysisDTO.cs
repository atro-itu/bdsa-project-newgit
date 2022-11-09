namespace NEWgIT.Core;

public record AnalysisDTO(int Id, string repoName, string latestCommitHash);

public record AnalysisCreateDTO(string repoIdentifier, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisUpdateDTO(int Id, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisDeleteDTO(int Id);

// details DTO - returns collection of commits?
