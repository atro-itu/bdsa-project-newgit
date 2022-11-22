namespace NEWgIT.Core;

public interface IAnalysisRepository
{
    Task<Results<Created<AnalysisDTO>, Conflict<AnalysisDTO>>> CreateAsync(AnalysisDTO analysisDTO);
    Task<IReadOnlyCollection<AnalysisDTO>> ReadAsync();
    Task<Results<Ok<AnalysisDTO>, NotFound<int>>> FindAsync(int id);

    Task<Results<Ok<AnalysisDTO>, NotFound<string>>> FindByIdentifierAsync(string repoIdentifier);

    Task<Results<NoContent, NotFound<int>>> UpdateAsync(AnalysisDTO analysisDTO);

    Task<Results<NoContent, NotFound<int>>> DeleteAsync(AnalysisDTO analysisDTO);
}
