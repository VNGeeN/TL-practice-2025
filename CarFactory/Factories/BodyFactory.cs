using CarFactory.Entities.Bodies;
using CarFactory.Enums;

namespace CarFactory.Factories
{
    public static class BodyFactory
    {
        public static IBody CreateBody( BodyType bodyType, ColorType colorType )
        {
            string color = colorType switch
            {
                ColorType.Red => "Красный",
                ColorType.Blue => "Синий",
                ColorType.Black => "Чёрный",
                ColorType.White => "Белый",
                _ => throw new ArgumentOutOfRangeException( nameof( colorType ), colorType, null )
            };

            return bodyType switch
            {
                BodyType.Sedan => new SedanBody( color ),
                BodyType.Hatchback => new HatchbackBody( color ),
                _ => throw new ArgumentOutOfRangeException( nameof( bodyType ), bodyType, null )
            };
        }
    }
}