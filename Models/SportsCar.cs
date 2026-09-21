namespace wt_lab2_2oop_yushkevich.Models;

public class SportsCar : Car
{
    public int MaxSpeed { get; set; }

    public SportsCar(
        int id,
        string brand,
        string model,
        int year,
        int horsePower,
        decimal pricePerDay,
        int maxSpeed)
        : base(id, brand, model, year, horsePower, pricePerDay)
    {
        MaxSpeed = maxSpeed;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("=== Спортивный автомобиль ===");

        PrintBaseInfo();

        Console.WriteLine($"Максимальная скорость: {MaxSpeed} км/ч");
    }
}