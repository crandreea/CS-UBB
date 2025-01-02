namespace Lab10.domain;

public class Match
{
    public int Id { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public DateTime Date { get; set; }

    public Match() {}

    public Match(int id, Team homeTeam, Team awayTeam, DateTime date)
    {
        Id = id;
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Date = date;
    }

    // CSV Serialization Methods
    public string ToCsvString()
    {
        return $"{Id},{HomeTeam.Id},{AwayTeam.Id},{Date:yyyy-MM-dd}";
    }

    public static Match FromCsvString(string csvLine, List<Team> teams)
    {
        var values = csvLine.Split(',');
        return new Match(
            int.Parse(values[0]),
            teams.FirstOrDefault(t => t.Id == int.Parse(values[1])),
            teams.FirstOrDefault(t => t.Id == int.Parse(values[2])),
            DateTime.Parse(values[3])
        );
    }
}