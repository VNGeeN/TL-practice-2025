using System.ComponentModel;

namespace CarFactory.Enums
{
    public enum TransmissionType
    {
        [Description( "Автоматическая (6 передач)" )]
        Automatic = 1,

        [Description( "Механическая (5 передач)" )]
        Manual = 2
    }
}