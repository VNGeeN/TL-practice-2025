namespace Casino
{
    public static class UserDataProcessingService
    {
        public static UserGameData _userData = new();

        private static int _currentStep = 0;

        private static readonly string[] _steps = {
            "Ввод имени игрока",
            "Ввод баланса игрока",
            "Подтверждение"
        };

        public static void StartUserDataProcess()
        {
            while ( _currentStep < _steps.Length )
            {
                var shouldContinue = ProccessCurrentStep();
                if ( !shouldContinue ) return;
            }
        }

        private static bool ProccessCurrentStep()
        {
            Console.WriteLine( $"\nШаг {_currentStep + 1}: {_steps[ _currentStep ]}" );
            switch ( _currentStep )
            {
                case 0: return ProcessUserNameStep();
                case 1: return ProcessUserBalanceStep();
                case 2: return ProcessConfirmationStep();
                default: return true;
            }
        }

        private static bool ProcessUserNameStep()
        {
            _userData.UserName = DataInputService.GetNonEmptyString(
                "Введите имя игрока: ",
                "Ошибка! Имя игрока не может быть пустым. Пожалуйста, введите значение."
            );
            _currentStep++;
            return true;
        }

        private static bool ProcessUserBalanceStep()
        {
            _userData.UserBalance = DataInputService.GetPositiveInteger(
                "Введите баланс игрока: ",
                "Ошибка! Значение должно быть целым положительным числом."
            );
            _currentStep++;
            return true;
        }

        private static bool ProcessConfirmationStep()
        {
            var validationErrors = ValidateUserData( _userData );

            if ( validationErrors.Any() )
            {
                Console.WriteLine( "\nОшибки в данных игрока:" );
                foreach ( var error in validationErrors )
                {
                    Console.WriteLine( $"- {error}" );
                }

                return true;
            }

            Console.WriteLine( "Введённые данные:" );
            ShowUserData();

            Program.ReturnToMainMenu();
            return false;
        }

        public static void ShowUserData()
        {
            Console.WriteLine( "\n---Данные игрока---" );
            Console.WriteLine( $"Имя игрока: {_userData.UserName}" );
            Console.WriteLine( $"Баланс игрока: {_userData.UserBalance}" );
        }

        private static List<string> ValidateUserData( UserGameData userData )
        {
            var errors = new List<string>();

            if ( string.IsNullOrWhiteSpace( userData.UserName ) )
            {
                errors.Add( "Имя игрока не заполнено" );
            }

            if ( userData.UserBalance <= 0 )
            {
                errors.Add( "баланс должно быть положительным" );
            }

            return errors;
        }
    }
}