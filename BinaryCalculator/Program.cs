using System;

namespace BinaryCalculator
{
    class Program
    {
        static Random random = new Random();

        const int BINARY_MAX_BITS = 8;
        const int BINARY_START_INDEX = 7;

        const int RANDOM_DECIMAL_MIN = 0;
        const int RANDOM_DECIMAL_MAX = 127;

        static char menuOption;
        static bool isMenuOptionValid;

        const char BINARY_TO_DECIMAL = 'A';
        const char DECIMAL_TO_BINARY = 'B';
        const char BINARY_ADD = 'C';
        const char RANDOM_ADDITION = 'D';

        static string menu = $"Operations:\n{BINARY_TO_DECIMAL}) Convert Binary -> Decimal\n{DECIMAL_TO_BINARY}) Convert Decimal -> Binary\n{BINARY_ADD}) Binary Addition\n{RANDOM_ADDITION}) Random Addition";

        static string inputValue1;
        static string inputValue2;

        static string resultValue1;

        static int carried = 0;

        static void Main(string[] args)
        {
            ProgramIntro();

            DisplayMenu();
            RetrieveMenuOption();

            DoCalculations();
            DisplayCalculations();

            ProgramOutro();
        }

        static void ProgramIntro()
        {
            Console.Clear();

            Console.WriteLine("Binary Calculator App\nPress any key to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        static void DisplayMenu()
        {
            Console.WriteLine(menu);
            Console.WriteLine();
        }

        static void RetrieveMenuOption()
        {
            string menuEntry;

            Console.Write("Option: ");
            menuEntry = Console.ReadLine().ToUpper();

            if (char.TryParse(menuEntry, out menuOption))
            {
                CheckOptionValidity();
            }
        }

        static void DoCalculations()
        {
            if (!isMenuOptionValid)
            {
                return;
            }

            switch (menuOption)
            {
                case BINARY_TO_DECIMAL:
                BinaryToDecimalLogic();
                break;

                case DECIMAL_TO_BINARY:
                DecimalToBinaryLogic();
                break;

                case BINARY_ADD:
                BinaryAdditionLogic();
                break;
            }
        }

        static void CheckOptionValidity()
        {
            isMenuOptionValid =
                menuOption == BINARY_TO_DECIMAL ||
                menuOption == DECIMAL_TO_BINARY ||
                menuOption == BINARY_ADD ||
                menuOption == RANDOM_ADDITION;
        }

        static void DisplayCalculation(string input, string explanation, string result)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(input);
            Console.ResetColor();

            Console.Write(explanation);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(result);
            Console.ResetColor();
        }

        static void DisplayCalculations()
        {
            if (!isMenuOptionValid)
            {
                return;
            }

            Console.WriteLine();

            switch (menuOption)
            {
                case BINARY_TO_DECIMAL:
                DisplayCalculation($"({inputValue1})2", " was converted  to ", $"({resultValue1})10");
                break;

                case DECIMAL_TO_BINARY:
                DisplayCalculation($"({inputValue1})10", " was converted to ", $"({resultValue1})2");
                break;

                case BINARY_ADD:
                DisplayCalculation($"   {inputValue1}\n + {inputValue2}", "\n ----------", $"\n   {resultValue1}");
                break;

                case RANDOM_ADDITION:
                DisplayRandomBinary();
                break;
            }

            Console.WriteLine("\n\n");
        }

        static void ProgramOutro()
        {
            if (!isMenuOptionValid)
            {
                Console.WriteLine();
                Console.WriteLine($"An invalid option was entered.");
                Console.WriteLine();
            }

            Console.WriteLine("Thank you for using Binary Calculator!");
            Console.WriteLine("Press any key to close the app.");

            Console.ReadKey();
        }

        static void BinaryToDecimalLogic()
        {
            Console.Write("Enter a binary number (max length: 8-bits): ");
            inputValue1 = Console.ReadLine();

            inputValue1 = OptimizeBinaryNumber(inputValue1);
            resultValue1 = BinaryToDecimal(inputValue1).ToString();
        }

        static void BinaryAdditionLogic()
        {
            Console.Write("Enter a binary number (max length: 7-bits): ");
            inputValue1 = Console.ReadLine();

            Console.Write("Enter another binary number (max length: 7-bits): ");
            inputValue2 = Console.ReadLine();

            inputValue1 = OptimizeBinaryNumber(inputValue1);
            inputValue2 = OptimizeBinaryNumber(inputValue2);

            resultValue1 = BinaryAdd(inputValue1, inputValue2);
        }

        static void DecimalToBinaryLogic()
        {
            Console.Write("Decimal Number (range: 0-255): ");
            inputValue1 = Console.ReadLine();

            resultValue1 = DecimalToBinary(int.Parse(inputValue1));
            resultValue1 = OptimizeBinaryNumber(resultValue1);
        }

        static int ConvertBinaryDigitToDecimal(char bin, int pow)
        {
            int result = 0;

            if (bin == '1')
            {
                if (pow == 0)
                {
                    result = 1;
                }
                else
                {
                    result = int.Parse(bin.ToString()) * (int)Math.Pow(2, pow);
                }
            }

            return result;
        }

        static int BinaryToDecimal(string binary)
        {
            int sum = 0;
            int binPow = 0;

            int index = BINARY_START_INDEX;

            sum += ConvertBinaryDigitToDecimal(binary[index], binPow);

            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);
            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);
            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);
            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);
            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);
            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);
            sum += ConvertBinaryDigitToDecimal(binary[--index], ++binPow);

            return sum;
        }

        static string EvaluateDecimalToBinary(int decimalNum, string currentResult)
        {
            int remainder = decimalNum % 2;

            return currentResult.PadLeft(currentResult.Length + 1, remainder.ToString()[0]);
        }

        static string DecimalToBinary(int decimalNum)
        {
            string result = string.Empty;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);
            decimalNum /= 2;

            result = EvaluateDecimalToBinary(decimalNum, result);

            return result;
        }

        static string OptimizeBinaryNumber(string number)
        {
            if (number.Length < BINARY_MAX_BITS)
            {
                number = number.PadLeft(number.Length + BINARY_MAX_BITS - number.Length, '0');
            }

            return number;
        }

        static string EvaluateBinaryCol(string curResult, int col, string binA, string binB)
        {
            int aDigit = int.Parse(binA[col].ToString());
            int bDigit = int.Parse(binB[col].ToString());

            int sum = aDigit + bDigit + carried;

            switch (sum)
            {
                case 0:
                curResult = curResult.PadLeft(curResult.Length + 1, '0');
                carried = 0;
                break;

                case 1:
                curResult = curResult.PadLeft(curResult.Length + 1, '1');
                carried = 0;
                break;

                case 2:
                curResult = curResult.PadLeft(curResult.Length + 1, '0');
                carried = 1;
                break;

                case 3:
                curResult = curResult.PadLeft(curResult.Length + 1, '1');
                carried = 1;
                break;
            }

            return curResult;
        }

        static string BinaryAdd(string binary1, string binary2)
        {
            string result = string.Empty;

            int index = BINARY_START_INDEX;

            result = EvaluateBinaryCol(result, index, binary1, binary2);

            result = EvaluateBinaryCol(result, --index, binary1, binary2);
            result = EvaluateBinaryCol(result, --index, binary1, binary2);
            result = EvaluateBinaryCol(result, --index, binary1, binary2);
            result = EvaluateBinaryCol(result, --index, binary1, binary2);
            result = EvaluateBinaryCol(result, --index, binary1, binary2);
            result = EvaluateBinaryCol(result, --index, binary1, binary2);
            result = EvaluateBinaryCol(result, --index, binary1, binary2);

            return result;
        }

        static void DisplayRandomBinary()
        {
            int decimal1 = random.Next(RANDOM_DECIMAL_MIN, RANDOM_DECIMAL_MAX + 1);
            int decimal2 = random.Next(RANDOM_DECIMAL_MIN, RANDOM_DECIMAL_MAX + 1);

            string decimal1Binary = DecimalToBinary(decimal1);
            string decimal2Binary = DecimalToBinary(decimal2);

            decimal1Binary = OptimizeBinaryNumber(decimal1Binary);
            decimal2Binary = OptimizeBinaryNumber(decimal2Binary);

            string binarySum = BinaryAdd(decimal1Binary, decimal2Binary);
            int decimalSum = BinaryToDecimal(binarySum);

            DisplayCalculation($"({decimal1})10", " was converted to ", $"({decimal1Binary})2\n");
            DisplayCalculation($"({decimal2})10", " was converted to ", $"({decimal2Binary})2\n\n");

            DisplayCalculation($"  {decimal1Binary}\n+ {decimal2Binary}\n", "----------", $"\n  {binarySum}");

            DisplayCalculation($"\n\n({binarySum})2", " was converted to ", $"({decimalSum})10");
        }
    }
}