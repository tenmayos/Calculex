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
    }
}
