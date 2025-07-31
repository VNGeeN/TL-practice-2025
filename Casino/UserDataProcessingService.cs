namespace Casino
{
    public class UserDataProcessingService
    {
        private readonly UserDataService _userDataService;
        private readonly IDataInputService _dataInputService;

        private int _currentStep = 0;

        private readonly string[] _steps = {
            "Ввод имени игрока",
            "Ввод баланса игрока",
            "Подтверждение"
        };

        public UserDataProcessingService(
            UserDataService userDataService,
            IDataInputService dataInputService )
        {
            _userDataService = userDataService;
            _dataInputService = dataInputService;
        }

        public void StartUserDataProcess()
        {
            _currentStep = 0;
            while ( _currentStep < _steps.Length )
            {
                bool shouldContinue = ProcessCurrentStep();
                if ( !shouldContinue ) return;
            }
        }

        private bool ProcessCurrentStep()
        {
            Console.WriteLine( $"\nШаг {_currentStep + 1}: {_steps[ _currentStep ]}" );

            if ( _currentStep == ( int )UserDataStep.Confirmation )
            {
                return ProcessConfirmationStep();
            }
            ProcessStep();
            return true;
        }

        private void ProcessStep()
        {
            switch ( _currentStep )
            {
                case ( int )UserDataStep.UserName:
                    _userDataService.UserData.UserName = _dataInputService.GetNonEmptyString(
                        "Введите имя игрока: ",
                        "Ошибка! Имя игрока не может быть пустым. Пожалуйста, введите значение."
                    );
                    break;
                case ( int )UserDataStep.UserBalance:
                    _userDataService.UserData.UserBalance = _dataInputService.GetPositiveInteger(
                        "Введите баланс игрока: ",
                        "Ошибка! Значение должно быть целым положительным числом."
                    );
                    break;
            }
            _currentStep++;
        }

        private bool ProcessConfirmationStep()
        {
            List<string> validationErrors = ValidateUserData( _userDataService.UserData );

            if ( validationErrors.Any() )
            {
                Console.WriteLine( "\nОшибки в данных игрока:" );
                foreach ( string error in validationErrors )
                {
                    Console.WriteLine( $"- {error}" );
                }

                return true;
            }

            Console.WriteLine( "Введённые данные:" );
            ShowUserData( _userDataService.UserData );

            Program.ReturnToMainMenu();
            return false;
        }

        public void ShowUserData( UserGameData userData )
        {
            Console.WriteLine( "\n---Данные игрока---" );
            Console.WriteLine( $"Имя игрока: {userData.UserName}" );
            Console.WriteLine( $"Баланс игрока: {userData.UserBalance}" );
        }

        private List<string> ValidateUserData( UserGameData userData )
        {
            List<string> errors = new List<string>();

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