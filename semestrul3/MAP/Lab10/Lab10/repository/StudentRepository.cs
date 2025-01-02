using Lab10.domain;

namespace Lab10.repository;

public class StudentRepository : FileRepository<int, Student>
{
    private List<Team> _teams;
    public StudentRepository(string filePath) : base(filePath)
    {
        filePath = "/MAP/Lab10/Lab10/files/students.txt";
        //_teams = teams;
    }
    protected override int GetId(Student entity)
    {
       return  entity.Id;
    }

    protected override string ToCsvString(Student entity)
    {
        if (entity is Player player)
        {
            return player.ToCsvString();
        }
        else
        {
            return entity.ToCsvString();
        }
    }

    protected override Student FromCsvString(string csvLine)
    {
        var values = csvLine.Split(',');
        if (values.Length == 4)
        {
            return Player.FromCsvString(csvLine, _teams);
        }
        else
        {
            return Student.FromCsvString(csvLine);
        }
    }
}