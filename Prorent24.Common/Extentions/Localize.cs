using Newtonsoft.Json;
using Prorent24.Common.ApplicationSettings;
using Prorent24.Common.Models.Columns;

namespace Prorent24.Common.Extentions
{
    public class Localize
    {
        ColumnLocalize columnLocalize = null;
        public Localize()
        {
            if (columnLocalize == null)
            {
                string pathJson = string.Concat(DirectorySettings.frontPathToI18n, "ru.json");
                string text = System.IO.File.ReadAllText(pathJson);
                columnLocalize = JsonConvert.DeserializeObject<ColumnLocalize>(text);
            }
        }

        public string this[string key]
        {
            get
            {
                string value;
                columnLocalize.Entity.TryGetValue(key, out value);
                return !string.IsNullOrWhiteSpace(value) ? value : key;
            }
            set { }
        }
    }
}
