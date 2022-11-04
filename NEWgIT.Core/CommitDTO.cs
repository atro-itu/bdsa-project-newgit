namespace NEWgIT.Core;

public record CommitDTO(int commitId, string author, DateTime date);

public record CommitCreateDTO(string author, DateTime date);

// update?
