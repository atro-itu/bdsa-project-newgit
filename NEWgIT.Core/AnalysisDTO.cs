namespace NEWgIT.Core;

public record AnalysisDTO(int id, string repoName, string latestCommitHash);

public record AnalysisCreateDTO(string repoIdentifier, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisUpdateDTO(int id, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisDeleteDTO(int id);

// details DTO - returns collection of commits?
