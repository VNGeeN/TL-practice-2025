using CarFactory.UI.Interfaces;

namespace CarFactory.Entities.Transmissions
{
    public interface ITransmission : INameable
    {
        int Gears { get; }

        string GetDescription();
    }
}