using Fighters.Core;
using Fighters.UI.Enums;
using Fighters.UI.Menus;
using Fighters.Entities.Characters;
public class GameManager
{
    private MenuBase _currentMenu;                                      // Текущее активное меню
    private readonly List<IFighter> _fighters = new List<IFighter>();   // Список созданных бойцов
    private readonly Arena _arena = new Arena();                        // Арена для проведения битв
    public GameManager()
    {
        _currentMenu = new MainMenu();                                  // Начинаем с главного меню
    }

    /// <summary> Основной игровой цикл </summary>
    public void Run()
    {
        bool isRunning = true;

        while ( isRunning )
        {
            Console.Clear();
            _currentMenu.Display();

            // Показываем меню подтверждения, если создание бойца готово
            if ( _currentMenu is CreateFighterMenu createMenu && createMenu.IsReadyToConfirm() )
            {
                createMenu.DisplaySummary();

                Console.WriteLine( "\nПодтвердите создание бойца:" );
                Console.WriteLine( "1. Подтвердить" );
                Console.WriteLine( "2. Назад" );
                Console.WriteLine( "3. Отмена" );
                Console.Write( "Выберите действие: " );

                string? navInput = Console.ReadLine();

                if ( Enum.TryParse( navInput, out NavigationOptions option ) )
                {
                    switch ( option )
                    {
                        case NavigationOptions.Confirm:
                            createMenu.CompleteFighterCreation();
                            _fighters.Add( createMenu.CurrentFighter! );
                            _currentMenu = new MainMenu();
                            break;

                        case NavigationOptions.Back:
                            // Возвращаемся в меню создания без подтверждения
                            createMenu.CurrentState = MenuState.CreateFighter;
                            // Явно перерисовываем экран
                            Console.Clear();
                            createMenu.Display();
                            continue; // Пропускаем остальную обработку

                        case NavigationOptions.Menu:
                            createMenu.ResetCreation();
                            _currentMenu = new MainMenu();
                            break;
                    }
                    continue;
                }
            }

            // Стандартная обработка ввода
            _currentMenu.HandleInput( NavigationOptions.Confirm );

            // Обработка перехода в главное меню
            if ( _currentMenu.CurrentState == MenuState.MainMenu )
            {
                _currentMenu = new MainMenu();
            }
            else if ( _currentMenu is MainMenu menu && menu.CurrentState == MenuState.Exit )
            {
                isRunning = false;
            }

            TransitionToNextMenu();
        }

        Console.WriteLine( "Выход из игры. До свидания!" );
    }

    private void TransitionToNextMenu()
    {
        if ( _currentMenu is MainMenu mainMenu )
        {
            switch ( mainMenu.CurrentState )
            {
                case MenuState.CreateFighter:
                case MenuState.CreateOpponent:
                    _currentMenu = new CreateFighterMenu
                    {
                        CurrentState = mainMenu.CurrentState,
                        FighterType = mainMenu.FighterType
                    };
                    break;
                case MenuState.Battle:
                    StartBattle();
                    _currentMenu = new MainMenu();
                    break;
            }
        }
        else if ( _currentMenu is CreateFighterMenu createMenu && createMenu.CurrentFighter != null )
        {
            _fighters.Add( createMenu.CurrentFighter );
            _currentMenu = new MainMenu();
        }
    }

    private void DisplayFighterSummary( CreateFighterMenu menu )
    {
        Console.WriteLine( "\n=== ВАШ ВЫБОР ===" );
        Console.WriteLine( $"Тип: {menu.FighterType}" );
        Console.WriteLine( $"Раса: {menu.SelectedRace?.Name ?? "не выбрано"}" );
        Console.WriteLine( $"Класс: {menu.SelectedClass?.Name ?? "не выбрано"}" );
        Console.WriteLine( $"Оружие: {menu.SelectedWeapon?.Name ?? "не выбрано"}" );
        Console.WriteLine( $"Броня: {menu.SelectedArmor?.Name ?? "не выбрано"}" );
    }

    private bool IsSelectionState()
    {
        if ( _currentMenu is CreateFighterMenu createMenu )
        {
            return createMenu.CurrentState is MenuState.SelectRace
                or MenuState.SelectClass
                or MenuState.SelectWeapon
                or MenuState.SelectArmor;
        }

        return false;
    }

    /// <summary> Обработка навигационных команд </summary>
    private bool HandleNavigation( NavigationOptions option )
    {
        switch ( option )
        {
            case NavigationOptions.Back:
                HandleBackNavigation();
                break;

            case NavigationOptions.Menu:
                _currentMenu = new MainMenu();  // Сброс к главному меню
                break;

            case NavigationOptions.Cancel:
                return false;

            default:
                // Передача управления текущему меню
                _currentMenu.HandleInput( option );
                if ( !IsInMainMenuWithoutSelection() )
                    TransitionToNextMenu();
                break;
        }

        return true;
    }

    private bool IsInMainMenuWithoutSelection()
    {
        if ( _currentMenu is MainMenu mainMenu )
        {
            return mainMenu.CurrentState == MenuState.MainMenu;
        }

        return false;
    }

    /// <summary> Запуск битвы между созданными бойцами </summary>
    private void StartBattle()
    {
        if ( _fighters.Count < 2 )
        {
            Console.WriteLine( "Недостаточно бойцов для битвы!" );
            Console.ReadKey();
            return;
        }

        // Запуск битвы на арене
        IFighter winner = _arena.StartBattle( _fighters );
        Console.WriteLine( $"\nПОБЕДИТЕЛЬ: {winner.Name}!" );
        Console.WriteLine( "Нажмите любую клавишу..." );
        Console.ReadKey();

        _fighters.Clear();  // Очистка списка для новой игры
    }

    private void HandleBackNavigation()
    {
        if ( _currentMenu is CreateFighterMenu createMenu )
        {
            if ( createMenu.StateHistory.Count > 0 )
            {
                createMenu.CurrentState = createMenu.StateHistory.Pop();
            }
            else
            {
                _currentMenu = new MainMenu();
            }
        }
        else
        {
            _currentMenu = new MainMenu();
        }
    }
}