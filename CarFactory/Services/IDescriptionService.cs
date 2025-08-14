using CarFactory.Enums;

namespace CarFactory.Services
{
    public interface IDescriptionService
    {
        string GetEngineDescription( EngineType engineType );
        string GetTransmissionDescription( TransmissionType transmissionType );
        string GetBodyDescription( BodyType bodyType );
        string GetColorDescription( ColorType colorType );
        string GetColorName( ColorType colorType );
    }

}