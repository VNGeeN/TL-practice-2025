using Casino;

class Program
{
    private static bool _isMainMenu = true;

    static void Main()
    {
        UserDataService userDataService = new();
        ConsoleDataInputService dataInputService = new();
        UserDataProcessingService userProcessing = new( userDataService, dataInputService );
        GameProcessingService gameProcessing = new( userDataService, dataInputService );

        while ( _isMainMenu )
        {
            MainMenu.WelcomeMessage();
            MenuOperations options = MainMenu.MainMenuChoice();

            switch ( options )
            {
                case MenuOperations.InitUserGameData:
                    userProcessing.StartUserDataProcess();
                    break;

                case MenuOperations.ShowUserBalance:
                    userProcessing.ShowUserData( userDataService.UserData );
                    ReturnToMainMenu();
                    break;

                case MenuOperations.StartGame:
                    gameProcessing.StartGame();
                    ReturnToMainMenu();
                    break;

                case MenuOperations.Exit:
                    Console.WriteLine( "спасибо за игру!" );
                    return;
            }
        }
    }

    public static void ReturnToMainMenu()
    {
        _isMainMenu = true;
        Console.WriteLine( "\nНажмите любую клавишу для возвращения в главное меню" );
        Console.ReadKey();
    }
}