using CarFactory.Entities.Bodies;
using CarFactory.Entities.Engines;
using CarFactory.Entities.Transmissions;

namespace CarFactory.Entities.Cars
{
    public class Car : ICar
    {
        public string Name { get; }
        public IEngine Engine { get; }
        public ITransmission Transmission { get; }
        public IBody Body { get; }
        public double MaxSpeed { get; }

        public Car( string brand, IEngine engine, ITransmission transmission, IBody body )
        {
            Name = brand;
            Engine = engine;
            Transmission = transmission;
            Body = body;
            MaxSpeed = CalculateMaxSpeed();
        }

        private double CalculateMaxSpeed()
        {
            double transmissionMultiplier = Transmission is AutomaticTransmission ? 1.2 : 1.0;
            return Engine.Power * transmissionMultiplier;
        }

        public override string ToString()
        {
            return $"Марка: {Name}\n" +
                   $"Двигатель: {Engine.Name} ({Engine.Power} л.с.)\n" +
                   $"Коробка передач: {Transmission.Name} ({Transmission.Gears} передач)\n" +
                   $"Кузов: {Body.Shape}, цвет: {Body.Color}\n" +
                   $"Макс. скорость: {MaxSpeed:F1} км/ч";
        }
    }
}