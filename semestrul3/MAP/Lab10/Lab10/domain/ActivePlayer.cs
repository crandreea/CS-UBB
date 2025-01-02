namespace Lab10.domain;

public class ActivePlayer
{
    public int Id { get; set; }
    public int PlayerId { get; set; }
    public int MatchId { get; set; }
    public int PointsScored { get; set; }
    public PlayerStatus Status { get; set; }
    

    public ActivePlayer(int id, int playerId, int matchId, int pointsScored, PlayerStatus status)
    {
        Id = id;
        PlayerId = playerId;
        MatchId = matchId;
        PointsScored = pointsScored;
        Status = status;
    }

    // CSV Serialization Methods
    public string ToCsvString()
    {
        return $"{Id},{PlayerId},{MatchId},{PointsScored},{Status}";
    }

    public static ActivePlayer FromCsvString(string csvLine)
    {
        var values = csvLine.Split(',');
        return new ActivePlayer(
            int.Parse(values[0]),
            int.Parse(values[1]),
            int.Parse(values[2]),
            int.Parse(values[3]),
            (PlayerStatus)Enum.Parse(typeof(PlayerStatus), values[4])
        );
    }
}