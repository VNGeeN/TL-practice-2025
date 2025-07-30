using Fighters.UI.Enums;

namespace Fighters.UI.Menus
{
    public class MainMenu : MenuBase
    {
        public MainMenu()
        {
            CurrentState = MenuState.MainMenu;
        }

        public override void Display()
        {
            Console.Clear();
            Console.WriteLine( "=== ГЛАВНОЕ МЕНЮ ===" );
            Console.WriteLine( "1. Создать бойца" );
            Console.WriteLine( "2. Создать противника" );
            Console.WriteLine( "3. Начать битву" );
            Console.WriteLine( "4. Выход" );
            Console.Write( "Выберите опцию: " );
        }

        public override void HandleInput( NavigationOptions option )
        {
            switch ( option )
            {
                case NavigationOptions.Confirm:
                    switch ( CurrentState )
                    {
                        case MenuState.MainMenu:
                            var input = Console.ReadLine();
                            if ( int.TryParse( input, out int choice ) )
                            {
                                switch ( choice )
                                {
                                    case 1:
                                        CurrentState = MenuState.CreateFighter;
                                        FighterType = "Боец";
                                        break;
                                    case 2:
                                        CurrentState = MenuState.CreateOpponent;
                                        FighterType = "Противник";
                                        break;
                                    case 3:
                                        CurrentState = MenuState.Battle;
                                        break;
                                    case 4:
                                        Environment.Exit( 0 );
                                        break;
                                }
                            }
                            break;
                    }
                    break;
            }
        }
    }
}