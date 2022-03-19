// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConsoleApplication
{
    class Program
    {
        private enum Operator
        {
            ADDITION = '+',
            SUBSTRACTION = '-',
            MULTIPLY = '*',
            DIVIDE = '/',
        }

        private static readonly string s_enterFirstOperandMessage = "Enter first operand: ";
        private static readonly string s_enterSecondOperandMessage = "Enter second operand: ";
        private static readonly string s_enterOperatorMessage = "Enter operator: ";

        private static readonly string s_invalidOperatorMessage = InitExpectedOperatorMessage();
        private static readonly string s_overflowMessage = "Overflow happend";

        private static readonly string s_exitTerminalLiteral = "...";
        public static int Main()
        {
            Console.WriteLine("Simple calculator console app\n");

            double? firstOperand, secondOperand, result;
            Operator? operation;

            while (true)
            {
                try
                {
                    firstOperand = ReadNumberOperand(s_enterFirstOperandMessage);
                    secondOperand = ReadNumberOperand(s_enterSecondOperandMessage);

                    operation = ReadOperator(s_enterOperatorMessage);

                    result = Calculate(firstOperand, secondOperand, operation);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error: " + ex.Message + "\n");
                    continue;
                }

                WriteCalculation(firstOperand, secondOperand, operation, result);

                if (AskUserForExit())
                {
                    break;
                }
            }

            return 0;
        }

        private static double? ReadNumberOperand(string enterInputMessage)
        {
            if (enterInputMessage != null)
            {
                Console.Write(enterInputMessage);
            }

            string? userInput = Console.ReadLine();

            double result;
            try
            {
                result = Convert.ToDouble(userInput);
            }
            catch (FormatException)
            {
                throw new FormatException($"'{userInput}' is not a valid number");
            }

            if (double.IsInfinity(result))
            {
                throw new OverflowException(s_overflowMessage);
            }

            return result;
        }
        private static Operator? ReadOperator(string enterInputMessage)
        {
            if (enterInputMessage != null)
            {
                Console.Write(enterInputMessage);
            }

            string? userInput = Console.ReadLine();

            return userInput switch
            {
                "+" => Operator.ADDITION,
                "-" => Operator.SUBSTRACTION,
                "*" => Operator.MULTIPLY,
                "/" => Operator.DIVIDE,
                _ => throw new InvalidOperationException($"Invalid operator given: '{userInput}'. " + s_invalidOperatorMessage),
            };
        }

        private static double? Calculate(double? firstOperand, double? secondOperand, Operator? operation)
        {
            double? result = (char)operation.GetValueOrDefault() switch
            {
                '+' => firstOperand + secondOperand,
                '-' => firstOperand - secondOperand,
                '*' => firstOperand * secondOperand,
                '/' => firstOperand / secondOperand,
                _ => throw new InvalidOperationException($"Invalid operator given. " + s_invalidOperatorMessage),
            };
            if (result != null && double.IsInfinity((double)result))
            {
                throw new OverflowException(s_overflowMessage);
            }

            return result;
        }

        private static void WriteCalculation(double? firstOperand, double? secondOperand, Operator? operation, double? result)
        {
            if (firstOperand != null && secondOperand != null && operation != null && result != null)
            {
                Console.WriteLine($"{firstOperand:e} {(char)operation!} {secondOperand:e} = {0 + result:e}");
            }
        }

        private static bool AskUserForExit()
        {
            Console.WriteLine("Enter an empty line to continue using program or '...' to exit");
            string? userInput = Console.ReadLine();

            return userInput != null && userInput.Equals(s_exitTerminalLiteral);
        }

        private static string InitExpectedOperatorMessage()
        {
            string spaceSeparatedAllowedOperators = "";
            foreach (char operatorName in Enum.GetValues<Operator>())
            {
                spaceSeparatedAllowedOperators += "'" + operatorName + "' ";
            }

            return "Expected: " + spaceSeparatedAllowedOperators + "\n";
        }
    }
}
