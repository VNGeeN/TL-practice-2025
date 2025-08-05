namespace CarFactory.Entities.Bodies;

public class HatchbackBody : IBody
{
    public HatchbackBody( string color ) => Color = color;
    public string Shape => "Хэтчбек";
    public string Color { get; }
}