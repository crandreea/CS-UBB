using Lab10.domain;

namespace Lab10.repository;

public class MatchRepository : FileRepository<int, Match>
{
    private List<Team> _teams;

    public MatchRepository( string filePath) : base(filePath)
    {
        filePath = "/MAP/Lab10/Lab10/files/matches.txt";
        //_teams = teams;
    }
    protected override int GetId(Match entity)
    {
        return entity.Id;
    }

    protected override string ToCsvString(Match entity)
    {
        return entity.ToCsvString();
    }

    protected override Match FromCsvString(string csvLine)
    {
        return Match.FromCsvString(csvLine, _teams);
    }
    
    
}