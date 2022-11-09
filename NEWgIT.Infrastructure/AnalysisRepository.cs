
namespace NEWgIT.Infrastructure;
public class AnalysisRepository : IAnalysisRepository
{

    private readonly GitContext _context;

    public AnalysisRepository(GitContext context)
    {
        _context = context;
    }

    /*
    UpdateOrCreate? or Find?
    public response, analysisid FindOrCreate(createDTO? maybe findDTO?)
    {
        find analysis in context
        or
        create table from dto
        then
        init libgit repo from analysis path
        for each commit => create object or find in db.. by hash?
        find newest commit => save as newest in table

    }
    */


    public (Response Response, int AnalysisId) Create(AnalysisCreateDTO analysisDTO)
    {
        var conflicts = _context.Analysis.Where(analysis => analysis.RepoIdentifier == analysisDTO.repoIdentifier);

        if (conflicts.Any()) return (Response.Conflict, conflicts.First().Id);

        // map each dto to commitinfo object
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
    }

    public AnalysisDTO Find(int ID)
    {
        var Analysis =
            from a in _context.Analysis
            where a.Id == ID
            select new AnalysisDTO(a.Id, a.RepoIdentifier, a.LatestCommitHash);

        return Analysis.FirstOrDefault()!;
    }

    public Response Update(AnalysisUpdateDTO analysis)
    {
        var Analysis = _context.Analysis.Find(analysis.Id);
        if (Analysis is null)
        {
            return Response.NotFound;
        }
        else if (_context.Analysis.FirstOrDefault(a =>
                                                    a.Id != analysis.Id
                                                 && a.RepoIdentifier != analysis.repoName
                                                 && a.LatestCommitHash != analysis.LatestCommitHash) != null)
        {
            return Response.Conflict;
        }
        else
        {
            /*
            Analysis.Commits.Clear()
            libgitCommits = new Repository(dto.RepoPath).GetCommits
            commitInfoList = libgitCommits.each => new CommitInfo(message, signature)
            Analysis.Commits = commitInfoList
            Analysis.LatestCommitHash = Commits.Latest
            */
            Analysis.Commits.Clear();
            var libgiCommits = new Repository().Commits;
            var commitInfoList = libgiCommits.Select(commit => new CommitInfo(commit.Author.Name, commit.Committer.When.DateTime, commit.Sha));
            Analysis.Commits = commitInfoList.ToHashSet();
            _context.SaveChanges();
            return Response.Updated;
        }
    }

    public Response Delete(AnalysisDeleteDTO analysis)
    {
        throw new NotImplementedException();
    }
}
