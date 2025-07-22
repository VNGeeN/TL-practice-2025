using Casino;

class Program
{
    private static bool _isMainMenu = true;

    static void Main()
    {
        while ( _isMainMenu )
        {
            MainMenu.WelcomeMessage();
            var options = MainMenu.MainMenuChoice();

            switch ( options )
            {
                case MenuOperations.InitUserGameData:
                    ProcessInitUserGameData();
                    break;
                case MenuOperations.ShowUserBalance:
                    UserDataProcessingService.ShowUserData();
                    ReturnToMainMenu();
                    break;
                case MenuOperations.StartGame:
                    GameProcessingService.StartGame();
                    ReturnToMainMenu();
                    break;
                case MenuOperations.Exit:
                    Console.WriteLine( "спасибо за игру!" );
                    return;
            }
        }
    }

    private static void ProcessInitUserGameData()
    {
        try
        {
            UserDataProcessingService.StartUserDataProcess();
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Произошла ошибка: {ex.Message}" );
            ReturnToMainMenu();
        }
    }

    public static void ReturnToMainMenu()
    {
        _isMainMenu = true;
        Console.WriteLine( "\nНажмите любую клавишу для возвращения в главное меню" );
        Console.ReadKey();
    }
}