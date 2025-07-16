public static class DataInputService
{
    public static int GetPositiveInteger(
        string message,
        string errorMessage = "Это поле обязательно для заполнения" )
    {
        int result;

        while ( true )
        {
            Console.Write( message );
            var input = Console.ReadLine();

            if ( int.TryParse( input, out result ) && result > 0 )
            {
                return result;
            }
            Console.WriteLine( errorMessage );
        }
    }
    public static string GetNonEmptyString(
        string message,
        string errorMessage = "Введите целое положительное число" )
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

    public static NavigationOption GetNavigationChoice(
        string message,
        Dictionary<NavigationOption, string> options )
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

        return ( NavigationOption )choice;
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