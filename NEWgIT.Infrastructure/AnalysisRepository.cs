
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

    public IReadOnlyCollection<AnalysisDTO> Read() =>
        _context.Analysis.Select(analysis => new AnalysisDTO(analysis.Id, analysis.RepoIdentifier, GetCommitDTOs(analysis), analysis.LatestCommitHash))
                          .ToArray();




    public AnalysisDTO Find(int ID)
    {
        var analysis = _context.Analysis.Find(ID);
        if (analysis == null) return null!;
        _context.Commits.Where(commit => commit.Analysis.Id == ID).Load();
        return new AnalysisDTO(analysis.Id, analysis.RepoIdentifier, GetCommitDTOs(analysis), analysis.LatestCommitHash);
    }

    public AnalysisDTO FindByIdentifier(string repoIdentifier)
    {
        var analysis = _context.Analysis.Where(analysis => analysis.RepoIdentifier == repoIdentifier).FirstOrDefault();
        if (analysis == null) return null!;
        return Find(analysis.Id);
    }

    public Response Update(AnalysisUpdateDTO analysisDTO)
    {
        var analysis = _context.Analysis.FindByIdentifier(analysisDTO.repoIdentifier);

        if (analysis == null) return Response.NotFound;
        if (analysis.LatestCommitHash == analysisDTO.latestCommitHash) return Response.NotModified;
        analysis.Commits.Clear();
        analysis.Commits = analysisDTO.commits.Select(dto => new CommitInfo(author: dto.author, date: dto.date, hash: dto.hash)).ToHashSet();
        analysis.LatestCommitHash = analysisDTO.latestCommitHash;
        _context.SaveChanges();

        return Response.Updated;
    }

    public Response Delete(AnalysisDeleteDTO analysisDTO)
    {
        var analysis = _context.Analysis.FindByIdentifier(analysisDTO.repoIdentifier);

        if (analysis is null) return Response.NotFound;

        _context.Analysis.Remove(analysis);
        _context.SaveChanges();
        return Response.Deleted;
    }

    private ICollection<CommitDTO> GetCommitDTOs(Analysis analysis) => analysis
        .Commits.Select(commit => new CommitDTO(
            commit.Id, commit.Author, commit.Date, commit.Hash)
        ).ToHashSet();
}

