namespace Lab10.repository;

public abstract class FileRepository<ID, E> : IRepository<ID, E>
{
    
    protected List<E> _entities = new();
    private string filePath;

    public FileRepository(string filePath)
    {
        this.filePath = filePath;
    }
    public void Add(E entity)
    {
        _entities.Add(entity);
        SaveToFile();
        
    }

    
    public void Remove(ID id)
    {
        var entityToRemove = _entities.FirstOrDefault(e => GetId(e).Equals(id));
        if (entityToRemove != null)
        {
            _entities.Remove(entityToRemove);
            SaveToFile();
        }
        
    }

    public E GetById(ID id)
    {
        return _entities.FirstOrDefault(e => GetId(e).Equals(id));
    }

    public List<E> GetAll()
    {
        return _entities;
    }

    public void SaveToFile()
    {
        var csvLines = _entities.Select(ToCsvString);
        File.WriteAllLines(filePath, csvLines);

    }

    public void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            _entities = File.ReadAllLines(filePath)
                .Select(FromCsvString)
                .ToList();
        }
    }
    
    protected abstract ID GetId(E entity);
    protected abstract string ToCsvString(E entity);
    protected abstract E FromCsvString(string csvLine);

}