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

        public string Compute(string equation)
        {
            try
            {
                return DataTable.Compute(equation, null).ToString();
            }
            catch (Exception)
            {
                return "0";
            }
            
        }

        public double ProcessResult(List<string> choppedInput)
        {
            var lastDigit = choppedInput[choppedInput.Count - 1];

            if (!ContainsMathOperator(choppedInput))
            {
                string result = "";

                for (int i = 0; i < choppedInput.Count; i++)
                {
                    result += choppedInput[i];
                }
                
                return double.Parse(result);
            }

            Dictionary<string, double> eqMap = new Dictionary<string, double>();
            string currentKey;
            string collectedNums = "";

            for (int i = 0; i < choppedInput.Count; i++)
            {
                if (IsMathOperator(choppedInput[i]))
                {
                    currentKey = choppedInput[i];
                    eqMap.Add(currentKey, double.Parse(collectedNums));
                    collectedNums = "";
                    continue;
                }

                collectedNums += choppedInput[i];
            }

            eqMap.Add("NA", double.Parse(collectedNums == ""? "0": collectedNums));

            // TODO: process the dictionary provided u know that key is operator string and value is the left-handside
            // and the next value is the right-handside -- Example:
            /*
              12+34
               dic
               +:12
               NA:34

               =12+34

               22+38-1

               dic
               +:22
               -:38
               NA:1
             */

            return 0;

        }
    }
}
