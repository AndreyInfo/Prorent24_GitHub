using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Grids;
using System.Collections.Generic;
using System.Reflection;

namespace Prorent24.Common.Extentions
{
    public static class GridEntityExtention
    {
        public static BaseGrid GetBaseGridByEntity(this List<object> list, string entityKey, params string[] columns)
        {
            bool addedColumns = false;
            BaseGrid grid = new BaseGrid();
            Dictionary<string, object> keys = null;

            list.ForEach(x =>
            {
                keys = new Dictionary<string, object>();

                object includeObj = x.GetType().GetProperty(entityKey).GetValue(x);

                foreach (string column in columns)
                {
                    if (includeObj != null)
                    {
                        PropertyInfo property = includeObj.GetType().GetProperty(column);

                        if (!addedColumns)
                        {
                            grid.Columns.Add(new Column()
                            {
                                Key = property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1),
                                Type = property.PropertyType.ConvertToTypeScriptType()
                            });
                        }

                        object value = property.GetValue(includeObj);

                        keys.Add(property.Name, value);
                    }
                }

                addedColumns = true;
                grid.Data.Add(keys);
            });

            return grid;
        }
    }
}
