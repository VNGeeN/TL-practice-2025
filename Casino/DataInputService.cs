namespace Casino
{
    public static class DataInputService
    {
        public static MenuOperations GetMenuChoice( Dictionary<MenuOperations, string> options )
        {
            int min = options.Keys.Min( o => ( int )o );
            int max = options.Keys.Max( o => ( int )o );
            int choice = GetValidatedInput( "Выберите действие: ", min, max );

            return ( MenuOperations )choice;
        }

        public static int GetPositiveInteger(
            string message,
            string errorMessage = "Введите целое положительное число" )
        {
            int result;
            bool isPositiveInteger = false;

            do
            {
                Console.Write( message );
                string? input = Console.ReadLine();

                if ( int.TryParse( input, out result ) && result > 0 )
                {
                    isPositiveInteger = true;
                }
                else
                {
                    Console.WriteLine( errorMessage );
                }
            }
            while ( !isPositiveInteger );

            return result;
        }

        public static string GetNonEmptyString(
            string message,
            string errorMessage = "Это поле обязательно для заполнения" )
        {
            bool isValid = false;
            string? input = null;
            while ( !isValid )
            {
                Console.Write( message );
                input = Console.ReadLine()?.Trim();
                isValid = !string.IsNullOrWhiteSpace( input );
                if ( !isValid )
                {
                    Console.WriteLine( errorMessage );
                }
            }
            return input!;
        }

        public static int GetValidatedInput( string message, int minValue, int maxValue )
        {
            int input;
            bool isValid;
            do
            {
                Console.Write( message );
                isValid = int.TryParse( Console.ReadLine(), out input ) && input >= minValue && input <= maxValue;
                if ( !isValid )
                {
                    Console.WriteLine( $"Ошибка! Введите число от {minValue} до {maxValue}" );
                }
            } while ( !isValid );
            return input;
        }
    }
}