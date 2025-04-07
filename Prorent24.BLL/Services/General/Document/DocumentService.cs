using DinkToPdf;
using DinkToPdf.Contracts;
using Prorent24.BLL.Models;
using Prorent24.BLL.Models.Configuration.Settings;
using Prorent24.BLL.Models.General.Document;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Project;
using Prorent24.BLL.Services.Configuration.Financial.Payment;
using Prorent24.BLL.Services.Configuration.Settings.ProjectType;
using Prorent24.BLL.Services.Project;
using Prorent24.BLL.Transfers.General;
using Prorent24.Common.Models.Filters;
using Prorent24.DAL.Storages.Configuration.CustomerCommunication.DocumentTemplate;
using Prorent24.DAL.Storages.General.Document;
using Prorent24.Entity.General;
using Prorent24.Enum.Configuration;
using Prorent24.Enum.Entity;
using Prorent24.Enum.Invoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Prorent24.BLL.Extentions;
using Prorent24.DAL.Storages.Invoice;
using Prorent24.BLL.Transfers.Invoice;
using Prorent24.BLL.Services.Configuration.AccountConfiguration.CompanyDetails;
using Prorent24.BLL.Services.Project.Financial;
using Prorent24.BLL.Services.Project.Equipment;
using System.Linq;
using Prorent24.BLL.Services.Project.Planning;
using Prorent24.Enum.Project;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Services.Project.AdditionalCost;

namespace Prorent24.BLL.Services.General.Document
{
    internal class DocumentService : IDocumentService
    {
        private IConverter _converter;

        private readonly IDocumentStorage _documentStorage;
        private readonly IDocumentTemplateStorage _documentTemplateStorage;

        private readonly IProjectService _projectService;
        private readonly IProjectTypeService _projectTypeService;
        private readonly IProjectFinancialService _projectFinancialService;
        private readonly IProjectEquipmentService _projectEquipmentService;
        private readonly IPaymentConditionService _paymentConditionService;
        private readonly ICompanyDetailsService _companyDetailsService;
        private readonly IProjectPlanningService _projectPlanningService;
        private readonly IProjectAdditionalCostService _projectAdditionalCostService;


        private readonly IInvoiceStorage _invoiceStorage;

        public DocumentService(
            IConverter converter,
            IDocumentStorage documentStorage,
            IDocumentTemplateStorage documentTemplateStorage,
            IProjectService projectService,
            IProjectTypeService projectTypeService,
            IProjectFinancialService projectFinancialService,
            IProjectEquipmentService projectEquipmentService,
            IProjectPlanningService projectPlanningService,
            IProjectAdditionalCostService projectAdditionalCostService,
            IPaymentConditionService paymentConditionService,
            ICompanyDetailsService companyDetailsService,
            IInvoiceStorage invoiceStorage
            )
        {
            _documentStorage = documentStorage;
            _converter = converter;
            _documentTemplateStorage = documentTemplateStorage;
            _projectService = projectService;
            _projectTypeService = projectTypeService;
            _projectFinancialService = projectFinancialService;
            _projectEquipmentService = projectEquipmentService;
            _projectAdditionalCostService = projectAdditionalCostService;
            _paymentConditionService = paymentConditionService;
            _invoiceStorage = invoiceStorage;
            _companyDetailsService = companyDetailsService;
            _projectPlanningService = projectPlanningService;
        }
        public async Task<byte[]> GenerateDocument(InvoiceDto invoice)
        {
            DocumentDto document = (await _documentStorage.GetAllByForeignId(EntityEnum.InvoiceEntity, invoice.Id)).TransferToListDto().FirstOrDefault();
            return await this.GetDocumentFileById(document.Id);
        }

