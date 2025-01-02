namespace Lab10.domain;

public class Player : Student
{
    public Team Team { get; set; }
    
    public Player(int id, string name, string school, Team team) 
        : base(id, name, school)
    {
        Team = team;
    }

    public override string ToCsvString()
    {
        return $"{base.ToCsvString()},{Team.Id}";
    }

    public static new Player FromCsvString(string csvLine, List<Team> teams)
    {
        var values = csvLine.Split(',');
        var team = teams.FirstOrDefault(t => t.Id == int.Parse(values[3]));
        return new Player(
            int.Parse(values[0]),
            values[1].Replace(";", ","),
            values[2].Replace(";", ","),
            team
        );
    }
}