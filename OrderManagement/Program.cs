using System.Diagnostics;

class Program
{
    private static bool _inMainMenu = true;

    static void Main()
    {
        while ( _inMainMenu )
        {
            NavigationOptions operation = MainMenu.GetMenuChoice();

            switch ( operation )
            {
                case NavigationOptions.Confirm:
                    ProcessOrder();
                    break;
                case NavigationOptions.Back:
                    Console.WriteLine( "Спасибо за использование нашего сервиса!" );
                    return;
            }
        }
    }

    private static void ProcessOrder()
    {
        try
        {
            OrderProcessingService.StartOrderProcess();
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Произошла ошибка: {ex.Message}" );
            ReturnToMainMenu();
        }
    }

    public static void ReturnToMainMenu()
    {
        _inMainMenu = true;
        Console.WriteLine( "\nНажмите любую клавишу для возврата в главное меню" );
        Console.ReadKey();
    }
}