namespace wt_lab2_2oop_yushkevich.Models;

public class BusinessCar : Car
{
    public string InteriorClass { get; set; }

    public BusinessCar(
        int id,
        string brand,
        string model,
        int year,
        int horsePower,
        decimal pricePerDay,
        string interiorClass)
        : base(id, brand, model, year, horsePower, pricePerDay)
    {
        InteriorClass = interiorClass;
    }

    public override void PrintInfo()
    {
        Console.WriteLine("=== Автомобиль бизнес-класса ===");

        PrintBaseInfo();

        Console.WriteLine($"Класс салона: {InteriorClass}");
    }
}