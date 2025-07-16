public static class MenuConfig
{
    public static readonly Dictionary<MenuOperation, string> MenuItems = new()
    {
        {MenuOperation.CreateOrder, "Создать новый заказ"},
        {MenuOperation.Exit, "Выйти"}
    };

    public static void DisplayMenu()
    {
        Console.WriteLine( "\n---Главное Меню---" );
        foreach ( var item in MenuItems )
        {
            Console.WriteLine( $"{( int )item.Key}. {item.Value}" );
        }
    }
}