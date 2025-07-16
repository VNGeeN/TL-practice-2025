namespace Calculator
{
    public class CalculationOverflowException : Exception
    {
        public double Operand1 { get; }
        public double Operand2 { get; }
        public MathOperation Operation { get; }

        public CalculationOverflowException(
            double op1,
            double op2,
            MathOperation operation,
            string message,
            Exception innerException = null )
            : base( message, innerException )
        {
            Operand1 = op1;
            Operand2 = op2;
            Operation = operation;
        }
    }
}