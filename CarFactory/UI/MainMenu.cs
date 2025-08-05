using CarFactory.Entities.Cars;

namespace CarFactory.UI
{
    public class MainMenu
    {
        private readonly CarConfigurator _configurator = new CarConfigurator();
        private readonly List<ICar> _configuredCars = new List<ICar>();

        public void Show()
        {
            bool IsShow = true;
            while ( IsShow )
            {
                Console.Clear();
                Console.WriteLine( "=== Автомобильный Конфигуратор ===" );
                Console.WriteLine( "1. Сконфигурировать новый автомобиль" );
                Console.WriteLine( "2. Просмотреть сконфигурированные автомобили" );
                Console.WriteLine( "3. Выход" );
                Console.Write( "Выберите опцию: " );

                switch ( Console.ReadLine() )
                {
                    case "1":
                        ConfigureNewCar();
                        break;
                    case "2":
                        ShowConfiguredCars();
                        break;
                    case "3":
                        IsShow = false;
                        return;
                    default:
                        Console.WriteLine( "Неверный ввод! Нажмите любую клавишу..." );
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ConfigureNewCar()
        {
            ICar car = _configurator.Configure();
            _configuredCars.Add( car );

            Console.WriteLine( "\nАвтомобиль успешно сконфигурирован!" );
            Console.WriteLine( "Нажмите любую клавишу для продолжения..." );
            Console.ReadKey();
        }

        private void ShowConfiguredCars()
        {
            Console.Clear();
            Console.WriteLine( "=== Сконфигурированные автомобили ===" );

            if ( _configuredCars.Count == 0 )
            {
                Console.WriteLine( "Автомобилей еще не сконфигурировано" );
            }
            else
            {
                foreach ( ICar car in _configuredCars )
                {
                    Console.WriteLine( car );
                    Console.WriteLine( "-----------------------------" );
                }
            }

            Console.WriteLine( "\nНажмите любую клавишу для возврата..." );
            Console.ReadKey();
        }
    }
}
