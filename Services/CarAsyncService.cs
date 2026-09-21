using wt_lab2_2oop_yushkevich.Models;

namespace wt_lab2_2oop_yushkevich.Services;

public class CarAsyncService
{
    private readonly List<Car> cars;

    public CarAsyncService(List<Car> cars)
    {
        this.cars = cars;
    }

    // ======================================
    // async/await + Task.Delay
    // ======================================

    public async Task<List<Car>> GetCarsAsync()
    {
        await Task.Delay(300);

        // Возвращаем копию списка,
        // чтобы внешний код не изменял внутреннюю коллекцию сервиса.
        return cars.ToList();
    }

    // ======================================
    // Асинхронный поиск по ID
    // ======================================

    public async Task<Car?> GetCarByIdAsync(int id)
    {
        await Task.Delay(300);

        return cars.FirstOrDefault(
            car => car.Id == id
        );
    }

    // ======================================
    // Асинхронный поиск по марке
    // ======================================

    public async Task<Car?> GetCarByBrandAsync(string brand)
    {
        await Task.Delay(300);

        return cars.FirstOrDefault(
            car => car.Brand.Equals(
                brand,
                StringComparison.OrdinalIgnoreCase
            )
        );
    }

    // ======================================
    // try/catch/finally
    // ======================================

    public async Task ProcessCarAsync(int id)
    {
        try
        {
            Console.WriteLine(
                $"Начата обработка автомобиля с ID {id}..."
            );

            await Task.Delay(500);

            if (id <= 0)
            {
                throw new ArgumentException(
                    "ID автомобиля должен быть больше нуля."
                );
            }

            Car? car = cars.FirstOrDefault(
                item => item.Id == id
            );

            if (car == null)
            {
                throw new InvalidOperationException(
                    "Автомобиль с указанным ID не найден."
                );
            }

            Console.WriteLine(
                $"Автомобиль {car.Brand} {car.Model} " +
                "успешно обработан."
            );
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(
                $"Ошибка аргумента: {ex.Message}"
            );
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(
                $"Ошибка операции: {ex.Message}"
            );
        }
        finally
        {
            Console.WriteLine(
                "Асинхронная операция завершена."
            );
        }
    }

    // ======================================
    // CancellationToken
    // ======================================

    public async Task<List<Car>> GetCarsWithCancellationAsync(
        CancellationToken cancellationToken)
    {
        List<Car> result = new();

        foreach (Car car in cars)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await Task.Delay(
                500,
                cancellationToken
            );

            result.Add(car);

            Console.WriteLine(
                $"Загружен: {car.Brand} {car.Model}"
            );
        }

        return result;
    }
}