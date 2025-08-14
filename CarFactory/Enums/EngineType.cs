using System.ComponentModel;

namespace CarFactory.Enums
{
    public enum EngineType
    {
        [Description( "Бензиновый (150 л.с.)" )]
        Gasoline = 1,

        [Description( "Дизельный (200 л.с.)" )]
        Diesel = 2
    }
}