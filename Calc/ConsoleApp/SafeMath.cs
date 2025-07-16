using Calculator;

public static class SafeMath
{
    public static double Add( double a, double b ) => SafeOperation(
        a, b, MathOperation.Add,
        ( x, y ) => x + y );
    public static double Subtract( double a, double b ) => SafeOperation(
        a, b, MathOperation.Subtract,
        ( x, y ) => x - y );
    public static double Multiply( double a, double b ) => SafeOperation(
        a, b, MathOperation.Multiply,
        ( x, y ) => x * y );
    public static double Divide( double a, double b ) => SafeOperation(
        a, b, MathOperation.Divide,
        ( x, y ) => x / y );

    private static double SafeOperation(
        double a,
        double b,
        MathOperation operation,
        Func<double, double, double> opFunction )
    {
        if ( double.IsNaN( a ) || double.IsNaN( b ) )
        {
            throw new ArgumentException( "Операнд не является числом (NaN)" );
        }

        if ( double.IsInfinity( a ) || double.IsInfinity( b ) )
        {
            throw new ArgumentException( "Операнд является бесконечностью" );
        }

        try
        {
            if ( operation == MathOperation.Divide && b == 0 )
            {
                throw new DivideByZeroException( "Деление на ноль" );
            }

            if ( operation == MathOperation.Multiply )
            {
                if ( Math.Abs( a ) > 1 && double.MaxValue / Math.Abs( a ) < Math.Abs( b ) )
                {
                    throw new OverflowException( $"Переполнение: |{a}| * |{b}| > {double.MaxValue}" );
                }
            }

            double result = opFunction( a, b );

            if ( double.IsInfinity( result ) )
            {
                throw new OverflowException( "Результат операции выходит за пределы диапазона double" );
            }

            if ( result != 0 && Math.Abs( result ) < double.Epsilon )
            {
                throw new OverflowException( "Результат операции теряет значимость" );
            }

            return result;
        }
        catch ( OverflowException ex )
        {
            throw new CalculationOverflowException(
                a, b, operation,
                $"Переполнение при {GetOperationName( operation )}: {a} и {b}",
                ex );
        }
    }

    private static string GetOperationName( MathOperation op ) => op switch
    {
        MathOperation.Add => "сложение",
        MathOperation.Subtract => "вычитание",
        MathOperation.Multiply => "умножение",
        MathOperation.Divide => "деление",
        _ => "операции"
    };
}