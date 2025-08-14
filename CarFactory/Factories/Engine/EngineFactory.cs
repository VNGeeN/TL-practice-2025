using CarFactory.Entities.Engines;
using CarFactory.Enums;

namespace CarFactory.Factories.Engine
{
    public class EngineFactory : IEngineFactory
    {
        public IEngine CreateEngine( EngineType engineType )
        {
            return engineType switch
            {
                EngineType.Gasoline => new GasolineEngine(),
                EngineType.Diesel => new DieselEngine(),
                _ => throw new ArgumentException( $"Неподдерживаемый тип двигателя: {engineType}" )
            };
        }
    }
}