public static class MainMenu
{
    public static void WelcomeMessage()
    {
        Console.WriteLine( "Добро пожаловать в сервис заказа товаров!" );
    }

    public static NavigationOptions GetMenuChoice()
    {
        WelcomeMessage();
        NavigationOptions option = DataInputService.GetNavigationChoice(
            "---Главное меню---",
            new Dictionary<NavigationOptions, string>
            {
                { NavigationOptions.Confirm, "Создать новый заказ" },
                { NavigationOptions.Back, "Выход" }
            } );

        return ( NavigationOptions )option;
    }
}