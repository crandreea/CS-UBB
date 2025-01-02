using Lab10.domain;
using Lab10.service;

namespace Lab10;
class Program
{
    static void Main(string[] args)
    {
        GeneralService dataManager = new GeneralService();
        PopulateInitialData(dataManager);
        dataManager.SaveAllData();

        UI console = new UI();
        console.Run();
    }

    static void PopulateInitialData(GeneralService dataManager)
    {
        // Populate Teams
        List<Team> teams = new List<Team>
        {
            new Team(1, "Houston Rockets"),
            new Team(2, "Los Angeles Lakers"),
            new Team(3, "LA Clippers"),
            new Team(4, "Chicago Bulls"),
            new Team(5, "Cleveland Cavaliers"),
            new Team(6, "Utah Jazz"),
            new Team(7, "Brooklyn Nets"),
            new Team(8, "New Orleans Pelicans"),
            new Team(9, "Indiana Pacers"),
            new Team(10, "Toronto Raptors")
        };
        teams.ForEach(team => dataManager.Teams.Add(team));

        // Populate Students (Players)
        List<Student> students = new List<Student>
        {
            // Houston Rockets Players
            new Player(1, "Ion Popescu", "Scoala Horea", teams[0]),
            new Player(2, "Maria Ionescu", "Scoala Horea", teams[0]),
            new Player(3, "Andrei Popa", "Scoala Horea", teams[0]),
            new Player(4, "Elena Radu", "Scoala Horea", teams[0]),
            new Player(5, "Mihai Stanescu", "Scoala Horea", teams[0]),

            // Los Angeles Lakers Players
            new Player(6, "Cristina Mureșan", "Scoala Octavian Goga", teams[1]),
            new Player(7, "Bogdan Constantinescu", "Scoala Octavian Goga", teams[1]),
            new Player(8, "Andreea Vasile", "Scoala Octavian Goga", teams[1]),
            new Player(9, "Marius Rizescu", "Scoala Octavian Goga", teams[1]),
            new Player(10, "Ana Popescu", "Scoala Octavian Goga", teams[1]),

            // LA Clippers Players
            new Player(11, "Vlad Georgescu", "Liceul Lucian Blaga", teams[2]),
            new Player(12, "Ioana Dimitriu", "Liceul Lucian Blaga", teams[2]),
            new Player(13, "Radu Marinescu", "Liceul Lucian Blaga", teams[2]),
            new Player(14, "Silvia Nicolescu", "Liceul Lucian Blaga", teams[2]),
            new Player(15, "Cosmin Tudor", "Liceul Lucian Blaga", teams[2])
        };
        students.ForEach(student => dataManager.Students.Add(student));

        // Populate Matches
        List<Match> matches = new List<Match>
        {
            new Match(1, teams[0], teams[1], new DateTime(2024, 2, 15)),
            new Match(2, teams[1], teams[2], new DateTime(2024, 2, 22)),
            new Match(3, teams[2], teams[0], new DateTime(2024, 3, 1)),
            new Match(4, teams[0], teams[2], new DateTime(2024, 3, 10)),
            new Match(5, teams[1], teams[0], new DateTime(2024, 3, 18))
        };
        matches.ForEach(match => dataManager.Matches.Add(match));

        // Populate Active Players
        List<ActivePlayer> activePlayers = new List<ActivePlayer>
        {
            // Match 1
            new ActivePlayer(1, 1, 1, 10, PlayerStatus.Participant),
            new ActivePlayer(2, 6, 1, 8, PlayerStatus.Participant),
            new ActivePlayer(3, 2, 1, 5, PlayerStatus.Substitute),
            new ActivePlayer(4, 7, 1, 12, PlayerStatus.Participant),

            // Match 2
            new ActivePlayer(5, 7, 2, 9, PlayerStatus.Participant),
            new ActivePlayer(6, 11, 2, 7, PlayerStatus.Participant),
            new ActivePlayer(7, 8, 2, 6, PlayerStatus.Substitute),
            new ActivePlayer(8, 12, 2, 11, PlayerStatus.Participant),

            // Match 3
            new ActivePlayer(9, 12, 3, 10, PlayerStatus.Participant),
            new ActivePlayer(10, 1, 3, 8, PlayerStatus.Participant),
            new ActivePlayer(11, 13, 3, 5, PlayerStatus.Substitute),
            new ActivePlayer(12, 2, 3, 7, PlayerStatus.Participant)
        };
        activePlayers.ForEach(activePlayer => dataManager.ActivePlayers.Add(activePlayer));
        dataManager.SaveAllData();
    }
}