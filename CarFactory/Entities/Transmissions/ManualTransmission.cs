namespace CarFactory.Entities.Transmissions;

public class ManualTransmission : ITransmission
{
    public string Name => "5-ступенчатая МКПП";
    public int Gears => 5;

    public string GetDescription() => $"{Name} ({Gears} передач)";
}