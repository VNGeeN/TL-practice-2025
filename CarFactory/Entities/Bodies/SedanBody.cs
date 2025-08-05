namespace CarFactory.Entities.Bodies;

public class SedanBody : IBody
{
    public SedanBody( string color ) => Color = color;
    public string Shape => "Седан";
    public string Color { get; }
}