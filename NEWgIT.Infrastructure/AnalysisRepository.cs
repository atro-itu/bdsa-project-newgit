namespace NEWgIT.Infrastructure;
public class AnalysisRepository : IAnalysisRepository
{

    private readonly GitContext _context;

    public AnalysisRepository(GitContext context)
    {
        _context = context;
    }

    public (Response Response, int AnalysisId) Create(AnalysisCreateDTO analysisDTO)
    {
        var entity = _context.Analysis.FirstOrDefault(analysis
         => analysis.RepoName == analysisDTO.repoName);
        Response response;

        if (entity == null)
        {
            entity = new Analysis();
            entity.RepoName = analysisDTO.repoName;

            _context.Analysis.Add(entity);
            _context.SaveChanges();

            response = Response.Created;
        }
        else
        {
            response = Response.Conflict;
        }


        return (response, entity.Id);
    }

    public IReadOnlyCollection<AnalysisDTO> Read()
    {
        var analysis
    }

    public AnalysisDTO? Find(int ID)
    {

    }

    public Response Update(AnalysisUpdateDTO analysis)
    {


    }

    public Response Delete(AnalysisDeleteDTO analysis)
    {

    }
}
