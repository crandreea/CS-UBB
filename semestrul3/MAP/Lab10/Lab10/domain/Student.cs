namespace Lab10.domain;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string School { get; set; }

    public Student() {}

    public Student(int id, string name, string school)
    {
        Id = id;
        Name = name;
        School = school;
    }

    // CSV Serialization Methods
    public virtual string ToCsvString()
    {
        return $"{Id},{Name.Replace(",", ";")},{School.Replace(",", ";")}";
    }

    public static Student FromCsvString(string csvLine)
    {
        var values = csvLine.Split(',');
        return new Student(
            int.Parse(values[0]),
            values[1].Replace(";", ","),
            values[2].Replace(";", ",")
        );
    }
}