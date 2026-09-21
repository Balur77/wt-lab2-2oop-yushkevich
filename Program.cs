using wt_lab2_2oop_yushkevich.Interfaces;
using wt_lab2_2oop_yushkevich.Models;
using wt_lab2_2oop_yushkevich.Services;

Console.WriteLine("======================================");
Console.WriteLine("     АВТОСАЛОН — АРЕНДА АВТОМОБИЛЕЙ");
Console.WriteLine("======================================");

// ======================================
// Создание тестовых объектов
// ======================================

// Используется обычный конструктор
SportsCar sportsCar = new SportsCar(
    1,
    "Porsche",
    "911",
    2023,
    450,
    300,
    310
);

// Используется объектный инициализатор.
// Он дополняет создание объекта после вызова конструктора.
BusinessCar businessCar = new BusinessCar(
    2,
    "Mercedes-Benz",
    "E-Class",
    2024,
    258,
    200,
    "Premium"
)
{
    Color = "Черный"
};

SportsCar sportsCar2 = new SportsCar(
    3,
    "Ferrari",
    "F8 Tributo",
    2022,
    720,
    500,
    340
);

BusinessCar businessCar2 = new BusinessCar(
    4,
    "BMW",
    "7 Series",
    2023,
    340,
    280,
    "Luxury"
);

SportsCar sportsCar3 = new SportsCar(
    5,
    "Audi",
    "R8",
    2021,
    570,
    400,
    330
);

BusinessCar businessCar3 = new BusinessCar(
    6,
    "Toyota",
    "Camry",
    2022,
    200,
    150,
    "Business"
);

// ======================================
// Единый список автомобилей
// ======================================

List<Car> carsList =
[
    sportsCar,
    businessCar,
    sportsCar2,
    businessCar2,
    sportsCar3,
    businessCar3
];

// ======================================
// Интерфейсный сервис
// ======================================

IItemService<Car> carService = new CarService();

foreach (Car car in carsList)
{
    carService.Add(car);
}

// ======================================
// Dictionary<int, Car>
// Быстрый поиск автомобиля по ID
// ======================================

Dictionary<int, Car> carsDictionary = carsList.ToDictionary(
    car => car.Id
);

// ======================================
// Асинхронный сервис
// ======================================

CarAsyncService asyncService = new CarAsyncService(carsList);

// ======================================
// Массив базового типа Car
// Демонстрация полиморфизма
// ======================================

Car[] carsArray = carsList.ToArray();

// ======================================
// Меню
// ======================================

