namespace CarFactory.Entities.Transmissions;

public class AutomaticTransmission : ITransmission
{
    public string Name => "6-ступенчатая АКПП";
    public int Gears => 6;

    public string GetDescription() => $"{Name} ({Gears} передач)";
}