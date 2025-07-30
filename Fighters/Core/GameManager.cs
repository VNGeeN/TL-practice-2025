using Fighters.Core;
using Fighters.UI.Enums;
using Fighters.UI.Menus;
using Fighters.Entities.Characters;
public class GameManager
{
    private MenuBase _currentMenu;          // Текущее активное меню
    private readonly List<IFighter> _fighters = new List<IFighter>();  // Список созданных бойцов
    private readonly Arena _arena = new Arena();  // Арена для проведения битв

    public GameManager()
    {
        _currentMenu = new MainMenu();  // Начинаем с главного меню
    }

    /// <summary> Основной игровой цикл </summary>
    public void Run()
    {
        bool isRunning = true;

        while ( isRunning )
        {
            _currentMenu.Display();

            if ( IsSelectionState() )
            {
                _currentMenu.HandleInput( NavigationOptions.Confirm );
                continue;
            }
            else
            {
                _currentMenu.HandleInput( NavigationOptions.Confirm );
            }

            Console.WriteLine( "\nОпции навигации:" );
            Console.WriteLine( "1. Подтвердить" );
            Console.WriteLine( "2. Назад" );
            Console.WriteLine( "3. Главное меню" );
            Console.WriteLine( "4. Отмена" );
            Console.Write( "Выберите действие: " );

            var navInput = Console.ReadLine();

            if ( Enum.TryParse( navInput, out NavigationOptions option ) )
            {
                isRunning = HandleNavigation( option );
            }
            else
            {
                Console.WriteLine( "Неверный ввод. Попробуйте снова." );
                Console.ReadKey();
            }
        }

        Console.WriteLine( "Выход из игры. До свидания!" );
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
                TransitionToNextMenu();  // Переход к следующему состоянию
                break;
        }

        return true;
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
        var winner = _arena.StartBattle( _fighters );
        Console.WriteLine( $"\nПОБЕДИТЕЛЬ: {winner.Name}!" );
        Console.WriteLine( "Нажмите любую клавишу..." );
        Console.ReadKey();

        _fighters.Clear();  // Очистка списка для новой игры
    }

    private void HandleBackNavigation()
    {
        if ( _currentMenu is CreateFighterMenu createMenu )
        {
            if ( createMenu.CurrentState != MenuState.CreateFighter &&
                createMenu.CurrentState != MenuState.CreateOpponent )
            {
                createMenu.CurrentState = createMenu.PreviousState;
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
}