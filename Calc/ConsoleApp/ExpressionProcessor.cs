using System.Globalization;

namespace Calculator
{
    public static class ExpressionProcessor
    {
        public static double Process( List<string> tokens )
        {
            tokens = ProccesMulDiv( tokens );
            return ProccesAddSub( tokens );
        }

        private static List<string> ProccesMulDiv( List<string> tokens )
        {
            for ( int i = 1; i < tokens.Count; )
            {
                if ( tokens[ i ] == "*" || tokens[ i ] == "/" )
                {
                    ValidateOperands( tokens, i );

                    double left = ParseDouble( tokens[ i - 1 ] );
                    double right = ParseDouble( tokens[ i + 1 ] );

                    double result;

                    try
                    {
                        result = tokens[ i ] == "*"
                            ? SafeMath.Multiply( left, right )
                            : SafeMath.Divide( left, right );
                    }
                    catch ( CalculationOverflowException )
                    {
                        throw;
                    }
                    catch ( Exception ex ) when ( ex is DivideByZeroException )
                    {
                        throw;
                    }

                    if ( tokens[ i ] == "/" && right == 0 )
                    {
                        throw new DivideByZeroException( "Деление на ноль" );
                    }

                    tokens[ i - 1 ] = result.ToString();
                    tokens.RemoveRange( i, 2 );
                }
                else
                {
                    i++;
                }
            }
            return tokens;
        }

        private static double ProccesAddSub( List<string> tokens )
        {
            double result = ParseDouble( tokens[ 0 ] );

            for ( int i = 1; i < tokens.Count; i += 2 )
            {
                if ( i + 1 >= tokens.Count )
                {
                    throw new ArgumentException( "Некоректное выражение" );
                }

                double num = ParseDouble( tokens[ i + 1 ] );

                try
                {
                    result = tokens[ i ] switch
                    {
                        "+" => SafeMath.Add( result, num ),
                        "-" => SafeMath.Subtract( result, num ),
                        _ => throw new ArgumentException( $"Недопустимая операция: {tokens[ i ]}" )
                    };
                }
                catch ( CalculationOverflowException )
                {
                    throw;
                }
            }
            return result;
        }

        private static void ValidateOperands( List<string> tokens, int index )
        {
            if ( index - 1 < 0 || index + 1 >= tokens.Count )
            {
                throw new ArgumentException( "Некоректное выражение" );
            }

            if ( !TryParseDouble( tokens[ index - 1 ], out _ ) ||
                !TryParseDouble( tokens[ index + 1 ], out _ ) )
            {
                throw new ArgumentException( "Операнды должны быть числами" );
            }
        }

        private static double ParseDouble( string str )
        {
            try
            {
                return double.Parse(
                    str,
                    NumberStyles.Float | NumberStyles.AllowExponent,
                    CultureInfo.InvariantCulture
                );
            }
            catch ( FormatException )
            {
                throw new ArgumentException( $"Некоректкный числовой формат: {str}" );
            }
            catch ( OverflowException )
            {
                throw new OverflowException( $"Число выходит за пределы диапазона: '{str}'" );
            }
        }

        private static bool TryParseDouble( string str, out double result )
        {
            return double.TryParse(
                str,
                NumberStyles.Float | NumberStyles.AllowExponent,
                CultureInfo.InvariantCulture,
                out result
            );
        }
    }
}