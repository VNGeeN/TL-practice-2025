using CarFactory.Enums;
using CarFactory.Factories;
using CarFactory.Entities.Cars;
using CarFactory.Entities.Engines;
using CarFactory.Entities.Transmissions;
using CarFactory.Entities.Bodies;

namespace CarFactory.UI
{
    public class CarConfigurator
    {
        public ICar Configure()
        {
            Console.Clear();
            Console.WriteLine( "=== Конфигурация нового автомобиля ===" );

            string brand = GetBrand();
            EngineType engineType = GetEngineType();
            TransmissionType transmissionType = GetTransmissionType();
            (BodyType bodyType, ColorType colorType) = GetBodyAndColor();

            IEngine engine = EngineFactory.CreateEngine( engineType );
            ITransmission transmission = TransmissionFactory.CreateTransmission( transmissionType );
            IBody body = BodyFactory.CreateBody( bodyType, colorType );

            return new Car( brand, engine, transmission, body );
        }

        private string GetBrand()
        {
            Console.Write( "\nВведите марку автомобиля: " );
            return Console.ReadLine() ?? "Без марки";
        }

        private EngineType GetEngineType()
        {
            Console.WriteLine( "\nВыберите двигатель:" );
            Console.WriteLine( "1. Бензиновый (150 л.с.)" );
            Console.WriteLine( "2. Дизельный (200 л.с.)" );

            return GetChoice( 2 ) switch
            {
                1 => EngineType.Gasoline,
                2 => EngineType.Diesel,
                _ => throw new InvalidOperationException( "Некорректный выбор двигателя" )
            };
        }

        private TransmissionType GetTransmissionType()
        {
            Console.WriteLine( "\nВыберите коробку передач:" );
            Console.WriteLine( "1. Автоматическая (6 передач)" );
            Console.WriteLine( "2. Механическая (5 передач)" );

            return GetChoice( 2 ) switch
            {
                1 => TransmissionType.Automatic,
                2 => TransmissionType.Manual,
                _ => throw new InvalidOperationException( "Некорректный выбор коробки передач" )
            };
        }

        private (BodyType, ColorType) GetBodyAndColor()
        {
            Console.WriteLine( "\nВыберите тип кузова:" );
            Console.WriteLine( "1. Седан" );
            Console.WriteLine( "2. Хэтчбек" );
            int bodyChoice = GetChoice( 2 );

            Console.WriteLine( "\nВыберите цвет:" );
            Console.WriteLine( "1. Красный" );
            Console.WriteLine( "2. Синий" );
            Console.WriteLine( "3. Чёрный" );
            Console.WriteLine( "4. Белый" );
            int colorChoice = GetChoice( 4 );

            BodyType bodyType = bodyChoice switch
            {
                1 => BodyType.Sedan,
                2 => BodyType.Hatchback,
                _ => throw new InvalidOperationException( "Некорректный выбор кузова" )
            };

            ColorType colorType = colorChoice switch
            {
                1 => ColorType.Red,
                2 => ColorType.Blue,
                3 => ColorType.Black,
                4 => ColorType.White,
                _ => throw new InvalidOperationException( "Некорректный выбор цвета" )
            };

            return (bodyType, colorType);
        }

        private int GetChoice( int maxOption )
        {
            int choice;
            do
            {
                Console.Write( "> " );
            } while ( !int.TryParse( Console.ReadLine(), out choice ) || choice < 1 || choice > maxOption );

            return choice;
        }
    }
}