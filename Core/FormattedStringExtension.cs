
using System.Runtime.CompilerServices;

namespace Calculex.Core
{
    internal static class FormattedStringExtension
    {
        public static void ClearSpans(this FormattedString fs)
        {
            for (int i = 0; i < fs.Spans.Count; i++)
            {
                fs.Spans.RemoveAt(i);
            }
        }
    }
}
