namespace Calculex.Core
{
    public static class ConfigHolder
    {
        public static string path { 
            get 
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "equations.json");
            }
        }
    }
}
