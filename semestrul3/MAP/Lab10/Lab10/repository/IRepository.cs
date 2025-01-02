namespace Lab10.repository;

public interface IRepository<ID, E>
{
    
    void Add(E entity);
    void Remove(ID id);
    E GetById(ID id);
    List<E> GetAll();
    void SaveToFile();
    void LoadFromFile();
}