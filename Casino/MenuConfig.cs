namespace Casino
{
    public static class MenuConfig
    {
        const string GameName = @"
         ####   #    ###  # #   #  ###
        #      # #  #     # ##  # #   #
        #     #   #  ###  # # # # #   #
        #     #####     # # #  ## #   # 
         #### #   #  ###  # #   #  ###
        ";
        public static readonly Dictionary<MenuOperations, string> MenuItems = new()
        {
            {MenuOperations.InitUserGameData, "Ввести данные для игры"},
            {MenuOperations.ShowUserBalance, "Показать баланс игрока"},
            {MenuOperations.StartGame, "Начать игру"},
            {MenuOperations.Exit, "Выйти"}
        };

        public static Dictionary<MenuOperations, string> DisplayMenu()
        {
            Console.WriteLine( GameName );
            Console.WriteLine( "---Главное меню---" );
            foreach ( var item in MenuItems )
            {
                Console.WriteLine( $"{( int )item.Key}. {item.Value}" );
            }

            return MenuItems;
        }
    }
}