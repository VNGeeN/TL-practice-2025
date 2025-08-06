using System.ComponentModel;

namespace CarFactory.Enums
{
    public enum ColorType
    {
        [Description( "Красный" )]
        Red = 1,

        [Description( "Синий" )]
        Blue = 2,

        [Description( "Чёрный" )]
        Black = 3,

        [Description( "Белый" )]
        White = 4
    }
}