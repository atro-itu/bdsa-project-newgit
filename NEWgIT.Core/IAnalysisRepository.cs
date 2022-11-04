namespace NEWgIT.Core;

public interface IAnalysisRepository
{
    (Response Response, int AnalysisId) Create(AnalysisCreateDTO analysis);
    IReadOnlyCollection<AnalysisDTO> Read();
    AnalysisDTO Find(int ID);

    Response Update(AnalysisUpdateDTO analysis);

    Response Delete(AnalysisDeleteDTO analysis);

    // Fra TagDTO ??
    //    (Response Response, int TagId) Create(AnalysisCreateDTO analysi);
    // IReadOnlyCollection<TagDTO> Read();
    // TagDTO Find(int tagId);
    // Response Update(TagDTO tag);
    // Response Delete(int tagId, bool force = false);
}
