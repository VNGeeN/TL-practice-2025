namespace Casino
{
    public class GameProcessingService
    {
        private readonly UserDataService _userDataService;
        private readonly IDataInputService _dataInputService;
        public GameProcessingService( UserDataService userDataService, IDataInputService dataInputService )
        {
            _userDataService = userDataService;
            _dataInputService = dataInputService;
        }
        private readonly double _multiplicator = 0.10;
        const int MaxNumberInSet = 20;
        const int MinWinNumber = 18;
        const int RollDivider = MinWinNumber - 1; // 17
        public void StartGame()
        {
            if ( string.IsNullOrWhiteSpace( _userDataService.UserData.UserName ) ||
                 _userDataService.UserData.UserBalance <= 0 )
            {
                Console.WriteLine( "\nОшибка! Данные игрока не заданы или баланс равен нулю. Введите новые данные" );
                return;
            }

            Console.WriteLine( "\n---Начало игры---" );
            int bet = _dataInputService.GetPositiveInteger(
                "Введите вашу ставку: ",
                "Ставка должна быть положительным числом!"
            );

            if ( bet > _userDataService.UserData.UserBalance )
            {
                Console.WriteLine( "Ошибка! Ставка превышает текущий баланс" );
                return;
            }

            Random rand = new Random();
            int roll = rand.Next( 1, MaxNumberInSet + 1 );

            Console.WriteLine( $"Выпало число {roll}" );

            if ( roll >= MinWinNumber )
            {

                double winCash = bet * ( 1 + ( _multiplicator * ( roll % RollDivider ) ) );
                _userDataService.UserData.UserBalance += ( int )Math.Round( winCash );
                Console.WriteLine( $"Поздравляем! Вы выйграли: {Math.Round( winCash )}. Ваш новый баланс: {_userDataService.UserData.UserBalance}" );
            }
            else
            {
                _userDataService.UserData.UserBalance -= bet;
                Console.WriteLine( $"Вы проиграли. Ставка {bet} списана с баланса. Ваш новый баланс: {_userDataService.UserData.UserBalance}" );
            }
        }
    }
}