using Newtonsoft.Json;
using Prorent24.Common.Attributes;
using Prorent24.Common.Models.Columns;
using Prorent24.Common.Models.Grids;
using Prorent24.Common.Models.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Prorent24.Common.Extentions
{
    public static class GridExtention
    {
        public static BaseGrid CreateGrid<Model>(this List<Model> list, List<UserColumn> userColumns = null)
        {
            BaseGrid grid = new BaseGrid();

            grid.Columns.AddColumns<Model>(userColumns);

            grid.Columns = grid.Columns.OrderBy(x => x.Order).ToList();

            grid.Data.AddData<Model>(list);

            grid.Hierarchy = AddHierarchy<Model>();

            return grid;
        }

        /// <summary>
        /// Add Columns
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static void AddColumns<Model>(this List<Column> columns, List<UserColumn> userColumns = null, bool imported = false)
        {
            Type entityType = typeof(Model);
            PropertyInfo[] properties = typeof(Model).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                IncludeToGrid attribute = property.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid;

                if (attribute != null && (!imported || imported && attribute.Imported))
                {
                    string key = string.Empty;
                    string originKey = string.Empty;

                    if (!string.IsNullOrWhiteSpace(attribute.DisplayName))
                    {
                        originKey = attribute.DisplayName;
                        key = attribute.DisplayName.Substring(0, 1).ToLower() + attribute.DisplayName.Substring(1);
                    }
                    else if (!string.IsNullOrWhiteSpace(attribute.ColumnName))
                    {
                        originKey = attribute.ColumnName;
                        key = attribute.ColumnName.Substring(0, 1).ToLower() + attribute.ColumnName.Substring(1);
                    }
                    else
                    {
                        originKey = property.Name;
                        key = property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1);
                    }

                    UserColumn userColumn = userColumns != null && userColumns.Count > 0 ? userColumns.FirstOrDefault(x => x.Column.ToLower().Trim() == originKey.ToLower().Trim()) : null;

                    Column column = new Column()
                    {
                        EntityKey = attribute.IsEntity ? attribute.EntityKey : null,
                        EntityKeyType = attribute.IsEntity ? attribute.EntityKeyType : null,
                        Type = property.PropertyType.ConvertToTypeScriptType(attribute.IncludeType, attribute.KeyType),
                        Key = key,
                        OriginalKey = key,
                        IsDisplayName = attribute.IsDisplay,
                        Order = attribute.Order,
                        IsEntity = attribute.IsEntity,
                        Required = attribute.Required,
                    };

                    if (property.PropertyType.IsEnum)
                    {
                        var values = property.PropertyType.ToKayValue().Select(x => x.Key).ToList();
                        column.Values = values;
                    }


                    if (userColumn != null)
                    {
                        column.IsDisplayName = userColumn.ShowColumn;
                        column.Order = userColumn.Order > -1 ? userColumn.Order : attribute.Order;
                        column.Width = userColumn.WidthColumn;
                    }

                    if (!columns.Any(x=>x.Key == key))
                    {
                        columns.Add(column);
                    }
                }
            }
        }


        /// <summary>
        /// Add Columns
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static void AddColumns<Model>(this List<Column> columns, string[] userColumns)
        {
            Type entityType = typeof(Model);
            PropertyInfo[] properties = typeof(Model).GetProperties();

            var propertyAttributes = properties.Select(x => new { name =  x.Name, attribute = x.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid}).Where(x => x.attribute != null).ToList();

            foreach (string _column in userColumns)
            {
                string key = string.Empty;

                var propAttribute = propertyAttributes.FirstOrDefault(x => x.name.ToLower() == _column.ToLower());
                var attribute = propAttribute.attribute;

                // такої колонки не знайдено
                if (attribute == null) continue;

                if (!string.IsNullOrWhiteSpace(attribute.DisplayName))
                {
                    key = attribute.DisplayName.Substring(0, 1).ToLower() + attribute.DisplayName.Substring(1);
                }
                else if (!string.IsNullOrWhiteSpace(attribute.ColumnName))
                {
                    key = attribute.ColumnName.Substring(0, 1).ToLower() + attribute.ColumnName.Substring(1);
                }
                else
                {
                    key = propAttribute.name.Substring(0, 1).ToLower() + propAttribute.name.Substring(1);
                }

                Column column = new Column()
                {
                    EntityKey = attribute.IsEntity ? attribute.EntityKey : null,
                    EntityKeyType = attribute.IsEntity ? attribute.EntityKeyType : null,
                    //Type = property.PropertyType.ConvertToTypeScriptType(attribute.IncludeType, attribute.KeyType),
                    Key = key,
                    OriginalKey = key,
                };

                if(!columns.Any(x=>x.Key == column.Key))
                {
                    columns.Add(column);
                }
            }
        }


        public static void AddColumnsByGroup<Model>(this List<Column> columns, string groupName)
        {
            short order = 0;
            Type entityType = typeof(Model);
            PropertyInfo[] properties = typeof(Model).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                IncludeToGrid attribute = property.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid;

                if (attribute != null && attribute.ColumnGroup != null && attribute.ColumnGroup.ToString() == groupName)
                {
                    order += 1;

                    string key = string.Empty;

                    if (!string.IsNullOrWhiteSpace(attribute.DisplayName))
                    {
                        key = attribute.DisplayName.Substring(0, 1).ToLower() + attribute.DisplayName.Substring(1);
                    }
                    else if (!string.IsNullOrWhiteSpace(attribute.ColumnName))
                    {
                        key = attribute.ColumnName.Substring(0, 1).ToLower() + attribute.ColumnName.Substring(1);
                    }
                    else
                    {
                        key = property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1);
                    }

                    if (columns.Any(x => x.Key != key))
                    {
                        columns.Add(new Column()
                        {
                            EntityKey = attribute.IsEntity ? attribute.EntityKey : null,
                            EntityKeyType = attribute.IsEntity ? attribute.EntityKeyType : null,
                            Type = property.PropertyType.ConvertToTypeScriptType(attribute.IncludeType),
                            Key = key,
                            IsDisplayName = attribute.IsDisplay,
                            Order = attribute.Order > -1 ? attribute.Order : order,
                            IsEntity = attribute.IsEntity,
                            Required = attribute.Required
                        });
                    } 
                }
            }
        }

        /// <summary>
        /// Add Rows
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="rows"></param>
        /// <param name="list"></param>
        public static void AddData<Model>(this List<Dictionary<string, object>> data, List<Model> list)
        {
            Localize localize = new Localize();

            foreach (Model item in list)
            {
                Dictionary<string, object> keys = new Dictionary<string, object>();
                PropertyInfo[] properties = item.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    IncludeToGrid attribute = property.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid;
                    JsonConverterAttribute converterAttribute = property.GetCustomAttribute(typeof(JsonConverterAttribute)) as JsonConverterAttribute;
                    if (attribute != null)
                    {
                        object value = property.GetValue(item);

                        try
                        {
                            string key = string.Empty;

                            if (!string.IsNullOrWhiteSpace(attribute.DisplayName))
                            {
                                key = attribute.DisplayName;
                            }
                            else if (!string.IsNullOrWhiteSpace(attribute.ColumnName))
                            {
                                key = attribute.ColumnName.Substring(0, 1).ToLower() + attribute.ColumnName.Substring(1);
                            }
                            else
                            {
                                key = property.Name;
                            }


                            if (attribute.IsKey && string.IsNullOrEmpty(attribute.KeyType))
                            {
                                keys.Add(key, Convert.ToInt32(value));
                            }
                            else if (attribute.IsKey && attribute.KeyType == "string")
                            {
                                keys.Add(key, value?.ToString());
                            }
                            else if (attribute.IsKey && attribute.KeyType == "enum")
                            {
                                keys.Add(key, value?.ToString().ToLower());
                            }
                            else
                            {
                                // enum
                                if (converterAttribute != null && value != null)
                                {
                                    string resultValue = localize[value?.ToString().ToLower()];
                                    keys.Add(key, resultValue);
                                }
                                else
                                {
                                    keys.Add(key.IsNormalized() ? key.Substring(0, 1).ToLower() + key.Substring(1) : key, attribute.IncludeType == "dateshort" ? (value != null ? Convert.ToDateTime(value).Date.ToString("dd/MM/yyyy") : null) : value);
                                }
                            }
                        }
                        catch { }
                    }
                }

                data.Add(keys);
            }
        }

        public static TreeHierarchy AddHierarchy<Model>()
        {
            GridOptions attribute = typeof(Model).GetCustomAttribute(typeof(GridOptions)) as GridOptions;
            if (attribute != null)
            {
                return new TreeHierarchy() { Root = attribute.HierarchyRoot };
            }
            return null;
        }
    }
}
