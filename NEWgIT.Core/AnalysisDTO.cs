namespace NEWgIT.Core;

public record AnalysisDTO(int Id, string repoName, string LatestCommitHash);

public record AnalysisCreateDTO(string repoIdentifier, ICollection<CommitCreateDTO> commits, string latestCommitHash);

public record AnalysisUpdateDTO(int Id, string repoName, string LatestCommitHash);

public record AnalysisDeleteDTO(int Id, string repoName, string LatestCommitHash);

// details DTO - returns collection of commits?

// example from assignment 
// public record WorkItemDetailsDTO(int Id, string Title, string Description, DateTime Created, string AssignedToName, IReadOnlyCollection<string> Tags, State State, DateTime StateUpdated);
