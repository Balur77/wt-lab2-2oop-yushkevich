namespace wt_lab2_2oop_yushkevich.Interfaces;

public interface IItemService<T>
{
    void Add(T item);

    List<T> GetAll();

    T? GetById(int id);

    T? GetById(string brand);
}