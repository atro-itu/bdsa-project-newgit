namespace NEWgIT.Core;

public interface ICommitRepository
{
    (Response Response, int CommitId) Create(CommitCreateDTO commit);
    IReadOnlyCollection<CommitDTO> Read();
    CommitDTO Find(int CommitId);
    Response Update(CommitUpdateDTO commit); // needed? who knows
    Response Delete(int commitId, bool force = false); // needed?

}
