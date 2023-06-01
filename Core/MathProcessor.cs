using org.matheval;

namespace Calculex.Core
{
    internal class MathProcessor
    {
        public char[] AllowedOperators { get; private set; }

        public MathProcessor()
        {
            AllowedOperators = new char[5] { '+', '=', '/', '*', '^' };
        }

        public object Compute(string equation)
        {
            char lastChar = equation[equation.Length - 1];

            if (equation == "0" || equation == "(" || IsMathOperator(lastChar))
                return 0;

            object result;

            Expression e = new Expression(equation);

            try
            {
                result = e.Eval();
            }
            catch (Exception)
            {
                result = "ERR";
            }
            
            return result;
        }

        public bool IsMathOperator(char ch)
        {
            return AllowedOperators.Contains(ch);
        }
    }
}
