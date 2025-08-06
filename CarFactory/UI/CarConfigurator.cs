using CarFactory.Enums;
using CarFactory.Factories.Body;
using CarFactory.Factories.Transmission;
using CarFactory.Factories.Engine;
using CarFactory.Entities.Cars;
using CarFactory.Entities.Engines;
using CarFactory.Entities.Transmissions;
using CarFactory.Entities.Bodies;
using System.ComponentModel;
using System.Reflection;

namespace CarFactory.UI
{
    public class CarConfigurator : ICarConfigurator
    {
        private readonly IEngineFactory _engineFactory;
        private readonly ITransmissionFactory _transmissionFactory;
        private readonly IBodyFactory _bodyFactory;

        public CarConfigurator( IEngineFactory engineFactory,
            ITransmissionFactory transmissionFactory,
            IBodyFactory bodyFactory )
        {
            _engineFactory = engineFactory;
            _transmissionFactory = transmissionFactory;
            _bodyFactory = bodyFactory;
        }
        public ICar Configure()
        {
            Console.Clear();
            Console.WriteLine( "=== Конфигурация нового автомобиля ===" );

            string brand = GetBrand();
            EngineType engineType = GetComponentType<EngineType>( "двигатель" );
            TransmissionType transmissionType = GetComponentType<TransmissionType>( "коробку передач" );
            BodyType bodyType = GetComponentType<BodyType>( "тип кузова" );
            ColorType colorType = GetComponentType<ColorType>( "цвет" );


            IEngine engine = _engineFactory.CreateEngine( engineType );
            ITransmission transmission = _transmissionFactory.CreateTransmission( transmissionType );
            IBody body = _bodyFactory.CreateBody( bodyType, colorType );

            return new Car( brand, engine, transmission, body );
        }

        private string GetBrand()
        {
            Console.Write( "\nВведите марку автомобиля: " );
            return Console.ReadLine() ?? "Без марки";
        }

        private T GetComponentType<T>( string componentName ) where T : Enum
        {
            Console.WriteLine( $"\nВыберите {componentName}:" );

            // Получаем все значения enum
            Array values = Enum.GetValues( typeof( T ) );

            // Выводим все варианты
            foreach ( T value in values )
            {
                string description = GetEnumDescription( value );
                Console.WriteLine( $"{( int )( object )value}. {description}" );
            }

            // Валидация ввода
            while ( true )
            {
                Console.Write( "> " );
                if ( int.TryParse( Console.ReadLine(), out int choice ) && Enum.IsDefined( typeof( T ), choice ) )
                {
                    return ( T )Enum.ToObject( typeof( T ), choice );
                }

                Console.WriteLine( $"Неверный выбор! Введите число от 1 до {values.Length}" );
            }
        }

        private static string GetEnumDescription( Enum value )
        {
            FieldInfo? field = value.GetType().GetField( value.ToString() );
            DescriptionAttribute? attribute = Attribute.GetCustomAttribute( field, typeof( DescriptionAttribute ) ) as DescriptionAttribute;
            return attribute?.Description ?? value.ToString();
        }
    }
}