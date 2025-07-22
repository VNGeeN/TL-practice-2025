public static class DataInputService
{
    public static int GetPositiveInteger(
        string message,
        string errorMessage = "Введите целое положительное число" )
    {
        int result;
        bool isPositiveInteger = false;

        do
        {
            Console.Write( message );
            var input = Console.ReadLine();

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
        while ( true )
        {
            Console.Write( message );
            var input = Console.ReadLine()?.Trim();
            if ( !string.IsNullOrWhiteSpace( input ) )
            {
                return input;
            }

            Console.WriteLine( errorMessage );
        }
    }

    public static NavigationOptions GetNavigationChoice(
        string message,
        Dictionary<NavigationOptions, string> options )
    {
        Console.WriteLine( message );
        Console.WriteLine( "Варианты действий:" );

        foreach ( var option in options )
        {
            Console.WriteLine( $"{( int )option.Key}. {option.Value}" );
        }

        int min = options.Keys.Min( o => ( int )o );
        int max = options.Keys.Max( o => ( int )o );
        int choice = GetValidatedInput( "Выберите действие ", min, max );

        return ( NavigationOptions )choice;
    }

    public static int GetValidatedInput( string message, int minValue, int maxValue )
    {
        int input;
        while ( true )
        {
            Console.Write( message );
            if ( int.TryParse( Console.ReadLine(), out input ) && input >= minValue && input <= maxValue )
            {
                return input;
            }
            Console.WriteLine( $"Ошибка! Введите число от {minValue} до {maxValue}" );
        }
    }
}