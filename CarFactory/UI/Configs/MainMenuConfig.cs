using CarFactory.UI.Enums.MainMenu;

namespace CarFactory.UI.Configs.MainMenuConfig;

public class MainMenuConfig
{
    public static readonly Dictionary<MainMenuOperations, string> MainMenuItems = new()
    {
        {MainMenuOperations.ConfigureNewCar, "Сконфигурировать новый автомобиль"},
        {MainMenuOperations.ShowConfiguredCars, "Просмотреть сконфигурированные автомобили"},
        {MainMenuOperations.Exit, "Выход"}
    };

    public static Dictionary<MainMenuOperations, string> DisplayMainMenu()
    {
        Console.WriteLine( "=== Автомобильный Конфигуратор ===" );
        foreach ( KeyValuePair<MainMenuOperations, string> item in MainMenuItems )
        {
            Console.WriteLine( $"{( int )item.Key}. {item.Value}" );
        }

        return MainMenuItems;
    }
}
