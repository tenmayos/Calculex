using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace Calculex.Core
{
    internal class EquationSaver
    {
        private string Name;
        private bool IsFavorite;
        private FormattedString FormattedEquation;

        public EquationSaver(string name, bool isFavorite = false, FormattedString formattedEquation)
        {
            Name = name;
            IsFavorite = isFavorite;
            FormattedEquation = formattedEquation;
        }

        public void SaveToDisk()
        {
            using (StreamWriter sw = new StreamWriter(ConfigHolder.path))
            {
                string serializedJson = JsonSerializer.Serialize(this);
                sw.Write(serializedJson);
            }
        }
    }
}
