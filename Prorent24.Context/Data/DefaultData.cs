using Newtonsoft.Json;
using Prorent24.Entity.Base;
using Prorent24.Entity.Configuration;
using Prorent24.Entity.Configuration.CustomerCommunication;
using Prorent24.Entity.Configuration.Financial;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Directory;
using Prorent24.Enum.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Prorent24.Context.Data
{
    public static class DefaultData
    {
        private class ListFiles
        {
            public EntityEnum Key { get; set; }
            public string Value { get; set; }
        }

        private static List<ListFiles> files = new List<ListFiles>();

        public static void Initialize(this Prorent24DbContext context)
        {
            AddListFiles();

            PropertyInfo[] properties = context.GetType().GetProperties();
            Type baseEntity = typeof(BaseEntity);
            Assembly assembly = Assembly.GetAssembly(baseEntity);

            files = files.OrderBy(x => x.Key).ToList();

            foreach (var file in files)
            {
                switch (file.Key)
                {
                    case EntityEnum.UserRoleEntity:
                        {

                            //list<prorent24.entity.role> roles = jsonconvert.deserializeobject<list<prorent24.entity.role>>(file.value);
                            //foreach(var item in roles)
                            //{
                            //    context.entry<prorent24.entity.role>(item);
                            //}

                            //context.savechanges();
                            break;
                        }
                    case EntityEnum.DirectoryCurrencyEntity:
                        {
                            context.DirectoryInitialize(file, DirectoryTypeEnum.Currency);
                            break;
                        }
                    case EntityEnum.DirectoryCountryEntity:
                        {
                            context.DirectoryInitialize(file, DirectoryTypeEnum.Country);
                            break;
                        }
                    case EntityEnum.DirectoryLanguageEntity:
                        {
                            context.DirectoryInitialize(file, DirectoryTypeEnum.Language);
                            break;
                        }
                    case EntityEnum.DirectoryTimeZoneEntity:
                        {
                            context.DirectoryInitialize(file, DirectoryTypeEnum.TimeZone);
                            break;
                        }
                    case EntityEnum.SystemSettingEntity:
                        {

                            context.SystemSettingsInitialize(file);
                            break;
                        }
                    case EntityEnum.VatClassSchemeRateEntity:
                        {
                            if (context.VatClassSchemeRates.Any()) break;

                            var vatClasses = context.VatClasses.ToList();
                            var vatSchemes = context.VatSchemes.ToList();
                            var vatClassSchemeRates = JsonConvert.DeserializeObject<List<VatClassSchemeRateEntity>>(file.Value);

                            for (int i = 0; i < vatClassSchemeRates.Count; i++)
                            {
                                VatClassSchemeRateEntity vatClassSchemeRateEntity = vatClassSchemeRates[i];
                                vatClassSchemeRateEntity.VatClassId = i > 2 ? vatClasses[0].Id : vatClasses[i].Id;
                                vatClassSchemeRateEntity.VatSchemeId = vatSchemes.First(x => x.Type == vatClassSchemeRateEntity.Type).Id;
                                context.VatClassSchemeRates.Add(vatClassSchemeRateEntity);
                            }

                            context.SaveChanges();
                            break;
                        }
                    case EntityEnum.FactorGroupOptionEntity:
                        {
                            if (context.FactorGroupOptions.Any()) break;

                            var factorGroup = context.FactorGroups.First();
                            var factorGroupOptionEntities = JsonConvert.DeserializeObject<List<FactorGroupOptionEntity>>(file.Value);

                            for (int i = 0; i < factorGroupOptionEntities.Count; i++)
                            {
                                FactorGroupOptionEntity factorGroupOptionEntity = factorGroupOptionEntities[i];
                                factorGroupOptionEntity.FactorGroupId = factorGroup.Id;
                                context.FactorGroupOptions.Add(factorGroupOptionEntity);
                            }

                            context.SaveChanges();
                            break;
                        }
                    case EntityEnum.DocumentTemplateEntity:
                        {
                            if (context.DocumentTemplates.Any()) break;

                            List<DocumentTemplateEntity> docTemplates = JsonConvert.DeserializeObject<List<DocumentTemplateEntity>>(file.Value);
                            if (docTemplates != null)
                            {
                                var directoryRu = context.Directories.Where(x => (x.Type == DirectoryTypeEnum.Country && x.Locs.Any(c => c.Name == "Россия")) ||
                                                                  (x.Type == DirectoryTypeEnum.Language && x.Locs.Any(l => l.Name == "Русский"))).ToList();

                                var directoryEn = context.Directories.Where(x => x.Type == DirectoryTypeEnum.Language && x.Locs.Any(l => l.Name == "English")).ToList();

                                int countryId = directoryRu.First(x => x.Type == DirectoryTypeEnum.Country).Locs.FirstOrDefault().DirectoryId;
                                int languageIdRu = directoryRu.First(x => x.Type == DirectoryTypeEnum.Language).Locs.FirstOrDefault().DirectoryId;
                                int languageIdEn = directoryEn.First(x => x.Type == DirectoryTypeEnum.Language).Locs.FirstOrDefault().DirectoryId;

                                docTemplates.ForEach(item =>
                                {
                                    item.CountryId = countryId;
                                    item.LanguageId = item.LanguageId == 0 ? languageIdRu : languageIdEn;
                                });

                                context.DocumentTemplates.AddRange(docTemplates);
                                context.SaveChanges();
                            }

                            break;
                        }
                    default:
                        {
                            context.AutoInitialize(assembly, file);
                            break;
                        }
                }

            }
        }

        #region Private

        private static void AddListFiles()
        {
            string[] directories = Directory.GetFiles("AppData");
            for (int i = 0; i < directories.Length; i++)
            {
                string directory = directories[i];

                string entityName = Path.GetFileName(directory).Replace(".json", "");
                string value = File.ReadAllText(directory);

                EntityEnum key = System.Enum.Parse<EntityEnum>(entityName);

                files.Add(new ListFiles()
                {
                    Key = key,
                    Value = value
                });
            }
        }

        private static void AutoInitialize(this Prorent24DbContext context, Assembly assembly, ListFiles file)
        {
            Type type = assembly.ExportedTypes.FirstOrDefault(x => x.Name.ToLower() == file.Key.ToString().ToLower());
            object entity = Activator.CreateInstance(type);
            type = entity.GetType();
            List<object> entities = new List<object>();

            var items = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(file.Value);

            foreach (var item in items)
            {
                object newEntity = Activator.CreateInstance(type);

                foreach (var data in item)
                {
                    PropertyInfo propertyInfo = type.GetProperty(data.Key);

                    if (propertyInfo != null)
                    {
                        Type propertyField = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

                        if (propertyField.IsEnum)
                        {
                            object valueSet = System.Enum.Parse(propertyField, data.Value.ToString(), true);
                            propertyInfo.SetValue(newEntity, valueSet, null);
                        }
                        else
                        {
                            propertyInfo.SetValue(newEntity, Convert.ChangeType(data.Value, propertyField));
                        }
                    }
                }

                entities.Add(newEntity);
            }

            try
            {
                context.AddRange(entities);
                context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        private static void DirectoryInitialize(this Prorent24DbContext context, ListFiles file, DirectoryTypeEnum type)
        {
            if (context.Directories.Where(x => x.Type == type).Any()) return;

            var directories = JsonConvert.DeserializeObject<List<Prorent24.Entity.Directory.DirectoryEntity>>(file.Value);
            context.Directories.AddRange(directories);
            context.SaveChanges();
        }

        private static void SystemSettingsInitialize(this Prorent24DbContext context, ListFiles file)
        {
            if (context.SystemSettings.Any()) return;

            List<SystemSettingEntity> systemSettingEntities = JsonConvert.DeserializeObject<List<SystemSettingEntity>>(file.Value);
            var vatClasses = context.VatClasses.ToList();
            var vatScheme = context.VatSchemes.First();

            if (systemSettingEntities != null)
            {
                systemSettingEntities.ForEach(item =>
                {
                    switch (item.Type)
                    {
                        case SystemSettingsTypeEnum.FinancialSetting:
                            {

                                var data = new
                                {
                                    CurrencySign = "$",
                                    DefaultExpirationPeriodQuotationContract = 14,
                                    DefaultDueTermInvoiceReminders = 14,
                                    DefaultVatSchemeId = vatScheme.Id,
                                    DefaultVatClassId = vatClasses[2].Id,
                                    DefaultTaxClassCrewFunctionId = vatClasses[2].Id,
                                    DefaultTaxClassTransportFunctionId = vatClasses[2].Id,
                                    VatClassInsuranceId = vatClasses[0].Id,

                                    PrintOnLetterhead = true,
                                    SendTermsConditionsWithQuotations = true,
                                    SendTermsConditionsWithContracts = true,
                                    SendTermsConditionsWithInvoices = true
                                };

                                item.Value = JsonConvert.SerializeObject(data);

                                break;
                            }
                    }
                });
                context.SystemSettings.AddRange(systemSettingEntities);
            }

            context.SaveChanges();
        }

        #endregion


    }
}
