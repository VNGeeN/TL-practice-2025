namespace Calculator
{
    public static class Tokenizer
    {
        public static List<string> Tokenize( string input )
        {
            List<string> tokens = new List<string>();
            int i = 0;

            while ( i < input.Length )
            {
                if ( char.IsDigit( input[ i ] ) || input[ i ] == '.' )
                {
                    tokens.Add( ParseNumber( input, ref i ) );
                }
                else if ( "+-*/".Contains( input[ i ] ) )
                {
                    tokens.Add( ParseOperator( input, tokens, ref i ) );
                }
                else if ( char.IsWhiteSpace( input[ i ] ) )
                {
                    i++;
                }
                else
                {
                    throw new ArgumentException( $"Недопустимый символ: '{input[ i ]}'" );
                }
            }
            return tokens;
        }

        private static string ParseNumber( string input, ref int index )
        {
            string num = "";
            bool hasDot = false;
            bool hasExponent = false;

            while ( index < input.Length )
            {
                char c = input[ index ];
                if ( char.IsDigit( c ) )
                {
                    num += c;
                    index++;
                }
                else if ( input[ index ] == '.' && !hasDot && !hasExponent )
                {
                    hasDot = true;
                    num += c;
                    index++;
                }
                else if ( ( c == 'e' || c == 'E' ) && !hasExponent )
                {
                    hasExponent = true;
                    num += c;
                    index++;

                    if ( index < input.Length && ( input[ index ] == '+' || input[ index ] == '-' ) )
                    {
                        num += input[ index ];
                        index++;
                    }
                }
                else
                {
                    break;
                }
            }

            if ( num.EndsWith( "e", StringComparison.OrdinalIgnoreCase ) ||
                num.EndsWith( "e+", StringComparison.OrdinalIgnoreCase ) ||
                num.EndsWith( "e-", StringComparison.OrdinalIgnoreCase ) )
            {
                throw new ArgumentException( $"Некоректное число: '{num}' - отсутствует экспонента" );
            }

            if ( num == "." || num.EndsWith( "." ) && !hasExponent )
            {
                throw new ArgumentException( $"Некоректное число: '{num}'" );
            }

            return num;
        }

        private static string ParseOperator( string input, List<string> tokens, ref int index )
        {
            char current = input[ index ];

            if ( current == '-' && ( tokens.Count == 0 || "+-*/".Contains( tokens[ ^1 ] ) ) )
            {
                index++;
                return "-" + ParseNumber( input, ref index );
            }

            index++;
            return current.ToString();
        }
    }
}