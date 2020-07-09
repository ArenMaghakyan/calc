using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;


namespace Calc
{
    class Program
    {
        static int Main(string[] args)
        {
            var validationRegex = new Regex("^[0-9+*-/.]+$");
            var divisionByZeroRegex = new Regex("/0");
            // var validationPattern = new Regex();

            SettingAppHeader();


            while (true)
            {
                var input = Console.ReadLine();

                if (input == "x")
                {
                    break;
                }

                var isValid = ValidateInput(input);

                if (isValid)
                {
                    char[] charArr = input.ToCharArray();
                    List<char> num = new List<char>();
                    List<string> myCollection = new List<string>();
                    var isFloat = true;


                    foreach (char ch in charArr)
                    {
                        if (char.IsDigit(ch))
                        {
                            num.Add(ch);
                        } else if (ch == '.' && isFloat)
                        {
                            num.Add(ch);
                            isFloat = false;
                        } else if (ch == '.' && !isFloat)
                        {
                            ErrorHandling("Number is not in correct format");
                            break;
                        }
                        else
                        {
                            var str = new string(num.ToArray());
                            if (str.Length != 0)
                            {
                                myCollection.Add(str);
                                myCollection.Add(ch.ToString());
                                isFloat = true;
                                num.Clear();
                            }
                            else
                            {
                                ErrorHandling("Double sign is not a good idea. Think twice");
                                break;
                            }
                        }
                    }


                    var result = new DataTable().Compute(input, null).ToString();
                    Console.WriteLine(result);

                }

            }

            bool ValidateInput(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    ErrorHandling("Expression can't be empty! Check once more");
                    return false;
                }

                var isForbiddenSymbol = !validationRegex.IsMatch(input);
                
                if (isForbiddenSymbol)
                {
                    ErrorHandling("Forbidden symbol detected. Use only following symbols [0-9+-/*]");
                    return false;
                }

                var isDividedToZero = divisionByZeroRegex.IsMatch(input);

                if (isDividedToZero)
                {
                    ErrorHandling("Division to Zero is not allowed");
                    return false;
                }

                var lastSymbolNotANumber = !char.IsDigit(input.Last());

                if (lastSymbolNotANumber)
                {
                    ErrorHandling("Format is not correct. Please check last char and resubmit");
                    return false;
                }

                return true;
            }


            void SettingAppHeader()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Input expression and press Enter. Only simple operation is supported (+, -, /, *)");
                Console.WriteLine("To exit, input char x and press Enter");
                Console.ResetColor();
            }

            void ErrorHandling(string errorMessage)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(errorMessage);
                Console.ResetColor();
            }

            return 0;
        }

    }
}
