using CarFactory.Entities.Cars;
using CarFactory.UI.Enums.MainMenu;
using CarFactory.UI.Configs.MainMenuConfig;

namespace CarFactory.UI
{
    public class MainMenu
    {
        private readonly ICarConfigurator _configurator;

        private readonly List<ICar> _configuredCars = new List<ICar>();


        public MainMenu( ICarConfigurator configurator )
        {
            _configurator = configurator;
        }

        public void Show()
        {
            bool isShow = true;
            while ( isShow )
            {
                Console.Clear();
                MainMenuOperations options = MainMenuChoice();

                switch ( options )
                {
                    case MainMenuOperations.ConfigureNewCar:
                        ConfigureNewCar();
                        break;
                    case MainMenuOperations.ShowConfiguredCars:
                        ShowConfiguredCars();
                        break;
                    case MainMenuOperations.Exit:
                        isShow = false;
                        return;
                        // default:
                        //     Console.WriteLine( "Неверный ввод! Нажмите любую клавишу..." );
                        //     Console.ReadKey();
                        //     break;
                }
            }
        }

        private static MainMenuOperations MainMenuChoice()
        {
            Dictionary<MainMenuOperations, string> options = MainMenuConfig.DisplayMainMenu();

            return DataInputService.GetMenuChoice( options );
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
