using System.ComponentModel;
using System.Reflection;
using CarFactory.Entities.Bodies;
using CarFactory.Enums;

namespace CarFactory.Factories.Body
{
    public class BodyFactory : IBodyFactory
    {
        public IBody CreateBody( BodyType bodyType, ColorType colorType )
        {
            string color = GetEnumDescription( colorType );

            return bodyType switch
            {
                BodyType.Sedan => new SedanBody( color ),
                BodyType.Hatchback => new HatchbackBody( color ),
                _ => throw new ArgumentException( $"Неподдерживаемый тип кузова: {bodyType}" )
            };
        }

        private static string GetEnumDescription( Enum value )
        {
            FieldInfo? field = value.GetType().GetField( value.ToString() );
            DescriptionAttribute? attribute = Attribute.GetCustomAttribute( field, typeof( DescriptionAttribute ) ) as DescriptionAttribute;
            return attribute?.Description ?? value.ToString();
        }
    }
}