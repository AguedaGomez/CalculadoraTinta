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
        public string MssgFeedback { get; set; }
        public bool CompletedOperation { get; set; }
        public string operation = "";

        readonly List<string> numbers = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        readonly List<string> multiplyOperators = new List<string>() { "x", "X", "*" };
        readonly List<string> divisionOperators = new List<string>() { "/", "%" };
        readonly string addition = "+";
        readonly string sustraction = "-";
        readonly string equal = "=";
        

        public string EvaluateString(string input)
        {
            MssgFeedback = "";
            string result = "";
            if (Mode == 1)
            {
                if (numbers.Contains(input) | multiplyOperators.Contains(input) | divisionOperators.Contains(input) | input == sustraction | input == addition | input == equal)
                {

                    if (input == equal)
                    {
                        if (numbers.Contains(operation[operation.Length - 1].ToString()))
                        {
                            result = " = " + new DataTable().Compute(operation, null).ToString();
                            operation = "";
                            CompletedOperation = true;
                        }
                        else
                        {
                            MssgFeedback = "Falta un operando en la operación introducida";
                        }

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
                        CompletedOperation = false;

                    }

                }
                else
                {
                    result = "?";
                    MssgFeedback = "Se ha introducido un caracter no permitido";
                    CompletedOperation = false;
                }
                    
            }
            else if (Mode == 2)
            {
                foreach (var c in input)
                {
                    string s = c.ToString();
                    if (numbers.Contains(s) | multiplyOperators.Contains(s) | divisionOperators.Contains(s) | s == equal | s == sustraction | s == addition)
                        if (multiplyOperators.Contains(s))
                            result += "*";
                        else if (s == equal)
                            result += "";
                        else
                            result += s;

                }
                if (result != "")
                   if (numbers.Contains(result[result.Length - 1].ToString()))
                    {
                        if (result != input & !result.Contains('*'))
                            MssgFeedback = "Se han eliminado algunos caracteres no permitidos \n La expresión original reconocida ha sido: " + input;
                        result += " = " + new DataTable().Compute(result, null).ToString();
                        CompletedOperation = true;

                    }           
                    else 
                        MssgFeedback = "Falta un operando en la operación introducida";
            }

            return result;

        }
    }
}
