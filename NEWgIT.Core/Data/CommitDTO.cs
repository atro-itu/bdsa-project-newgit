namespace NEWgIT.Core.Data;

public record CommitDTO(int commitId, string author, DateTime date, string hash);

public record CommitCreateDTO(string author, DateTime date, string hash);

// update?
