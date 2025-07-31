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