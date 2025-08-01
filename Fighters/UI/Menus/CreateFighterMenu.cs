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

        public IRace? SelectedRace => _selectedRace;
        public IClass? SelectedClass => _selectedClass;
        public IWeapon? SelectedWeapon => _selectedWeapon;
        public IArmor? SelectedArmor => _selectedArmor;

        public override void Display()
        {
            Console.Clear();
            // Для состояний выбора показываем специальный заголовок
            if ( CurrentState == MenuState.SelectRace ||
                CurrentState == MenuState.SelectClass ||
                CurrentState == MenuState.SelectWeapon ||
                CurrentState == MenuState.SelectArmor )
            {
                Console.WriteLine( $"=== СОЗДАНИЕ {FighterType.ToUpper()} ===" );
            }
            else
            {
                Console.WriteLine( "=== ГЛАВНОЕ МЕНЮ СОЗДАНИЯ ===" );
            }

            switch ( CurrentState )
            {
                case MenuState.Recreation:
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
            //для отладки
            Console.WriteLine( "----------------------------------" );
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


        private void HandleCreationMenuInput()
        {
            string? input = Console.ReadLine();
            if ( int.TryParse( input, out int choice ) )
            {
                switch ( choice )
                {
                    case 1:
                        NavigateTo( MenuState.SelectRace );
                        break;
                    case 2:
                        NavigateTo( MenuState.SelectClass );
                        break;
                    case 3:
                        NavigateTo( MenuState.SelectWeapon );
                        break;
                    case 4:
                        NavigateTo( MenuState.SelectArmor );
                        break;
                    case 5:
                        CompleteFighterCreation();
                        break;
                    case 6:
                        // Возврат в главное меню с очисткой
                        ResetCreation();
                        CurrentState = MenuState.MainMenu;
                        break;
                }
            }
        }

        private void HandleSelectionInput<T>( List<T> options, Action<T> setter ) where T : INameable
        {
            string? input = Console.ReadLine();
            if ( int.TryParse( input, out int choice ) )
            {
                if ( choice > 0 && choice <= options.Count )
                {
                    setter( options[ choice - 1 ] );
                }

                // Всегда возвращаемся к предыдущему состоянию
                if ( StateHistory.Count > 0 )
                {
                    CurrentState = StateHistory.Pop();
                }
                else
                {
                    CurrentState = MenuState.MainMenu;
                }

                // Явная перерисовка после изменения состояния
                Display();
            }
        }

        public override void HandleInput( NavigationOptions option )
        {
            // Добавим логирование для отладки
            Console.WriteLine( $"Обработка ввода для состояния: {CurrentState}, Опция: {option}" );

            switch ( CurrentState )
            {
                case MenuState.Recreation:
                case MenuState.CreateFighter:
                case MenuState.CreateOpponent:
                    HandleCreationMenuInput();
                    break;

                case MenuState.SelectRace:
                    HandleSelectionInput(
                        _fighterFactory.GetAvailableRaces(),
                        race => _selectedRace = race
                    );
                    break;

                case MenuState.SelectClass:
                    HandleSelectionInput(
                        _fighterFactory.GetAvailableClasses(),
                        cls => _selectedClass = cls
                    );
                    break;

                case MenuState.SelectWeapon:
                    HandleSelectionInput(
                        _fighterFactory.GetAvailableWeapons(),
                        weapon => _selectedWeapon = weapon
                    );
                    break;

                case MenuState.SelectArmor:
                    HandleSelectionInput(
                        _fighterFactory.GetAvailableArmors(),
                        armor => _selectedArmor = armor
                    );
                    break;
            }
        }
        public bool IsReadyToConfirm()
        {
            return ( CurrentState == MenuState.CreateFighter || CurrentState == MenuState.CreateOpponent )
                && _selectedRace != null
                && _selectedClass != null
                && _selectedWeapon != null
                && _selectedArmor != null;
        }

        public void DisplaySummary()
        {
            Console.Clear();
            Console.WriteLine( $"=== ПОДТВЕРЖДЕНИЕ СОЗДАНИЯ: {FighterType.ToUpper()} ===" );
            Console.WriteLine( $"Раса   : {_selectedRace?.Name}" );
            Console.WriteLine( $"Класс  : {_selectedClass?.Name}" );
            Console.WriteLine( $"Оружие : {_selectedWeapon?.Name}" );
            Console.WriteLine( $"Броня  : {_selectedArmor?.Name}" );
        }

        public void ResetCreation()
        {
            _selectedRace = null;
            _selectedClass = null;
            _selectedWeapon = null;
            _selectedArmor = null;
            CurrentFighter = null;
            StateHistory.Clear();  // Очищаем историю
        }

        public void ResetConfirmation()
        {
            // Сбрасываем только флаги подтверждения, сохраняя выбранные параметры
            CurrentFighter = null;
            StateHistory.Clear();
        }

        public void CompleteFighterCreation()
        {
            try
            {
                DisplaySummary();
                // Создание экземпляра бойца через фабрику
                CurrentFighter = _fighterFactory.CreateFighter(
                    FighterType,
                    _selectedRace!,
                    _selectedClass!,
                    _selectedWeapon!,
                    _selectedArmor!
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
