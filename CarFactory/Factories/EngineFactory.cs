using CarFactory.Entities.Engines;
using CarFactory.Enums;

namespace CarFactory.Factories
{
    public static class EngineFactory
    {
        public static IEngine CreateEngine( EngineType engineType )
        {
            return engineType switch
            {
                EngineType.Gasoline => new GasolineEngine(),
                EngineType.Diesel => new DieselEngine(),
                _ => throw new ArgumentOutOfRangeException( nameof( engineType ), engineType, null )
            };
        }
    }
}