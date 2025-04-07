using Prorent24.Common.Models.Columns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Prorent24.Common.Extentions
{
    public static class ModelExtention
    {
        public static Model MoveDataToModel<Model>(this Model model, List<Column> columns)
        {
            try
            {
                PropertyInfo[] modelProperies = model.GetType().GetProperties();

                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i].Key != null)
                    {
                        PropertyInfo modelProp = modelProperies.FirstOrDefault(x => x.Name.ToLower() == columns[i].Key.ToLower());

                        if (modelProp != null)
                        {
                            try
                            {
                                Type propertyField = Nullable.GetUnderlyingType(modelProp.PropertyType) ?? modelProp.PropertyType;

                                if (propertyField.IsEnum)
                                {
                                    if (System.Enum.TryParse(propertyField, columns[i].Value.ToString(), true, out object valueSet))
                                    {
                                        modelProp.SetValue(model, valueSet, null);
                                    }
                                }
                                else
                                {
                                    modelProp.SetValue(model, Convert.ChangeType(columns[i].Value, modelProp.PropertyType));
                                }
                            }
                            catch
                            {
                                //TODO: List errors for User
                            }
                        }
                    }
                }
                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Model MoveDataToModel<Model>(this Model model, PropertyInfo[] modelProperies, List<Column> columns, List<Column> columnsSelected)
        {
            try
            {
                for (int i = 0; i < modelProperies.Length; i++)
                {
                    PropertyInfo modelProp = modelProperies[i];
                    int index = columnsSelected.FindIndex(x => x.Key.ToLower().Equals(modelProp.Name.ToLower()));

                    if(index > -1)
                    {
                        Type propertyField = Nullable.GetUnderlyingType(modelProp.PropertyType) ?? modelProp.PropertyType;

                        if (propertyField.IsEnum)
                        {
                            if (System.Enum.TryParse(propertyField, columns[index].Value.ToString(), true, out object valueSet))
                            {
                                modelProperies[i].SetValue(model, valueSet, null);
                            }
                        }
                        else
                        {
                            modelProperies[i].SetValue(model, Convert.ChangeType(columns[index].Value, modelProp.PropertyType));
                        }
                    }
                }

                //for (int i = 0; i < columnsSelected.Count; i++)
                //{
                //    if (columnsSelected[i].Key != null)
                //    {
                //        PropertyInfo modelProp = modelProperies.FirstOrDefault(x => x.Name.ToLower() == columnsSelected[i].Key.ToLower());

                //        if (modelProp != null)
                //        {
                //            try
                //            {
                //                Type propertyField = Nullable.GetUnderlyingType(modelProp.PropertyType) ?? modelProp.PropertyType;

                //                if (propertyField.IsEnum)
                //                {
                //                    if (System.Enum.TryParse(propertyField, columns[i].Value.ToString(), true, out object valueSet))
                //                    {
                //                        modelProp.SetValue(model, valueSet, null);
                //                    }
                //                }
                //                else
                //                {
                //                    modelProp.SetValue(model, Convert.ChangeType(columns[i].Value, modelProp.PropertyType));
                //                }
                //            }
                //            catch
                //            {
                //                //TODO: List errors for User
                //            }
                //        }
                //    }
                //}
                return model;
            }
            catch (Exception ex)
            {
                return model;
                // throw;
            }
        }

    }
}
