using System.Diagnostics;

class Program
{
    private static bool _inMainMenu = true;

    static void Main()
    {
        while ( _inMainMenu )
        {
            MainMenu.WelcomeMessage();
            var operation = MainMenu.GetMenuChoice();

            switch ( operation )
            {
                case MenuOperation.CreateOrder:
                    ProcessOrder();
                    break;
                case MenuOperation.Exit:
                    Console.WriteLine( "Спасибо за использование нашего сервиса!" );
                    return;
            }
        }
    }

    private static void ProcessOrder()
    {
        try
        {
            OrderProcessingService.StartOrderProcess( ReturnToMainMenu );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Произошла ошибка: {ex.Message}" );
            ReturnToMainMenu();
        }
    }

    private static void ReturnToMainMenu()
    {
        _inMainMenu = true;
        Console.WriteLine( "\nНажмите любую клавишу для возврата в главное меню" );
        Console.ReadKey();
    }
}