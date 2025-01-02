using Lab10.domain;
using Lab10.repository;

namespace Lab10.service;

public class GeneralService
{
    public TeamRepository Teams { get; private set; }
    public StudentRepository Students { get; private set; }
    public MatchRepository Matches { get; private set; }
    
    public PlayerRepository Players { get; private set; }
    public ActivePlayerRepository ActivePlayers { get; private set; }

    private const string TEAMS_FILE = "teams.txt";
    private const string STUDENTS_FILE = "students.txt";
    private const string MATCHES_FILE = "matches.txt";
    private const string ACTIVE_PLAYERS_FILE = "activeplayers.txt";
    private const string PLAYERS_FILE = "players.txt";

    public GeneralService()
    {
        Teams = new TeamRepository("teams.txt");
        Teams.LoadFromFile();

        Students = new StudentRepository("students.txt");
        Students.LoadFromFile();
        
        Matches = new MatchRepository("matches.txt");
        Matches.LoadFromFile();
        
        Players = new PlayerRepository("players.txt");
        Players.LoadFromFile();
        
        ActivePlayers = new ActivePlayerRepository("activeplayers.txt");
        ActivePlayers.LoadFromFile();
    }

    public void SaveAllData()
    {
        Teams.SaveToFile();
        Students.SaveToFile();
        Matches.SaveToFile();
        ActivePlayers.SaveToFile();
    }
    
    public List<Match> GetMatchesByPeriod(DateTime start, DateTime end)
    {
        List<Match> entities = Matches.GetAll();
        return entities.Where(m => m.Date >= start && m.Date <= end).ToList();
    }
    
    public List<Player> GetPlayersByTeam(int teamId)
    {
        Team team = Teams.GetById(teamId);
        List<Player> entities = Players.GetAll();
        return entities.Where(pl => pl.Team == team).ToList();
    }
    
    public int GetMatchScore(int matchId)
    {
        List<ActivePlayer> entities= ActivePlayers.GetAll();
        return entities
            .Where(ap => 
                ap.MatchId == matchId &&
                ap.Status == PlayerStatus.Participant)
            .Sum(ap => ap.PointsScored);
    } 
    
    public List<ActivePlayer> GetActivePlayersByTeamAndMatch(int teamId, int matchId)
    {
        List<Player> entities = GetPlayersByTeam(teamId);
        List<int> playersIds = entities.Select(ap => ap.Id).ToList();
        List<ActivePlayer> entities2 = ActivePlayers.GetAll();
        return entities2.Where(pl => pl.MatchId == matchId && playersIds.Contains(pl.PlayerId)).ToList();
    }

    
}




