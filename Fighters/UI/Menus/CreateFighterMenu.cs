using Fighters.Core;
using Fighters.Entities.Armors;
using Fighters.Entities.Characters;
using Fighters.Entities.Classes;
using Fighters.Entities.Races;
using Fighters.Entities.Weapons;
using Fighters.UI.Enums;
using Fighters.UI.Interfaces;

namespace Fighters.UI.Menus
{
    public class CreateFighterMenu : MenuBase
    {
        private readonly FighterFactory _fighterFactory = new FighterFactory();
        private IRace? _selectedRace;
        private IClass? _selectedClass;
        private IWeapon? _selectedWeapon;
        private IArmor? _selectedArmor;

        public override void Display()
        {
            Console.Clear();
            Console.WriteLine( $"=== СОЗДАНИЕ {FighterType.ToUpper()} ===" );

            switch ( CurrentState )
            {
                case MenuState.CreateFighter:
                case MenuState.CreateOpponent:
                    Console.WriteLine( "1. Выбрать расу" );
                    Console.WriteLine( "2. Выбрать класс" );
                    Console.WriteLine( "3. Выбрать оружие" );
                    Console.WriteLine( "4. Выбрать броню" );
                    Console.WriteLine( "5. Подтвердить создание" );
                    Console.WriteLine( "6. Вернуться в главное меню" );
                    break;

                case MenuState.SelectRace:
                    DisplaySelectionMenu( "РАСА", _fighterFactory.GetAvailableRaces() );
                    break;

                case MenuState.SelectClass:
                    DisplaySelectionMenu( "КЛАСС", _fighterFactory.GetAvailableClasses() );
                    break;

                case MenuState.SelectWeapon:
                    DisplaySelectionMenu( "ОРУЖИЕ", _fighterFactory.GetAvailableWeapons() );
                    break;

                case MenuState.SelectArmor:
                    DisplaySelectionMenu( "БРОНЯ", _fighterFactory.GetAvailableArmors() );
                    break;
            }
        }

        private void DisplaySelectionMenu<T>( string title, List<T> options ) where T : INameable
        {
            Console.WriteLine( $"=== ВЫБОР {title} ===" );
            for ( int i = 0; i < options.Count; i++ )
            {
                Console.WriteLine( $"{i + 1}. {options[ i ].Name}" );
            }
            Console.WriteLine( $"{options.Count + 1}. Назад" );

            Console.Write( "Выберите действие: " );
        }

        public override void HandleInput( NavigationOptions option )
        {
            switch ( CurrentState )
            {
                case MenuState.CreateFighter:
                case MenuState.CreateOpponent:
                    HandleCreationMenuInput();
                    break;

                case MenuState.SelectRace:
                    HandleSelectionInput( _fighterFactory.GetAvailableRaces(),
                        race => _selectedRace = race, MenuState.SelectRace );
                    break;

                case MenuState.SelectClass:
                    HandleSelectionInput( _fighterFactory.GetAvailableClasses(),
                        cls => _selectedClass = cls, MenuState.SelectClass );
                    break;

                case MenuState.SelectWeapon:
                    HandleSelectionInput( _fighterFactory.GetAvailableWeapons(),
                        weapon => _selectedWeapon = weapon, MenuState.SelectWeapon );
                    break;

                case MenuState.SelectArmor:
                    HandleSelectionInput( _fighterFactory.GetAvailableArmors(),
                        armor => _selectedArmor = armor, MenuState.SelectArmor );
                    break;
            }
        }

        private void HandleCreationMenuInput()
        {
            var input = Console.ReadLine();
            if ( int.TryParse( input, out int choice ) )
            {
                switch ( choice )
                {
                    case 1:
                        PreviousState = CurrentState;
                        CurrentState = MenuState.SelectRace;
                        break;
                    case 2:
                        PreviousState = CurrentState;
                        CurrentState = MenuState.SelectClass;
                        break;
                    case 3:
                        PreviousState = CurrentState;
                        CurrentState = MenuState.SelectWeapon;
                        break;
                    case 4:
                        PreviousState = CurrentState;
                        CurrentState = MenuState.SelectArmor;
                        break;
                    case 5:
                        CompleteFighterCreation();
                        break;
                    case 6:
                        CurrentState = MenuState.MainMenu;
                        break;
                }
            }
        }

        private void HandleSelectionInput<T>( List<T> options, Action<T> setter, MenuState state ) where T : INameable
        {
            var input = Console.ReadLine();
            if ( int.TryParse( input, out int choice ) )
            {
                if ( choice > 0 && choice <= options.Count )
                {
                    setter( options[ choice - 1 ] );
                    CurrentState = PreviousState;
                }
                else if ( choice == options.Count + 1 )
                {
                    CurrentState = PreviousState;
                }
            }
        }

        private void CompleteFighterCreation()
        {
            try
            {
                // Создание экземпляра бойца через фабрику
                CurrentFighter = _fighterFactory.CreateFighter(
                    FighterType,
                    _selectedRace,
                    _selectedClass,
                    _selectedWeapon,
                    _selectedArmor
                );

                Console.WriteLine( $"\n{FighterType} успешно создан!" );
                Console.WriteLine( "Нажмите любую клавишу..." );
                Console.ReadKey();
                CurrentState = MenuState.MainMenu;  // Возврат в главное меню
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Ошибка: {ex.Message}" );
                Console.ReadKey();
            }
        }
    }
}
