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
        throw new NotImplementedException();

    }

    public AnalysisDTO Find(int ID)
    {
        var Analysis =
            from a in _context.Analysis
            where a.Id == ID
            select new AnalysisDTO(a.Id, a.RepoName, a.LatestCommitHash);

        return Analysis.FirstOrDefault()!;
    }

    public Response Update(AnalysisUpdateDTO analysis)
    {
        var Analysis = _context.Analysis.Find(analysis.Id);
        Response response;
        if (Analysis is null)
        {
            response = Response.NotFound;
        }
        else if (_context.Analysis.FirstOrDefault(a => a.Id != analysis.Id && a.RepoName != analysis.repoName && a.LatestCommitHash != analysis.LatestCommitHash) != null)
        {
            response = Response.Conflict;
        }
        else
        {
            Analysis.Id = analysis.Id;
            Analysis.RepoName = analysis.repoName;
            Analysis.LatestCommitHash = analysis.LatestCommitHash;
            _context.SaveChanges();
            response = Response.Updated;
        }

        return response;
    }

    public Response Delete(AnalysisDeleteDTO analysis)
    {
        throw new NotImplementedException();
    }
}
