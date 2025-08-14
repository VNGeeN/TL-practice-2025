using CarFactory.Entities.Transmissions;
using CarFactory.Enums;

namespace CarFactory.Factories.Transmission
{
    public class TransmissionFactory : ITransmissionFactory
    {
        public ITransmission CreateTransmission( TransmissionType transmissionType )
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