while (true)
{
    Console.WriteLine();
    Console.WriteLine("======================================");
    Console.WriteLine("МЕНЮ");
    Console.WriteLine("======================================");
    Console.WriteLine("1. Показать все автомобили (Car[])");
    Console.WriteLine("2. Изменить стоимость аренды");
    Console.WriteLine("3. Найти автомобиль по ID через сервис");
    Console.WriteLine("4. Найти автомобиль по марке через сервис");
    Console.WriteLine("5. Показать список автомобилей (List<Car>)");
    Console.WriteLine("6. Найти автомобиль по ID через Dictionary");
    Console.WriteLine("7. Выполнить LINQ-запросы");
    Console.WriteLine("8. Показать статистику");
    Console.WriteLine("9. Группировка автомобилей");
    Console.WriteLine("10. Асинхронная загрузка автомобилей");
    Console.WriteLine("11. Параллельный поиск через Task.WhenAll");
    Console.WriteLine("12. Обработка ошибок");
    Console.WriteLine("13. Отмена асинхронной операции");
    Console.WriteLine("0. Выход");
    Console.WriteLine("======================================");

    Console.Write("Выберите действие: ");

    string? choice = Console.ReadLine();

    Console.WriteLine();

    switch (choice)
    {
        // ==================================
        // 1. Car[] + полиморфизм
        // ==================================

        case "1":

            Console.WriteLine(
                "=== Автомобили через массив Car[] ==="
            );

            foreach (Car car in carsArray)
            {
                // Вызовется реализация PrintInfo()
                // соответствующего дочернего класса.
                car.PrintInfo();

                Console.WriteLine();
            }

            break;

        // ==================================
        // 2. Изменение цены
        // ==================================

        case "2":

            Console.Write("Введите ID автомобиля: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int priceCarId))
            {
                Console.WriteLine("Некорректный ID.");
                break;
            }

            if (!carsDictionary.TryGetValue(
                    priceCarId,
                    out Car? carToChange))
            {
                Console.WriteLine(
                    "Автомобиль с таким ID не найден."
                );

                break;
            }

            Console.Write("Введите новую стоимость: ");

            if (!decimal.TryParse(
                    Console.ReadLine(),
                    out decimal newPrice))
            {
                Console.WriteLine(
                    "Некорректная стоимость."
                );

                break;
            }

            carToChange.ChangePrice(newPrice);

            Console.WriteLine(
                "Стоимость аренды успешно обработана."
            );

            break;

        // ==================================
        // 3. Поиск через интерфейсный сервис
        // ==================================

        case "3":

            Console.Write("Введите ID автомобиля: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int id))
            {
                Console.WriteLine("Некорректный ID.");
                break;
            }

            Car? foundById = carService.GetById(id);

            if (foundById != null)
            {
                Console.WriteLine("Автомобиль найден:");
                foundById.PrintInfo();
            }
            else
            {
                Console.WriteLine(
                    "Автомобиль с таким ID не найден."
                );
            }

            break;

        // ==================================
        // 4. Перегрузка GetById(string)
        // ==================================

        case "4":

            Console.Write("Введите марку автомобиля: ");

            string? brand = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(brand))
            {
                Console.WriteLine("Марка не введена.");
                break;
            }

            Car? foundByBrand = carService.GetById(brand);

            if (foundByBrand != null)
            {
                Console.WriteLine("Автомобиль найден:");
                foundByBrand.PrintInfo();
            }
            else
            {
                Console.WriteLine(
                    "Автомобиль такой марки не найден."
                );
            }

            break;

        // ==================================
        // 5. List<Car>
        // ==================================

        case "5":

            Console.WriteLine(
                "=== Список автомобилей List<Car> ==="
            );

            foreach (Car car in carsList)
            {
                Console.WriteLine(
                    $"ID: {car.Id} | " +
                    $"{car.Brand} {car.Model} | " +
                    $"{car.PricePerDay} BYN/сутки"
                );
            }

            break;

        // ==================================
        // 6. Dictionary + TryGetValue
        // ==================================

        case "6":

            Console.Write("Введите ID автомобиля: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int searchId))
            {
                Console.WriteLine("Некорректный ID.");
                break;
            }

            if (carsDictionary.TryGetValue(
                    searchId,
                    out Car? foundCar))
            {
                Console.WriteLine("Автомобиль найден:");
                foundCar.PrintInfo();
            }
            else
            {
                Console.WriteLine(
                    "Автомобиль с таким ID не найден."
                );
            }

            break;

        // ==================================
        // 7. LINQ
        // ==================================

        case "7":

            // ------------------------------
            // Where
            // ------------------------------

            Console.WriteLine(
                "=== Where: автомобили дороже 250 BYN/сутки ==="
            );

            IEnumerable<Car> expensiveCars = carsList
                .Where(car => car.PricePerDay > 250);

            foreach (Car car in expensiveCars)
            {
                Console.WriteLine(
                    $"{car.Brand} {car.Model} — " +
                    $"{car.PricePerDay} BYN/сутки"
                );
            }

            // ------------------------------
            // OrderByDescending
            // ------------------------------

            Console.WriteLine();
            Console.WriteLine(
                "=== OrderByDescending: сортировка по цене ==="
            );

            IEnumerable<Car> sortedCars = carsList
                .OrderByDescending(car => car.PricePerDay);

            foreach (Car car in sortedCars)
            {
                Console.WriteLine(
                    $"{car.Brand} {car.Model} — " +
                    $"{car.PricePerDay} BYN/сутки"
                );
            }

            // ------------------------------
            // Select
            // ------------------------------

            Console.WriteLine();
            Console.WriteLine(
                "=== Select: выбор необходимых полей ==="
            );

            var carNames = carsList
                .Select(car => new
                {
                    Name = $"{car.Brand} {car.Model}",
                    Price = car.PricePerDay
                });

            foreach (var car in carNames)
            {
                Console.WriteLine(
                    $"{car.Name} — {car.Price} BYN/сутки"
                );
            }

            break;

        // ==================================
        // 8. Агрегатные операции
        // ==================================

        case "8":

            Console.WriteLine(
                "=== Статистика автомобилей ==="
            );

            int count = carsList.Count;

            decimal sum = carsList.Sum(
                car => car.PricePerDay
            );

            decimal average = carsList.Average(
                car => car.PricePerDay
            );

            decimal min = carsList.Min(
                car => car.PricePerDay
            );

            decimal max = carsList.Max(
                car => car.PricePerDay
            );

            Console.WriteLine(
                $"Количество автомобилей: {count}"
            );

            Console.WriteLine(
                $"Суммарная стоимость аренды: {sum} BYN"
            );

            Console.WriteLine(
                $"Средняя стоимость аренды: {average:F2} BYN"
            );

            Console.WriteLine(
                $"Минимальная стоимость аренды: {min} BYN"
            );

            Console.WriteLine(
                $"Максимальная стоимость аренды: {max} BYN"
            );

            break;

        // ==================================
        // 9. GroupBy
        // ==================================

        case "9":

            Console.WriteLine(
                "=== Группировка автомобилей по категориям ==="
            );

            var groupedCars = carsList
                .GroupBy(car => car.GetType().Name);

            foreach (var group in groupedCars)
            {
                Console.WriteLine();
                Console.WriteLine(
                    $"Категория: {group.Key}"
                );

                Console.WriteLine(
                    $"Количество: {group.Count()}"
                );

                foreach (Car car in group)
                {
                    Console.WriteLine(
                        $"- {car.Brand} {car.Model}"
                    );
                }
            }

            break;

        // ==================================
        // 10. async/await + Task.Delay
        // ==================================

        case "10":

            Console.WriteLine(
                "=== Асинхронная загрузка автомобилей ==="
            );

            List<Car> loadedCars =
                await asyncService.GetCarsAsync();

            Console.WriteLine(
                $"Загружено автомобилей: {loadedCars.Count}"
            );

            foreach (Car car in loadedCars)
            {
                Console.WriteLine(
                    $"{car.Brand} {car.Model}"
                );
            }

            break;

        // ==================================
        // 11. Task.WhenAll
        // ==================================

        case "11":

            Console.WriteLine(
                "=== Параллельный поиск автомобилей ==="
            );

            Task<Car?> searchTask1 =
                asyncService.GetCarByIdAsync(1);

            Task<Car?> searchTask2 =
                asyncService.GetCarByIdAsync(3);

            Task<Car?> searchTask3 =
                asyncService.GetCarByBrandAsync("BMW");

            Car?[] searchResults = await Task.WhenAll(
                searchTask1,
                searchTask2,
                searchTask3
            );

            foreach (Car? car in searchResults)
            {
                if (car != null)
                {
                    Console.WriteLine(
                        $"Найден: {car.Brand} {car.Model}"
                    );
                }
                else
                {
                    Console.WriteLine(
                        "Автомобиль не найден."
                    );
                }
            }

            break;

        // ==================================
        // 12. try/catch/finally
        // ==================================

        case "12":

            Console.WriteLine(
                "=== Обработка исключений ==="
            );

            Console.Write("Введите ID автомобиля: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int asyncId))
            {
                Console.WriteLine("Некорректный ID.");
                break;
            }

            await asyncService.ProcessCarAsync(asyncId);

            break;

        // ==================================
        // 13. CancellationToken
        // ==================================

        case "13":

            Console.WriteLine(
                "=== Отмена асинхронной операции ==="
            );

            using (CancellationTokenSource cts =
                   new CancellationTokenSource())
            {
                try
                {
                    // Автоматическая отмена через 1.2 секунды.
                    // Загрузка каждого автомобиля занимает 500 мс.
                    cts.CancelAfter(1200);

                    List<Car> result =
                        await asyncService
                            .GetCarsWithCancellationAsync(
                                cts.Token
                            );

                    Console.WriteLine(
                        $"Загрузка завершена. " +
                        $"Получено автомобилей: {result.Count}"
                    );
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine(
                        "Операция была отменена."
                    );
                }
            }

            break;

        // ==================================
        // Выход
        // ==================================

        case "0":

            Console.WriteLine(
                "Программа завершена."
            );

            return;

        default:

            Console.WriteLine(
                "Ошибка: неизвестная команда."
            );

            break;
    }
}