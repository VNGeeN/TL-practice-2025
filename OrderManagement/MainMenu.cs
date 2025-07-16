public static class MainMenu
{
    public static void WelcomeMessage()
    {
        Console.WriteLine( "Добро пожаловать в сервис заказа товаров!" );
    }
    public static MenuOperation GetMenuChoice()
    {
        MenuConfig.DisplayMenu();
        int choice = DataInputService.GetValidatedInput(
            "Выберите пункт меню: ",
            minValue: ( int )Enum.GetValues( typeof( MenuOperation ) ).Cast<MenuOperation>().Min(),
            maxValue: ( int )Enum.GetValues( typeof( MenuOperation ) ).Cast<MenuOperation>().Max()
        );

        return ( MenuOperation )choice;
    }
}