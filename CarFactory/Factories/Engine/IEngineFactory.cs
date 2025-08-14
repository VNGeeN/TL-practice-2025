using CarFactory.Entities.Engines;
using CarFactory.Enums;

namespace CarFactory.Factories.Engine
{
    public interface IEngineFactory
    {
        IEngine CreateEngine( EngineType engineType );
    }
}


