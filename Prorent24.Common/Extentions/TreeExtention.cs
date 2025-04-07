using Prorent24.Common.Attributes;
using Prorent24.Common.Models.Excels;
using Prorent24.Common.Models.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Prorent24.Common.Models.Columns;

namespace Prorent24.Common.Extentions
{
    public static class TreeExtention
    {
        private static Localize localize = new Localize();

        /// <summary>
        /// Add Tree columns
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static void AddTreeColumns<Model>(this List<TreeColumn> trees, System.Enum columnGroup) // ColumnGroupEnum
        {
            short order = 0;
            Type entityType = typeof(Model);
            PropertyInfo[] properties = typeof(Model).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                IncludeToGrid attribute = property.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid;

                if (attribute != null && attribute.ColumnGroup != null)
                {
                    order += 1;

                    trees.Add(new TreeColumn()
                    {
                        Id = attribute.IsEntity ? attribute.EntityKey : property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1),
                        ParentId = attribute.IsEntity ? "-1" : attribute.ColumnGroup?.ToString(),
                        Text = property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1)
                        // Value = attribute.
                    });
                }
            }
        }

        public static void AddTreeColumnsGroup<Model>(this List<TreeGroupColumn> treesGroups, Dictionary<string, bool> selectedColumns = null, bool useTreeColumnAttr = false)
        {
            PropertyInfo[] properties = typeof(Model).GetProperties();

            List<IncludeToGrid> includeAttributes = properties.Select(x => x.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid).ToList();
            var groups = includeAttributes.Where(x => x != null && x.ColumnGroup != null).GroupBy(x => x.ColumnGroup).ToList();

            foreach (var group in groups)
            {
                TreeGroupColumn treeGroupColumn = new TreeGroupColumn()
                {
                    GroupName = group.Key.ToString(),
                    Collection = new List<TreeColumn>()
                };

                foreach (PropertyInfo property in properties)
                {
                    IncludeToGrid attribute = property.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid;

                    if (attribute != null && !attribute.IsSystem && attribute.ColumnGroup != null && attribute.ColumnGroup.ToString() == treeGroupColumn.GroupName)
                    {
                        string text = string.IsNullOrWhiteSpace(attribute.ColumnName) ? property.Name.Substring(0, 1).ToLower() + property.Name.Substring(1) : attribute.ColumnName;
                        bool selected = false;

                        if (selectedColumns != null && selectedColumns.Count > 0)
                        {
                            selectedColumns.TryGetValue(text, out selected);
                        }
                        else
                        {
                            selected = attribute.IsDisplay;
                        }

                        if (!useTreeColumnAttr)
                        {
                            treeGroupColumn.Collection.Add(new TreeColumn()
                            {
                                Id = attribute.IsEntity ? attribute.EntityKey : text,
                                ParentId = attribute.IsEntity ? "-1" : attribute.ColumnGroup?.ToString(),
                                Text = text,
                                Type = property.PropertyType.ConvertToTypeScriptType(),
                                Selected = selected,
                                IsRequiredForImport = attribute.Required
                            });
                        }
                        else if (useTreeColumnAttr && attribute.TreeColumn)
                        {
                            treeGroupColumn.Collection.Add(new TreeColumn()
                            {
                                Id = attribute.IsEntity ? attribute.EntityKey : text,
                                ParentId = attribute.IsEntity ? "-1" : attribute.ColumnGroup?.ToString(),
                                Text = text,
                                Type = property.PropertyType.ConvertToTypeScriptType(),
                                Selected = selected,
                                IsRequiredForImport = attribute.Required,
                                Order = attribute.TreeColumnOrder
                            });
                        }
                    }
                }

                if (treeGroupColumn.Collection.Count > 0)
                {
                    treesGroups.Add(treeGroupColumn);

                    if (useTreeColumnAttr)
                    {
                        treesGroups.ForEach(x =>
                        {
                            x.Collection = x.Collection.OrderBy(o => o.Order).ToList();
                        });
                    }
                }
            }
        }

        public static void AddRequiredFields<Model>(this List<string> requiredFields)
        {
            PropertyInfo[] properties = typeof(Model).GetProperties();
            List<string> fields = properties.Where(x => (x.GetCustomAttribute(typeof(IncludeToGrid)) as IncludeToGrid)?.Required == true).Select(x => x.Name).ToList();
            requiredFields.AddRange(fields);
            // return requiredFields;
        }

        // compare template columns with prepared

        public static ExcelCustomWorksheet ExctractColumns<Model>(this ExcelCustomWorksheet excelCustomWorksheet)
        {
            // columns structure
            List<Column> columns = new List<Column>();
            columns.AddColumns<Model>(null, true);
            // prepare keys for compare
            columns.ForEach(x =>
            {
                x.Key = localize[x.Key.ToLower()];

                if (x.Required)
                {
                    x.Key = string.Concat(x.Key, " *");
                }

                if (x.Values != null)
                {
                    // перевірити тип - enum!!!
                    var values = (x.Values) as IList<string>;
                    x.Key = string.Concat(x.Key, $" { localize["values"]} ({string.Join(",", values)})");
                }
            });

            if (excelCustomWorksheet.Rows != null && excelCustomWorksheet.Rows.Count >= 1)
            {
                ExcelCustomRow row = new ExcelCustomRow();
                excelCustomWorksheet.Rows[0].Cells.ForEach(x =>
                {
                    // знаходимо колонку яка відповідає тому, що бачимо в таблиці
                    var _columns = columns.Where(y => y.Key == x.Value.ToString()).Select(z => z);
                    var column = _columns.Any() ? _columns.FirstOrDefault() : new Column() { Key = x.Key, OriginalKey = "-1" }; // з таким працює клієнта і не тільки - клієнт перевести на empty
                    row.Cells.Add(column);
                    x.Key = column.OriginalKey;
                });

                excelCustomWorksheet.Columns.Add(row);
            }

            return excelCustomWorksheet;
        }
    }
}
