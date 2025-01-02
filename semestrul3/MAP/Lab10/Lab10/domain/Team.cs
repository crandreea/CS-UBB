namespace Lab10.domain;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Team() {}

    public Team(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public string ToCsvString()
    {
        return $"{Id},{Name.Replace(",", ";")}";
    }

    public static Team FromCsvString(string csvLine)
    {
        var values = csvLine.Split(',');
        return new Team(
            int.Parse(values[0]),
            values[1].Replace(";", ",")
        );
    }
}