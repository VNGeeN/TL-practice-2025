using CarFactory.UI.Interfaces;

namespace CarFactory.Entities.Engines
{
    public interface IEngine : INameable
    {
        int Power { get; }

        string GetDescription();
    }
}