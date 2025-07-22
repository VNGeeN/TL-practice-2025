using System.Threading.Tasks.Dataflow;

namespace Casino
{
    public static class GameProcessingService
    {
        private static double _multiplicator = 0.10;
        public static void StartGame()
        {
            if ( string.IsNullOrWhiteSpace( UserDataProcessingService._userData.UserName ) ||
                 UserDataProcessingService._userData.UserBalance <= 0 )
            {
                Console.WriteLine( "\nОшибка! Данные игрока не заданы или баланс равен нулю. Введите новые данные" );
                return;
            }

            Console.WriteLine( "\n---Начало игры---" );
            int bet = DataInputService.GetPositiveInteger(
                "Введите вашу ставку: ",
                "Ставка должна быть положительным числом!"
            );

            if ( bet > UserDataProcessingService._userData.UserBalance )
            {
                Console.WriteLine( "Ошибка! Ставка превышает текущий баланс" );
                return;
            }

            var rand = new Random();
            int roll = rand.Next( 1, 21 );

            Console.WriteLine( $"Выпало число {roll}" );

            if ( roll >= 18 )
            {

                double winCash = bet * ( 1 + ( _multiplicator * ( roll % 17 ) ) );
                UserDataProcessingService._userData.UserBalance += ( int )Math.Round( winCash );
                Console.WriteLine( $"Поздравляем! Вы выйграли: {Math.Round( winCash )}. Ваш новый баланс: {UserDataProcessingService._userData.UserBalance}" );
            }
            else
            {
                UserDataProcessingService._userData.UserBalance -= bet;
                Console.WriteLine( $"Вы проиграли. Ставка {bet} списана с баланса. Ваш новый баланс: {UserDataProcessingService._userData.UserBalance}" );
            }
        }
    }
}