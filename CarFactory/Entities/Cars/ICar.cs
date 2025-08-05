using CarFactory.Entities.Bodies;
using CarFactory.Entities.Engines;
using CarFactory.Entities.Transmissions;
using CarFactory.UI.Interfaces;

namespace CarFactory.Entities.Cars
{
    public interface ICar : INameable
    {
        IEngine Engine { get; }
        ITransmission Transmission { get; }
        IBody Body { get; }
        double MaxSpeed { get; }
    }
}

