public static class OrderProcessingService
{
    private static OrderData _currentOrder = new();
    private static int _currentStep = 0;
    private static int _pastStep = 0;
    private static readonly string[] _steps = {
        "Ввод названия товара",
        "Ввод количества",
        "Ввод имени",
        "Ввод адреса",
        "Подтверждение"
    };

    public static void StartOrderProcess( Action backToMenuAction )
    {
        ResetOrder();

        while ( _currentStep < _steps.Length )
        {
            var shouldContinue = ProcessCurrentStep( backToMenuAction );
            if ( !shouldContinue ) return;
        }

        FinalizeOrder( backToMenuAction );

    }

    public static void ResetOrder()
    {
        _currentOrder = new OrderData();
        _currentStep = 0;
    }

    private static bool ProcessCurrentStep( Action backToMenuAction )
    {
        Console.WriteLine( $"\nШаг {_currentStep + 1}: {_steps[ _currentStep ]}" );

        switch ( _currentStep )
        {
            case 0: return ProcessProductStep();
            case 1: return ProcessCountStep();
            case 2: return ProcessNameStep();
            case 3: return ProcessAddressStep();
            case 4: return ProcessConfirmationStep( backToMenuAction );
            default: return true;
        }
    }

    private static bool ProcessProductStep()
    {
        _currentOrder.Product = DataInputService.GetNonEmptyString(
            "Введите название товара: ",
            "Ошибка! Название товара не может быть пустым. Пожалуйста, введите значение."
        );
        _currentStep++;
        return true;
    }

    private static bool ProcessCountStep()
    {
        _currentOrder.Count = DataInputService.GetPositiveInteger(
            "Введите количество товара: ",
            "Ошибка! Количество должно быть целым положительным числом."
        );
        _currentStep++;
        return true;
    }

    private static bool ProcessNameStep()
    {
        _currentOrder.Name = DataInputService.GetNonEmptyString(
            "Введите ваше имя: ",
            "Ошибка! Имя не может быть пустым. Пожалуйста, введите значение."
        );
        _currentStep++;
        return true;
    }

    private static bool ProcessAddressStep()
    {
        _currentOrder.Address = DataInputService.GetNonEmptyString(
            "Введите адрес доставки: ",
            "Ошибка! Адрес доставки не может быть пустым. Пожалуйста, введите значение."
        );
        _currentStep++;
        return true;
    }

    private static bool ProcessConfirmationStep( Action backToMenuAction )
    {
        var validationErrors = ValidateOrder( _currentOrder );

        if ( validationErrors.Any() )
        {
            Console.WriteLine( "\nОшибки в данных заказа:" );
            foreach ( var error in validationErrors )
            {
                Console.WriteLine( $"- {error}" );
            }

            _currentStep = FindFirstInvalidStep( _currentOrder );
            return true;
        }

        var response = ConfirmationService.ConfirmOrder( _currentOrder );

        switch ( response )
        {
            case NavigationOption.Confirm:
                _currentStep++;
                return true;
            case NavigationOption.Back:
                ShowEditOptions();
                return true;
            case NavigationOption.Menu:
                backToMenuAction();
                return false;
            case NavigationOption.Cancel:
                Console.WriteLine( "Заказ отменен." );
                backToMenuAction();
                return false;
            default:
                return true;
        }
    }

    private static void ShowEditOptions()
    {
        Console.WriteLine( "\nКакое поле вы хотите изменить?" );
        Console.WriteLine( "1. Название товара" );
        Console.WriteLine( "2. Количество товара" );
        Console.WriteLine( "3. Имя" );
        Console.WriteLine( "4. Адрес" );
        Console.WriteLine( "5. Вернуться к подтверждению" );

        var choice = DataInputService.GetValidatedInput(
            "Выберите поле: ",
            minValue: 1,
            maxValue: 5
        );

        if ( choice == 5 ) return;

        int pastStep = _currentStep;
        _currentStep = choice - 1;

        bool continueProcessing = ProcessEditStep();

        if ( !continueProcessing )
        {
            return;
        }

        _currentStep = pastStep;
        return;
    }

    private static bool ProcessEditStep()
    {
        switch ( _currentStep )
        {
            case 0:
                Console.WriteLine( $"Текущее значение: {_currentOrder.Product}" );
                _currentOrder.Product = DataInputService.GetNonEmptyString(
                    "Введите новое название товара: ",
                    "Ошибка! Название товара не может быть пустым."
                );
                return true;

            case 1:
                Console.WriteLine( $"Текущее значение: {_currentOrder.Count}" );
                _currentOrder.Count = DataInputService.GetPositiveInteger(
                    "Введите новое количество товара: ",
                    "Ошибка! Количество должно быть целым положительным числом."
                );
                return true;

            case 2:
                Console.WriteLine( $"Текущее значение: {_currentOrder.Name}" );
                _currentOrder.Name = DataInputService.GetNonEmptyString(
                    "Введите новое имя: ",
                    "Ошибка! Имя не может быть пустым."
                );
                return true;

            case 3:
                Console.WriteLine( $"Текущее значение: {_currentOrder.Address}" );
                _currentOrder.Address = DataInputService.GetNonEmptyString(
                    "Введите новый адрес доставки: ",
                    "Ошибка! Адрес доставки не может быть пустым."
                );
                return true;

            default:
                return true;
        }
    }

    private static List<string> ValidateOrder( OrderData order )
    {
        var errors = new List<string>();

        if ( string.IsNullOrWhiteSpace( order.Product ) )
        {
            errors.Add( "Название товара не заполнено" );
        }

        if ( order.Count <= 0 )
        {
            errors.Add( "Количество товара должно быть положительным" );
        }

        if ( string.IsNullOrWhiteSpace( order.Name ) )
        {
            errors.Add( "Имя пользователя не указано" );
        }

        if ( string.IsNullOrWhiteSpace( order.Address ) )
        {
            errors.Add( "Адрес доставки не указан" );
        }

        return errors;
    }

    private static int FindFirstInvalidStep( OrderData order )
    {
        if ( string.IsNullOrWhiteSpace( order.Product ) ) return 0;
        if ( order.Count <= 0 ) return 1;
        if ( string.IsNullOrWhiteSpace( order.Name ) ) return 2;
        if ( string.IsNullOrWhiteSpace( order.Address ) ) return 3;
        return 4; // Если все заполнено, остаемся на подтверждении
    }

    private static void FinalizeOrder( Action backToMenuAction )
    {
        OrderDeliveryService.SuccesfullOrder( _currentOrder );

        var response = DataInputService.GetNavigationChoice
        (
            "\nЗаказ оформлен! Что вы ходите сделать дальше? ",
            new Dictionary<NavigationOption, string>
            {
                { NavigationOption.Confirm, "Создать новый заказ" },
                { NavigationOption.Back, "Вернуться в главное меню" }
            }
        );

        if ( response == NavigationOption.Confirm )
        {
            StartOrderProcess( backToMenuAction );
        }
        else
        {
            backToMenuAction();
        }
    }
}