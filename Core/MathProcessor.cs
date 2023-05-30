using System.Data;

namespace Calculex.Core
{
    internal class MathProcessor
    {
        public string[] AllowedNumbers { get; private set; }
        public string[] AllowedOperators { get; private set; }
        public DataTable DataTable { get; set; }

        public MathProcessor()
        {
            AllowedNumbers = new string[10];
            for (int i = 0; i < 10; i++)
            {
                AllowedNumbers[i] = i.ToString();
            }
            AllowedOperators = new string[5] { "+", "-", "/", "*", "^" };
            DataTable = new DataTable();
        }
        public bool CharacterIsAllowed(string ch)
        {
            bool isAllowed;
            if (AllowedNumbers.Contains(ch) || AllowedOperators.Contains(ch))
            {
                isAllowed = true;
            }
            else
            {
                isAllowed = false;
            }

            return isAllowed;
        }

        public bool IsMathOperator(string ch)
        {
            return AllowedOperators.Contains(ch);
        }

        public bool ContainsMathOperator(string str)
        {
            bool result = false;
            foreach (var ch in str)
            {
                if (AllowedOperators.Contains(ch.ToString()))
                {
                    result = true;
                }
            }

            return result;
        }

        public bool ContainsMathOperator(List<string> strList)
        {
            bool result = false;

            foreach (var digit in strList)
            {
                if (IsMathOperator(digit))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public double ProcessResult(List<string> choppedInput)
        {
            if (!ContainsMathOperator(choppedInput))
            {
                string result = "";

                for (int i = 0; i < choppedInput.Count; i++)
                {
                    result += choppedInput[i];
                }
                
                return double.Parse(result);
            }

            string lastChar = choppedInput[choppedInput.Count - 1];

            if (IsMathOperator(lastChar))
            {
                return 0;
            }

            var eqMap = MapEquation(choppedInput);

            return CalculateDict(eqMap);
        }

        private Dictionary<int, KeyValuePair<string, double>> MapEquation(List<string> choppedInput)
        {
            // what happens when i write code without thinking 2 seconds into the future, then attempt to fix it with my feet.

            Dictionary<int, KeyValuePair<string, double>> eqMap = new Dictionary<int, KeyValuePair<string, double>>();

            string currentKey;
            string collectedNums = "";

            for (int i = 0; i < choppedInput.Count; i++)
            {
                if (IsMathOperator(choppedInput[i]))
                {
                    currentKey = choppedInput[i];
                    eqMap.Add(i, new KeyValuePair<string, double>(currentKey, double.Parse(collectedNums)));
                    collectedNums = "";
                    continue;
                }

                collectedNums += choppedInput[i];
            }

            // using choppedInput.Count because it is never reached in the forloop therefore always going to be unique,
            // i hope.
            var leftOver = new KeyValuePair<string, double>("NA", double.Parse(collectedNums == "" ? "0" : collectedNums));
            eqMap.Add(choppedInput.Count, leftOver);

            return eqMap;
        }

        private double CalculateDict(Dictionary<int, KeyValuePair<string, double>> mappedEquation)
        {
            int p = 0;
            var pair = mappedEquation.ElementAt(p).Value;
            var mathOp = pair.Key;
            double result = pair.Value;

            do
            {
                p++;
                var pair2 = mappedEquation.ElementAt(p).Value;
                var num2 = pair2.Value;
                result = ExecuteComputation(mathOp, result, num2);
                mathOp = pair2.Key;
            } while (p < mappedEquation.Count - 1);

            return result;
        }

        private double ExecuteComputation(string op, double num1, double num2)
        {
            switch (op)
            {
                case "+":
                    return num1 + num2;
                case "-":
                    return num1 - num2;
                case "/":
                    return num1 / num2;
                case "*":
                    return num1 * num2;
                case "^":
                    return Math.Pow(num1, num2);

                default:
                    return 0;
            }
        }
    }
}
