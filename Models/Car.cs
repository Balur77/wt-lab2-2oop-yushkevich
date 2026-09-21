namespace wt_lab2_2oop_yushkevich.Models;

public abstract class Car
{
    public int Id { get; private set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public int HorsePower { get; set; }
    public string Color { get; set; }

    private decimal pricePerDay;

    public decimal PricePerDay
    {
        get => pricePerDay;
        private set
        {
            if (value <= 0)
            {
                throw new ArgumentException(
                    "Стоимость аренды должна быть больше нуля."
                );
            }

            pricePerDay = value;
        }
    }

    protected Car(
        int id,
        string brand,
        string model,
        int year,
        int horsePower,
        decimal pricePerDay)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Year = year;
        HorsePower = horsePower;
        PricePerDay = pricePerDay;
        Color = "Не указан";
    }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            Console.WriteLine("Ошибка: стоимость должна быть больше нуля.");
            return;
        }

        PricePerDay = newPrice;
    }

    protected void PrintBaseInfo()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Марка: {Brand}");
        Console.WriteLine($"Модель: {Model}");
        Console.WriteLine($"Год выпуска: {Year}");
        Console.WriteLine($"Мощность: {HorsePower} л.с.");
        Console.WriteLine($"Цвет: {Color}");
        Console.WriteLine($"Стоимость аренды: {PricePerDay} BYN/сутки");
        Console.WriteLine($"Цвет: {Color}");
    }

    public abstract void PrintInfo();
}