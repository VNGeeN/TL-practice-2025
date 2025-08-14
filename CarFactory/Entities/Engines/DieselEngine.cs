namespace CarFactory.Entities.Engines;

public class DieselEngine : IEngine
{
    public string Name => "Дизельный двигатель";
    public int Power => 200;

    public string GetDescription() => $"{Name} ({Power} л.с.)";
}