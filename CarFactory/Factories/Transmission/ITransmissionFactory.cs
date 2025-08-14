using CarFactory.Entities.Transmissions;
using CarFactory.Enums;

namespace CarFactory.Factories.Transmission
{
    public interface ITransmissionFactory
    {
        ITransmission CreateTransmission( TransmissionType transmissionType );
    }
}


