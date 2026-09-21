using wt_lab2_2oop_yushkevich.Interfaces;
using wt_lab2_2oop_yushkevich.Models;

namespace wt_lab2_2oop_yushkevich.Services;

public class CarService : IItemService<Car>
{
    private readonly List<Car> cars = new();

    public void Add(Car car)
    {
        cars.Add(car);
    }

    public List<Car> GetAll()
    {
        return cars;
    }

    public Car? GetById(int id)
    {
        return cars.FirstOrDefault(car => car.Id == id);
    }

    // Перегрузка метода GetById
    public Car? GetById(string brand)
    {
        return cars.FirstOrDefault(
            car => car.Brand.Equals(
                brand,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }
}