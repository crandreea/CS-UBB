using Lab10.domain;

namespace Lab10.repository;

public class ActivePlayerRepository : FileRepository<int, ActivePlayer>
{
    public ActivePlayerRepository(string filePath) : base(filePath)
    {
        filePath = "/MAP/Lab10/Lab10/files/activeplayers.txt";
    }

    protected override int GetId(ActivePlayer entity)
    {
        return entity.Id;
    }

    protected override string ToCsvString(ActivePlayer entity)
    {
        return entity.ToCsvString();
    }

    protected override ActivePlayer FromCsvString(string csvLine)
    {
        return ActivePlayer.FromCsvString(csvLine);
    }
    
    
}