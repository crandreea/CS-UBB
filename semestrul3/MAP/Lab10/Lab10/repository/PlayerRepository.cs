using Lab10.domain;

namespace Lab10.repository;

public class PlayerRepository : FileRepository<int, Player>
{
    private List<Team> _teams;

    public PlayerRepository(string filePath) : base(filePath)
    {
        filePath = "/MAP/Lab10/Lab10/files/players.txt";
        //_teams = teams;
    }
    protected override int GetId(Player entity)
    {
        return entity.Id;
    }

    protected override string ToCsvString(Player entity)
    {
        return entity.ToCsvString();
    }

    protected override Player FromCsvString(string csvLine)
    {
        return Player.FromCsvString(csvLine, _teams);
    }
}