using CarFactory.Entities.Bodies;
using CarFactory.Enums;

namespace CarFactory.Factories.Body
{
    public interface IBodyFactory
    {
        IBody CreateBody( BodyType bodyType, ColorType colorType );
    }
}

