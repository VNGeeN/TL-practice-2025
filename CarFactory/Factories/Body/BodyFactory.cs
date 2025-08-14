using CarFactory.Entities.Bodies;
using CarFactory.Enums;
using CarFactory.Services;

namespace CarFactory.Factories.Body
{
    public class BodyFactory : IBodyFactory
    {

        private readonly IDescriptionService _descriptionService;

        public BodyFactory( IDescriptionService descriptionService )
        {
            _descriptionService = descriptionService;
        }

        public IBody CreateBody( BodyType bodyType, ColorType colorType )
        {
            string color = _descriptionService.GetColorName( colorType );

            return bodyType switch
            {
                BodyType.Sedan => new SedanBody( color ),
                BodyType.Hatchback => new HatchbackBody( color ),
                _ => throw new ArgumentException( $"Неподдерживаемый тип кузова: {bodyType}" )
            };
        }
    }
}