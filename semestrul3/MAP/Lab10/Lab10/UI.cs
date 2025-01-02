using Lab10.domain;
using Lab10.service;

namespace Lab10;

public class UI
{
    private GeneralService service;

    public UI()
    {
        service = new GeneralService();
    }

    public void Run()
    {
        while (true)
        {
            MainMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ManageTeams();
                    break;
                case "2":
                    ManageStudents();
                    break;
                case "3":
                    ManageMatches();
                    break;
                case "4":
                    ManageActivePlayers();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă!");
                    break;
            }
        }
    }

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Meniu principal");
        Console.WriteLine("1. Gestionare echipe");
        Console.WriteLine("2. Gestionare studenti");
        Console.WriteLine("3. Gestionare meciuri");
        Console.WriteLine("4. Gestionare jucatori");
        Console.WriteLine("0. Exit");
        Console.Write("> ");
    }

    private void ManageTeams()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Gestionare Echipe");
            Console.WriteLine("1. Afisare Echipe");
            Console.WriteLine("2. Adaugare Echipa");
            Console.WriteLine("0. Inapoi");
            Console.Write("Alegeti o optiune: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var echipe = service.Teams.GetAll();

                    if (echipe.Count == 0)
                    {
                        Console.WriteLine("Nu exista echipe inregistrate.");
                        return;
                    }

                    foreach (Team echipa in echipe)
                    {
                        Console.WriteLine($"ID: {echipa.Id}, Nume: {echipa.Name}");
                    }

                    break;
                case "2":
                    int id;
                    while (true)
                    {
                        Console.Write("Introduceti ID-ul echipei: ");
                        if (int.TryParse(Console.ReadLine(), out id))
                        {
                            break;
                        }

                        Console.WriteLine("ID-ul introdus este invalid sau deja exista. Incercati din nou.");
                    }

                    Console.Write("Introduceti numele echipei: ");
                    string name = Console.ReadLine();

                    Team nouaEchipă = new Team(id, name);
                    service.Teams.Add(nouaEchipă);
                    Console.WriteLine("Echipa adaugata cu succes!");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Optiune invalida.");
                    break;
            }

            Console.WriteLine("\n");
            Console.ReadKey();
        }
    }

    private void ManageStudents()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Gestionare Elevi");
            Console.WriteLine("1. Afișare Elevi");
            Console.WriteLine("2. Adăugare Elev");
            Console.WriteLine("4. Jucatorii unei Echipe");
            Console.WriteLine("0. Înapoi");
            Console.Write("Alegeți o opțiune: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var elevi = service.Students.GetAll();

                    if (elevi.Count == 0)
                    {
                        Console.WriteLine("Nu exista elevi inregistrati.");
                        return;
                    }

                    foreach (var elev in elevi)
                    {
                        if (elev is Player player)
                        {
                            Console.WriteLine(
                                $"ID: {elev.Id}, Nume: {elev.Name}, Școală: {elev.School}, Echipă: {player.Team.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"ID: {elev.Id}, Nume: {elev.Name}, Școală: {elev.School}");
                        }
                    }

                    break;
                case "2":
                    var echipe = service.Teams.GetAll();

                    if (echipe.Count == 0)
                    {
                        Console.WriteLine("Nu exista echipe inregistrate.");
                        return;
                    }

                    foreach (Team echipa in echipe)
                    {
                        Console.WriteLine($"ID: {echipa.Id}, Nume: {echipa.Name}");
                    }

                    Console.Write("Introduceti ID-ul echipei pentru elev: ");
                    if (int.TryParse(Console.ReadLine(), out int idEchipă))
                    {
                        if (service.Teams.GetById(idEchipă) == null)
                        {
                            Console.WriteLine("Echipă inexistentă!");
                        }
                    }

                    int id;
                    while (true)
                    {
                        Console.Write("Introduceți ID-ul elevului: ");
                        if (int.TryParse(Console.ReadLine(), out id) &&
                            service.Students.GetById(id) == null)
                        {
                            break;
                        }

                        Console.WriteLine("ID-ul introdus este invalid!");
                    }

                    Console.Write("Introduceți numele elevului: ");
                    string name = Console.ReadLine();

                    Console.Write("Introduceți școala elevului: ");
                    string school = Console.ReadLine();

                    Player nouElev = new Player(id, name, school, service.Teams.GetById(idEchipă));
                    service.Students.Add(nouElev);
                    Console.WriteLine("Elev adăugat cu succes!");
                    break;

                case "3":
                    Console.Write("Introduceți id-ul echipei: ");
                    int team;
                    int.TryParse(Console.ReadLine(), out team);
                    Console.WriteLine($"{service.Teams.GetById(team).Name} :");
                    foreach (var el in service.GetPlayersByTeam(team))
                    {
                        Console.WriteLine($"ID: {el.Id}, Nume: {el.Name}");
                    }

                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }


            Console.WriteLine("\nApăsați orice tastă pentru a continua...");
            Console.ReadKey();
        }
    }

    private void ManageMatches()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Gestionare Meciuri");
                Console.WriteLine("1. Afișare Meciuri");
                Console.WriteLine("2. Adăugare Meci");
                Console.WriteLine("3. Meciuri intr-o perioada");
                Console.WriteLine("4. Scor meci");
                Console.WriteLine("0. Înapoi");
                Console.Write("Alegeți o opțiune: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var meciuri = service.Matches.GetAll();

                        if (meciuri.Count == 0)
                        {
                            Console.WriteLine("Nu există meciuri înregistrate.");
                            return;
                        }

                        foreach (var meci in meciuri)
                        {
                            Console.WriteLine($"ID: {meci.Id}, Acasă: {meci.HomeTeam.Name}, Deplasare: {meci.AwayTeam.Name}, Data: {meci.Date:dd/MM/yyyy}");
                        }
                        break;
                    case "2":
                        Console.Write("Introduceți ID-ul meciului: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Introduceți ID-ul echipei gazdă: ");
                        int homeTeamId = int.Parse(Console.ReadLine());
                        var homeTeam = service.Teams.GetById(homeTeamId);

                        Console.Write("Introduceți ID-ul echipei oaspete: ");
                        int awayTeamId = int.Parse(Console.ReadLine());
                        var awayTeam = service.Teams.GetById(awayTeamId);

                        if (homeTeam == null || awayTeam == null)
                        {
                            Console.WriteLine("Echipă invalidă!");
                            return;
                        }

                        Console.Write("Introduceți data meciului (yyyy-MM-dd): ");
                        DateTime date = DateTime.Parse(Console.ReadLine());

                        Match novMeci = new Match(id, homeTeam, awayTeam, date);
                        service.Matches.Add(novMeci);
                        Console.WriteLine("Meciul a fost adăugat cu succes!");
                        break;
                    
                    case "3":
                        Console.Write("Introduceți data de început (yyyy-MM-dd): ");
                        DateTime startDate = DateTime.Parse(Console.ReadLine());

                        Console.Write("Introduceți data de final (yyyy-MM-dd): ");
                        DateTime endDate = DateTime.Parse(Console.ReadLine());

                        var meciurii = service.GetMatchesByPeriod(startDate, endDate);
                        Console.WriteLine($"Meciuri între {startDate:yyyy-MM-dd} și {endDate:yyyy-MM-dd}:");
                        foreach (var meci in meciurii)
                        {
                            Console.WriteLine($"ID: {meci.Id}, Echipa Gazdă: {meci.HomeTeam.Name}, Echipa Oaspete: {meci.AwayTeam.Name}, Data: {meci.Date:yyyy-MM-dd}");
                        }
                        break;
                    case "4":
                        Console.Write("Introduceți ID-ul meciului: ");
                        int idd = int.Parse(Console.ReadLine());
                        var scor = service.GetMatchScore(idd);
                        Console.WriteLine($"Scorul pentru meciul {idd} este {scor}");
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opțiune invalidă.");
                        break;
                }

                Console.WriteLine("\nApăsați orice tastă pentru a continua...");
                Console.ReadKey();
            }
        }
    
    private void ManageActivePlayers()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Gestionare Jucători Activi");
            Console.WriteLine("1. Adaugă Jucător Activ");
            Console.WriteLine("2. Afiseaza Jucători Activi");
            Console.WriteLine("3. Jucători Activi pentru Meci și Echipă");
            Console.WriteLine("0. Înapoi");
            Console.Write("Alegeți o opțiune: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Introduceți ID-ul jucătorului activ: ");
                    int id = int.Parse(Console.ReadLine());
                    
                    Console.Write("Introduceți ID-ul elevului: ");
                    int playerId = int.Parse(Console.ReadLine());
                    
                    Console.Write("Introduceți ID-ul meciului: ");
                    int matchId = int.Parse(Console.ReadLine());

                    Console.Write("Introduceți punctele înscrise: ");
                    int puncte = int.Parse(Console.ReadLine());

                    Console.WriteLine("Statut jucător:");
                    Console.WriteLine("1. Participant");
                    Console.WriteLine("2. Rezervă");
                    Console.Write("Alegeți statutul: ");
                    PlayerStatus status = Console.ReadLine() == "1" 
                        ? PlayerStatus.Participant 
                        : PlayerStatus.Substitute;

                    ActivePlayer jucatorActiv = new ActivePlayer(id, playerId, matchId, puncte, status);
                    service.ActivePlayers.Add(jucatorActiv);
                    Console.WriteLine("Jucătorul activ a fost adăugat cu succes!");

                    break;
                
                case "2":
                    var jucatoriActivi = service.ActivePlayers.GetAll();
                    Console.WriteLine("=== Lista Jucătorilor Activi ===");
                    foreach (var jucator in jucatoriActivi)
                    {
                        var elev = service.Students.GetById(jucator.PlayerId);
                        Console.WriteLine($"ID: {jucator.Id}, Elev: {elev?.Name}, Meci ID: {jucator.MatchId}, Puncte: {jucator.PointsScored}, Statut: {jucator.Status}");
                    }
                    break;
                case "3":
                    Console.Write("Introduceți ID-ul meciului: ");
                    int matchIdd = int.Parse(Console.ReadLine());
                    
                    Console.Write("Introduceți ID-ul echipei gazdă: ");
                    int homeTeamIdd = int.Parse(Console.ReadLine());
                    
                    var jucatori = service.GetActivePlayersByTeamAndMatch(homeTeamIdd, matchIdd);
                    foreach (var jucator in jucatori)
                    {
                        Console.WriteLine($"Id: {jucator.PlayerId} Name: {service.Players.GetById(jucator.PlayerId).Name}");
                        
                    }
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opțiune invalidă.");
                    break;
            }

            Console.WriteLine("\nApăsați orice tastă pentru a continua...");
            Console.ReadKey();
        }
    }


    
}