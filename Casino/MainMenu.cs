namespace Casino
{
    public static class MainMenu
    {
        public static void WelcomeMessage()
        {
            Console.WriteLine( "Добро пожаловать в игру Казино!" );
        }

        public static MenuOperations MainMenuChoice()
        {
            Dictionary<MenuOperations, string> options = MenuConfig.DisplayMenu();

            return DataInputService.GetMenuChoice( options );
        }
    }
}