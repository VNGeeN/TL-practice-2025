namespace CarFactory.Entities.Bodies
{
    public interface IBody
    {
        string Shape { get; }
        string Color { get; }

        string GetDescription();
    }
}