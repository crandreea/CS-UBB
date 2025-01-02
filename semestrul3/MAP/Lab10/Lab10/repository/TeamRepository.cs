using Lab10.domain;

namespace Lab10.repository;

public class TeamRepository : FileRepository<int, Team>
{
    public TeamRepository(string filePath) : base(filePath)
    {
        filePath = "/MAP/Lab10/Lab10/files/teams.txt";
    }

    protected override int GetId(Team entity)
    {
       return entity.Id;
    }

    protected override string ToCsvString(Team entity)
    {
        return entity.ToCsvString();
    }

    protected override Team FromCsvString(string csvLine)
    {
        return Team.FromCsvString(csvLine);
    }
}