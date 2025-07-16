namespace Calculator
{
    class Programm
    {
        static void Main()
        {
            Console.WriteLine( "Калькулятор (поддерживает + - * / и отрицательные числа)" );
            Console.WriteLine( "Введите выражение: " );

            string input = Console.ReadLine()?.Trim() ?? "";

            if ( string.IsNullOrEmpty( input ) )
            {
                Console.WriteLine( "Пустой ввод" );
                return;
            }

            try
            {
                List<string> tokens = Tokenizer.Tokenize( input );
                double result = ExpressionProcessor.Process( tokens );
                Console.WriteLine( $"Резудьтат: {result}" );
            }
            catch ( CalculationOverflowException ex )
            {
                Console.WriteLine( $"Ошибка переполнения: {ex.Message}" );
                Console.WriteLine( $"Операция: {ex.Operation}" );
                Console.WriteLine( $"Операнды: {ex.Operand1}, {ex.Operand2}" );
            }
            catch ( ArgumentException ex )
            {
                Console.WriteLine( $"Ошибка вывода: {ex.Message}" );
            }
            catch ( Exception ex )
            {
                Console.WriteLine( $"Неизвестная ошибка: {ex.Message}" );
            }
        }
    }
}


// EnterMess();
// var operand1 = GetOperator();
// string @operator = Console.ReadLine();
// var operand2 = GetOperator();
// if (@operator == "+")
// {
//     var result = operand1 + operand2;
//     Console.WriteLine($"{result}");
// }
// else if (@operator == "-")
// {
//     var result = operand1 - operand2;
//     Console.WriteLine($"{result}");
// }
// else
// {
//     Console.WriteLine($"Unsuported operator, your input {@operator} ");
// }

// static void EnterMess()
// {
//     Console.WriteLine($"input a calc function with 2 numbers: ");
// }

// int GetOperator()
// {
//     var inStr = Console.ReadLine();
//     bool isParsed = int.TryParse(inStr, out int number);

//     if (isParsed)
//     {
//         Console.WriteLine($"Your number is: {number}.");
//     }
//     else
//     {
//         Console.WriteLine($"String is not a number, youre input is: {inStr}.");
//     }
//     return number;
// }

// // //обьявления переменных
// // string name = "Ivan";
// // int age = 19;

// // //обьединение строк
// // static void Main2() {
// //     string name = "buba";
// //     int age = 20;

// //     Console.WriteLine($"Name: {name}");
// //     Console.WriteLine($"Age : {age}");
// // }

// // Main2();