        public async Task<byte[]> GetDocumentFileById(int id)
        {
            DocumentEntity documentEntity = await _documentStorage.GetById(id);
            DocumentDto document = documentEntity.TransferToDto();

            switch (document.DocumentType)
            {
                case DocumentTemplateTypeEnum.Invoice:
                    var invoice = await InvoiceDocumentFromTemplate(document); //await InvoiceDocument(document);
                    return invoice;
                    break;
                case DocumentTemplateTypeEnum.Quotation:
                    var quotation = await QuotationDocumentFromTemplate(document);
                    return quotation;
                    break;
                case DocumentTemplateTypeEnum.Contract:
                    var contract = await ContractDocument(document);
                    return contract;
                    break;
                default:
                    // return not defined
                    break;
            }


            return new byte[0];
            // return new byte[0];
        }

        private async Task<byte[]> InvoiceDocument(DocumentDto document)
        {
            string content = "";
            CompanyDetailsDto company = await _companyDetailsService.GetAsync();
            document.Company = company;


            content += document.Company?.GetCompanyBlockInfo();
            content += document.Invoice?.GetInvoiceBlockInfo();
            content += document.Invoice?.Client?.GetContactBlockInfo();

            //content += document.Project?.ClientContact?.GetContactBlockInfo();
            //content += document.Invoice?.Client?.GetContactBlockInfo();
            if (document.Project != null)
            {
                //ProjectDto project = await _projectService.GetById(document.Project.Id);
                //ProjectFinancialDto projectFinancial = await _projectFinancialService.GetByProjectId(document.Project.Id);
                List<ProjectFinancialCategoryDto> projectFinancialCategories = await _projectFinancialService.GetCategoriesByProject(document.Project.Id);
                document.ProjectFinancialCategory = projectFinancialCategories.Where(x => x.Category == ProjectFinancialCategoryEnum.TotalExcludeVat).FirstOrDefault();

                // EQUIPMENTS
                document.Equipments = await _projectEquipmentService.GetEquipmentsByProjectId(document.Project.Id);
                content += document.Equipments?.GetEquipmentBlock();

                //CREW MEMBERS
                document.PlannedCrewMembers = await _projectPlanningService.GetPlanningGroupedFunctions(document.Project.Id, ProjectFunctionTypeEnum.Crew);
                if (document.PlannedCrewMembers != null && document.PlannedCrewMembers.Count() > 0)
                {
                    content += document.PlannedCrewMembers.GetCrewMemberBlock();
                }

                //TRANSPORT
                document.PlannedTransport = await _projectPlanningService.GetPlanningGroupedFunctions(document.Project.Id, ProjectFunctionTypeEnum.Transport);

                if (document.PlannedTransport != null && document.PlannedTransport.Count() > 0)
                {
                    content += document.PlannedTransport.GetTransportBlock();
                }
            }

            content += document.Invoice?.InvoiceLines?.GetInvoiceLineBlockInfo();
            // GetInvoiceProjectInfo
            content += document.Invoice?.GetInvoiceTotalInfo();
            content = content.GetInvoiceTemplate();

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    //Out = document.FileName,
                    //DocumentTitle = document.FileName,
                    Margins = new MarginSettings(){
                        Unit = Unit.Millimeters,
                        Bottom = 30, //document?.Letterhead?.BottomMargin,
                        Left = 32, //document?.Letterhead?.LeftMargin,
                        Right = 32, //document?.Letterhead?.RightMargin,
                        Top = 32, //document.Letterhead?.TopMargin,
                    },
                },
                Objects = {
                    new ObjectSettings() {
                        // Page ="-",
                        //PagesCount = true,
                        HtmlContent = content,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            if (!Directory.Exists(@"wwwroot\Documents"))
            {
                Directory.CreateDirectory(@"wwwroot\Documents");
            }

            using (FileStream stream = new FileStream(@"wwwroot\Documents\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

            return pdf;
        }

        private async Task<byte[]> InvoiceDocumentFromTemplate(DocumentDto document)
        {

            CompanyDetailsDto company = await _companyDetailsService.GetAsync();
            document.Company = company;
            // prepare Document data
            if (document.Project != null)
            {
                List<ProjectFinancialCategoryDto> projectFinancialCategories = await _projectFinancialService.GetCategoriesByProject(document.Project.Id);
                document.ProjectFinancialCategory = projectFinancialCategories.Where(x => x.Category == ProjectFinancialCategoryEnum.TotalExcludeVat).FirstOrDefault();

                // EQUIPMENTS

                document.Equipments = await _projectEquipmentService.GetEquipmentsByProjectId(document.Project.Id);

                //CREW MEMBERS
                document.PlannedCrewMembers = await _projectPlanningService.GetPlanningGroupedFunctions(document.Project.Id, ProjectFunctionTypeEnum.Crew);

                //TRANSPORT
                document.PlannedTransport = await _projectPlanningService.GetPlanningGroupedFunctions(document.Project.Id, ProjectFunctionTypeEnum.Transport);

                document.AdditionalCosts = await _projectAdditionalCostService.GetAdditionalCosts(document.Project.Id);


                ProjectFinancialDto financial = await _projectFinancialService.GetByProjectId(document.ProjectId.Value);
                document.FinancialInfo = financial;
            }

            string content = DocumentTemplatesExtention.GetInvoiceHTML(document);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    //Out = document.FileName,
                    //DocumentTitle = document.FileName,
                    Margins = new MarginSettings(){
                        Unit = Unit.Millimeters,
                        Bottom = 12, //document?.Letterhead?.BottomMargin,
                        Left = 8, //document?.Letterhead?.LeftMargin,
                        Right = 8, //document?.Letterhead?.RightMargin,
                        Top = 12, //document.Letterhead?.TopMargin,
                    },
                },
                Objects = {
                    new ObjectSettings() {
                        // Page ="-",
                        //PagesCount = true,
                        HtmlContent = content,
                        WebSettings = { DefaultEncoding = "utf-8" },
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            if (!Directory.Exists(@"wwwroot\Documents"))
            {
                Directory.CreateDirectory(@"wwwroot\Documents");
            }

            using (FileStream stream = new FileStream(@"wwwroot\Documents\" + document.FileName + ".pdf", FileMode.OpenOrCreate))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

            return pdf;
        }

        private async Task<byte[]> QuotationDocument(DocumentDto document)
        {
            string content = "";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    //Out = document.FileName,
                    //DocumentTitle = document.FileName,
                    Margins = new MarginSettings(){
                        Unit = Unit.Millimeters,
                        Bottom = 30, //document?.Letterhead?.BottomMargin,
                        Left = 32, //document?.Letterhead?.LeftMargin,
                        Right = 32, //document?.Letterhead?.RightMargin,
                        Top = 32, //document.Letterhead?.TopMargin,
                    },
                },
                Objects = {
                    new ObjectSettings() {
                        // Page ="-",
                        //PagesCount = true,
                        HtmlContent = content,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            if (!Directory.Exists(@"wwwroot\Documents"))
            {
                Directory.CreateDirectory(@"wwwroot\Documents");
            }

            using (FileStream stream = new FileStream(@"wwwroot\Documents\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

            return pdf;
        }

        private async Task<byte[]> QuotationDocumentFromTemplate(DocumentDto document)
        {
            CompanyDetailsDto company = await _companyDetailsService.GetAsync();
            document.Company = company;
            // prepare Document data
            if (document.Project != null)
            {
                List<ProjectFinancialCategoryDto> projectFinancialCategories = await _projectFinancialService.GetCategoriesByProject(document.Project.Id);
                document.ProjectFinancialCategory = projectFinancialCategories.Where(x => x.Category == ProjectFinancialCategoryEnum.TotalExcludeVat).FirstOrDefault();

                // EQUIPMENTS

                document.Equipments = await _projectEquipmentService.GetEquipmentsByProjectId(document.Project.Id);

                //CREW MEMBERS
                document.PlannedCrewMembers = await _projectPlanningService.GetPlanningGroupedFunctions(document.Project.Id, ProjectFunctionTypeEnum.Crew);

                //TRANSPORT
                document.PlannedTransport = await _projectPlanningService.GetPlanningGroupedFunctions(document.Project.Id, ProjectFunctionTypeEnum.Transport);

                document.AdditionalCosts = await _projectAdditionalCostService.GetAdditionalCosts(document.Project.Id);


                ProjectFinancialDto financial = await _projectFinancialService.GetByProjectId(document.ProjectId.Value);
                document.FinancialInfo = financial;
            }

            string content = DocumentTemplatesExtention.GetQoutatinHTML(document);

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4,
                    //Out = document.FileName,
                    //DocumentTitle = document.FileName,
                    Margins = new MarginSettings(){
                        Unit = Unit.Millimeters,
                        Bottom = 12, //document?.Letterhead?.BottomMargin,
                        Left = 8, //document?.Letterhead?.LeftMargin,
                        Right = 8, //document?.Letterhead?.RightMargin,
                        Top = 12, //document.Letterhead?.TopMargin,
                    },
                },
                Objects = {
                    new ObjectSettings() {
                        // Page ="-",
                        //PagesCount = true,
                        HtmlContent = content,
                        WebSettings = { DefaultEncoding = "utf-8" },
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            if (!Directory.Exists(@"wwwroot\Documents"))
            {
                Directory.CreateDirectory(@"wwwroot\Documents");
            }

            using (FileStream stream = new FileStream(@"wwwroot\Documents\" + document.FileName + ".pdf", FileMode.OpenOrCreate))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

            return pdf;
        }

        private async Task<byte[]> ContractDocument(DocumentDto document)
        {
            string content = "";
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    //Out = document.FileName,
                    //DocumentTitle = document.FileName,
                    Margins = new MarginSettings(){
                        Unit = Unit.Millimeters,
                        Bottom = 30, //document?.Letterhead?.BottomMargin,
                        Left = 32, //document?.Letterhead?.LeftMargin,
                        Right = 32, //document?.Letterhead?.RightMargin,
                        Top = 32, //document.Letterhead?.TopMargin,
                    },
                },
                Objects = {
                    new ObjectSettings() {
                        // Page ="-",
                        //PagesCount = true,
                        HtmlContent = content,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);

            if (!Directory.Exists(@"wwwroot\Documents"))
            {
                Directory.CreateDirectory(@"wwwroot\Documents");
            }

            // генерувати осмислений документ!!!

            using (FileStream stream = new FileStream(@"wwwroot\Documents\" + DateTime.UtcNow.Ticks.ToString() + ".pdf", FileMode.Create))
            {
                stream.Write(pdf, 0, pdf.Length);
            }

            return pdf;
        }

        public async Task<List<ProjectDocumentGroupDto>> GetDocumentsByProjectId(int projectId)
        {
            List<DocumentEntity> documentEntities = await _documentStorage.GetAllByForeignId(EntityEnum.ProjectEntity, projectId);
            List<DocumentDto> documentDtos = documentEntities.TransferToListDto("Project");

            List<ProjectDocumentGroupDto> result =
                documentDtos.TransferToProjectDocumentGroupDto();

            return result;

        }

        public async Task<DocumentDto> CreateDocument(DocumentTemplateTypeEnum documentType, int? projectId)
        {
            // якщо не обрано знайти спосіб як визначити
            var document = new DocumentStructureDto();
            var result = new DocumentDto();
            int typeId = 0;
            ProjectDto project = new ProjectDto();
            if (projectId != null)
            {
                project = await _projectService.GetById((int)projectId);
                typeId = project?.TypeId ?? 0;

                document.LinkedItem = new DocumentLinkedItemDto();
                document.LinkedItem.LinkId = projectId.Value;
                document.LinkedItem.Name = project?.Name;
            }
            if (typeId == 0)
            {
                ProjectTypeDefaultDto defaultType = await _projectTypeService.GetProjectTypeByDefault();
                typeId = defaultType.Id;
            }

            var paymentConditionByDefault = await _paymentConditionService.GetPaymentConditionByDefault();
            var paymentCondition = await _paymentConditionService.GetById(paymentConditionByDefault.Id);
            if (paymentCondition == null)
            {
                paymentCondition = await _paymentConditionService.GetById(1);
            }
            if (paymentCondition == null)
                return result;

            ProjectTypeDto type = await _projectTypeService.GetById(typeId);

            document.Type = documentType;
            document.Layout.LetterheadId = type.LetterheadTemplateId;
            document.Version.Date = DateTime.UtcNow;
            document.Version.Number = (DateTime.UtcNow.Millisecond % 1000).ToString();
            document.Version.Version = 1;

            document.Output.OpenKitsAndCases = OpenKitsAndCasesTypeEnum.DefaultKitOrCase;
            document.Financial.PaymentConditionId = paymentCondition.Id;
            document.Financial.DueDate = document.Version.Date.AddDays(paymentCondition.TermInDays);

            switch (documentType)
            {
                case DocumentTemplateTypeEnum.Invoice:
                    document.Layout.TemplateId = type.InvoiceTemplateId;
                    document.Output.Subject = $"Invoice {document.Version.Number}";
                    document.Output.FileName = $"Invoice {DateTime.UtcNow.ToString("yyyy-MM-dd")}.pdf";
                    break;
                case DocumentTemplateTypeEnum.Contract:
                    document.Layout.TemplateId = type.ContractTemplateId;
                    document.Output.Subject = $"Contract {document.Version.Number}";
                    document.Output.FileName = $"Contract {DateTime.UtcNow.ToString("yyyy-MM-dd")}.pdf";
                    break;
                case DocumentTemplateTypeEnum.Quotation:
                    document.Layout.TemplateId = type.QuotationTemplateId;
                    document.Output.Subject = $"Quotation {document.Version.Number}";
                    document.Output.FileName = $"Quotation {DateTime.UtcNow.ToString("yyyy-MM-dd")}.pdf";
                    break;
                case DocumentTemplateTypeEnum.PackingSlip:
                    document.Layout.TemplateId = type.PackingSlipTemplateId;
                    document.Output.Subject = $"PackingSlip {document.Version.Number}";
                    document.Output.FileName = $"PackingSlip {DateTime.UtcNow.ToString("yyyy-MM-dd")}.pdf";
                    break;
                // для усіх інших типів вибрати якийсь варіант по-замовчуванню
                case DocumentTemplateTypeEnum.Default:
                    break;
            }
            // отримуємо шаблон документу з якого будемо генерувати конкретний документ
            // var documentTemplate = await _documentTemplateStorage.GetById(document.Layout.TemplateId);

            var documentDto = document.TransferToDocumentDto();
            DocumentDto createResult = new DocumentDto();

            switch (documentType)
            {
                case DocumentTemplateTypeEnum.Invoice:
                    var invoiceDto = documentDto.PrepareInvoiceDto(project);
                    var createInvoice = await _invoiceStorage.Create(invoiceDto.TransferToEntity());
                    if (createInvoice != null)
                    {
                        var invoice = await _invoiceStorage.GetById(createInvoice.Id);
                        var documentById = await _documentStorage.GetById(invoice?.Document?.Id);
                        createResult = documentById?.TransferToDto();
                    }
                    break;
                default:
                    createResult = await Create(documentDto);
                    break;
            }



            // generate Other

            return createResult;
        }

        public Task<BaseListDto> GetPagedList(List<SelectedFilter> filterList)
        {
            throw new NotImplementedException();
        }

        public async Task<DocumentDto> GetById(int id)
        {
            //throw new NotImplementedException();
            DocumentEntity result = await _documentStorage.GetById(id);
            DocumentDto dto = result.TransferToDto();
            return dto;
        }

        public async Task<DocumentDto> Create(DocumentDto model)
        {
            DocumentEntity transferEntity = model.TransferToEntity();
            DocumentEntity entity = await _documentStorage.Create(transferEntity);
            DocumentEntity result = await _documentStorage.GetById(entity.Id);
            DocumentDto dto = result.TransferToDto();
            return dto;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = await _documentStorage.Delete(id);
            return result;
        }
        public async Task<DocumentDto> Update(DocumentDto model)
        {
            DocumentEntity dbEntity = await _documentStorage.GetById(model.Id);
            DocumentEntity transferEntity = model.TransferToEntity();

            dbEntity.TransferToEntityForUpdate(transferEntity);

            DocumentEntity entity = await _documentStorage.Update(dbEntity);
            DocumentDto dto = entity.TransferToDto();
            return dto;
        }
    }
}
