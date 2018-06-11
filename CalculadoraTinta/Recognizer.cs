using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace CalculadoraTinta
{
    public class Recognizer
    {
        public int Mode { get; set; }
        readonly List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        readonly List<string> multiplyOperators = new List<string>() { "x", "X", "*" };
        readonly List<string> divisionOperators = new List<string>() { "/", "%" };
        readonly string addition = "+";
        readonly string sustraction = "-";
        readonly string equal = "=";
        string operation = "";

        public string EvaluateString(string input)
        {
            string result = "";
            if (Mode == 1)
            {
                if (numbers.Contains(input) | multiplyOperators.Contains(input) | divisionOperators.Contains(input) | input == sustraction | input == addition | input == equal)
                {

                    if (input == equal)
                    {

                        result = " = " + new DataTable().Compute(operation, null).ToString() + "\n";
                        operation = " ";
                    }
                    else
                    {
                        if (numbers.Contains(input))
                        {
                            result = input;
                            operation += input;
                        }
                        else if (multiplyOperators.Contains(input))
                        {
                            operation += "*";
                            result += " * ";
                        }
                        else
                        {
                            operation += input;
                            result += " " + input + " ";
                        }

                    }

                }
                else
                    result = "?";
            }
            else if (Mode == 2)
            {
                foreach (var c in input)
                {
                    string s = c.ToString();
                    if (numbers.Contains(s) | multiplyOperators.Contains(s) | divisionOperators.Contains(s) | s == equal | s == sustraction | s == addition)
                        if (multiplyOperators.Contains(s))
                            result += "*";
                        else
                            result += s;
 
                }
                result += " = " + new DataTable().Compute(result, null).ToString() + "\n";
            }
               
            return result;

        }
    }
}
