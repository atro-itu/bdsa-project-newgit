
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
        var conflicts = _context.Analysis.Where(analysis => analysis.RepoIdentifier == analysisDTO.repoIdentifier);

        if (conflicts.Any()) return (Response.Conflict, conflicts.First().Id);

        // map each dto to commitinfo object, as this is our model
        ICollection<CommitInfo> commits = analysisDTO.commits.Select(dto => new CommitInfo(author: dto.author, date: dto.date, hash: dto.hash)).ToHashSet();

        var analysis = new Analysis(
            repoIdentifier: analysisDTO.repoIdentifier,
            latestCommitHash: analysisDTO.latestCommitHash
        );
        analysis.Commits = commits;

        _context.Analysis.Add(analysis);
        _context.SaveChanges();

        var response = Response.Created;

        return (response, analysis.Id);
    }

    public IReadOnlyCollection<AnalysisDTO> Read()
    {
        var Analysis =
            from a in _context.Analysis
            select new AnalysisDTO(a.Id, a.RepoIdentifier, a.LatestCommitHash);

        return Analysis.ToArray();

        // use LINQ instead probably
    }

    public AnalysisDTO Find(int ID)
    {
        var Analysis =
            from a in _context.Analysis
            where a.Id == ID
            select new AnalysisDTO(a.Id, a.RepoIdentifier, a.LatestCommitHash);

        return Analysis.FirstOrDefault()!;

        // use LINQ instead probably
    }

    public Response Update(AnalysisUpdateDTO analysisDTO)
    {
        // var analysis = _context.Analysis.Find(analysisDTO.Id);

        // if (analysis == null) return Response.NotFound;

        // override the commits from the dto to the analysis
        // something like:
        // analysis.Commits = analysisDTO.commits.Select(dto => new CommitInfo(author: dto.author, date: dto.date, hash: dto.hash)).ToHashSet();
        // set the other properties

        throw new NotImplementedException();
    }

    public Response Delete(AnalysisDeleteDTO analysis)
    {
        throw new NotImplementedException();
    }
}
