using System.Data;
using NCalc;

namespace Calculex.Core
{

    // Will redo the entire way calculations r done.
    internal class MathProcessor
    {
        public string[] AllowedOperators { get; private set; }

        public MathProcessor()
        {
            AllowedOperators = new string[5] { "+", "-", "/", "*", "^" };
        }

        public dynamic Compute(string equation)
        {
            Expression e = new Expression(equation);
            dynamic result = (dynamic)(e.Evaluate());
            return result;
        }

        public bool IsMathOperator(string ch)
        {
            return AllowedOperators.Contains(ch);
        }
    }
}
