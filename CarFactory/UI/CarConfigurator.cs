using CarFactory.Enums;
using CarFactory.Factories.Body;
using CarFactory.Factories.Transmission;
using CarFactory.Factories.Engine;
using CarFactory.Entities.Cars;
using CarFactory.Entities.Engines;
using CarFactory.Entities.Transmissions;
using CarFactory.Entities.Bodies;
using CarFactory.Services;
using System.ComponentModel;
using System.Reflection;

namespace CarFactory.UI
{
    public class CarConfigurator : ICarConfigurator
    {
        private readonly IEngineFactory _engineFactory;

        private readonly ITransmissionFactory _transmissionFactory;

        private readonly IBodyFactory _bodyFactory;

        private readonly IDescriptionService _descriptionService;

        public CarConfigurator( IEngineFactory engineFactory,
            ITransmissionFactory transmissionFactory,
            IBodyFactory bodyFactory,
            IDescriptionService descriptionService )
        {
            _engineFactory = engineFactory;
            _transmissionFactory = transmissionFactory;
            _bodyFactory = bodyFactory;
            _descriptionService = descriptionService;
        }

        public ICar Configure()
        {
            Console.Clear();
            Console.WriteLine( "=== Конфигурация нового автомобиля ===" );

            string brand = GetBrand();
            EngineType engineType = GetComponentType<EngineType>( "двигатель",
                t => _descriptionService.GetEngineDescription( ( EngineType )( object )t ) );

            TransmissionType transmissionType = GetComponentType<TransmissionType>( "коробку передач",
                t => _descriptionService.GetTransmissionDescription( ( TransmissionType )( object )t ) );

            BodyType bodyType = GetComponentType<BodyType>( "тип кузова",
                t => _descriptionService.GetBodyDescription( ( BodyType )( object )t ) );

            ColorType colorType = GetComponentType<ColorType>( "цвет",
                t => _descriptionService.GetColorDescription( ( ColorType )( object )t ) );


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

        private T GetComponentType<T>( string componentName, Func<int, string> getDescription ) where T : Enum
        {
            bool isChosen = false;
            T selectedValue = default!;
            Console.WriteLine( $"\nВыберите {componentName}:" );

            var values = Enum.GetValues( typeof( T ) );
            foreach ( T value in values )
            {
                int intValue = ( int )( object )value;
                Console.WriteLine( $"{intValue}. {getDescription( intValue )}" );
            }

            do
            {
                Console.Write( "> " );
                if ( int.TryParse( Console.ReadLine(), out int choice ) && Enum.IsDefined( typeof( T ), choice ) )
                {
                    selectedValue = ( T )( object )choice;
                    isChosen = true;
                }

                Console.WriteLine( $"Неверный выбор! Выберете другой вариант из предложенных" );
            } while ( !isChosen );

            return selectedValue;
        }
    }
}