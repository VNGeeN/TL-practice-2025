using CarFactory.Entities.Transmissions;
using CarFactory.Enums;

namespace CarFactory.Factories
{
    public static class TransmissionFactory
    {
        public static ITransmission CreateTransmission( TransmissionType transmissionType )
        {
            return transmissionType switch
            {
                TransmissionType.Automatic => new AutomaticTransmission(),
                TransmissionType.Manual => new ManualTransmission(),
                _ => throw new ArgumentOutOfRangeException( nameof( transmissionType ), transmissionType, null )
            };
        }
    }
}