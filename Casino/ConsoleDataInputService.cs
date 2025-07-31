public class ConsoleDataInputService : IDataInputService
{
    public string GetNonEmptyString( string prompt, string errorMessage )
    {
        bool isValid = false;
        string? input = null;
        while ( !isValid )
        {
            Console.Write( prompt );
            input = Console.ReadLine()?.Trim();
            isValid = !string.IsNullOrWhiteSpace( input );
            if ( !isValid )
            {
                Console.WriteLine( errorMessage );
            }
        }
        return input!;
    }

    public int GetPositiveInteger( string prompt, string errorMessage )
    {
        int result;
        bool isPositiveInteger = false;

        do
        {
            Console.Write( prompt );
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
}