using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculex.Core
{
    internal class MathProcessor
    {
        public string[] AllowedNumbers { get; private set; }
        public string[] AllowedOperators { get; private set; }

        public MathProcessor()
        {
            AllowedNumbers = new string[10];
            for (int i = 0; i < 10; i++)
            {
                AllowedNumbers[i] = i.ToString();
            }
            AllowedOperators = new string[5] { "+", "-", "/", "*", "^" };
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
    }
}
