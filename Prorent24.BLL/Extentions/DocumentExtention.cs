using DinkToPdf;
using Prorent24.BLL.Models.Configuration.AccountConfiguration;
using Prorent24.BLL.Models.Contact;
using Prorent24.BLL.Models.Invoice;
using Prorent24.BLL.Models.Project;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Prorent24.BLL.Extentions
{
    public static class DocumentExtention
    {
        // private static string td = "td";
        // можна рефлексією поля вибрати як будуть виводитися
        public static string GetContactBlockInfo(this ContactDto client)
        {
            string contractBlockInfo = "";
            contractBlockInfo += client.Name.InTag("b") + NewLine();
            contractBlockInfo += client.PostalAddress?.Address.InTag("b") + NewLine();
            contractBlockInfo += client.PostalAddress?.Number.InTag("b") + NewLine();
            contractBlockInfo += client.PostalAddress?.PostalCode.InTag("b") + NewLine();
            contractBlockInfo += client.PostalAddress?.City.InTag("b") + NewLine();
            contractBlockInfo = contractBlockInfo.InTag("div", new string[] { "style=\"display:block;\"" });

            return contractBlockInfo;
        }

        public static string GetEquipmentBlock(this List<ProjectEquipmentGridDto> equipmentGroups)
        {
            string invoiceBlockInfo = "";
            string tbody = "";
            invoiceBlockInfo += "Оборудование".InTag("h3").InTag("td", new string[] { "callspan = 3" }).InTag("tr");

            string _titleLine = ("Название".InTag("th", new string[] { "colspan=\"2\"" })
                        + "Стоимость".InTag("th", new string[] { "style=\"text-align:right;\"" })
                        + "Количество".InTag("th", new string[] { "style=\"text-align:right;\"" })
                        + "Цена".InTag("th", new string[] { "style=\"text-align:right;\"" })
                   ).InTag("tr", new string[] { "class=\"header-style\"" });

            invoiceBlockInfo += _titleLine;

            foreach (var equipmentGroup in equipmentGroups)
            {
                string _line = (equipmentGroup.GroupName.InTag("td", new string[] { "colspan=\"4\"" })
                   + equipmentGroup.Price.InTag("td", new string[] { "style=\"text-align:right;\"" })
                   ).InTag("tr");
                invoiceBlockInfo += _line;

                foreach (var equipment in equipmentGroup.Childrens)
                {
                    string _equipmentLine = (
                         equipment.EquipmentName.InTag("td", new string[] { "colspan=\"2\"" })
                        + equipment.Price.InTag("td", new string[] { "style=\"text-align:right;\"" })
                        + equipment.Quantity.InTag("td", new string[] { "style=\"text-align:right;\"" })
                        + (equipment.Price * equipment.Quantity).InTag("td", new string[] { "style=\"text-align:right;\"" })
                   ).InTag("tr");

                    invoiceBlockInfo += _equipmentLine;
                }
            }

            invoiceBlockInfo = invoiceBlockInfo.InTag("tbody");

            return invoiceBlockInfo;
        }

        public static string GetCrewMemberBlock(this List<ProjectPlanningGridDto> planningGroups)
        {
            string invoiceBlockInfo = "";
            string tbody = "";
            invoiceBlockInfo += "Персонал".InTag("h3").InTag("td", new string[] { "callspan = 3" }).InTag("tr");


            string _titleLine = ("Имя".InTag("th")
                       //+ "Стоимость".InTag("th", new string[] { "style=\"text-align:right;\"" })
                       + "Количество".InTag("th", new string[] { "style=\"text-align:right;\"" })
                       + "С".InTag("th", new string[] { "style=\"text-align:right;\"" })
                       + "По".InTag("th", new string[] { "style=\"text-align:right;\"" })
                       + "Цена".InTag("th", new string[] { "style=\"text-align:right;\"" })
                  ).InTag("tr", new string[] { "class=\"header-style\"" });

            invoiceBlockInfo += _titleLine;

            foreach (var group in planningGroups)
            {
                var price = 0;
                string _line = (group.Name.InTag("td")
                  + group.Quantity.InTag("td", new string[] { "style=\"text-align:right;\"" })
                  + group.PlanFrom?.ToShortDateString().InTag("td", new string[] { "style=\"text-align:right;\"" })
                  + group.PlanUntil?.ToShortDateString().InTag("td", new string[] { "style=\"text-align:right;\"" })
                  + price.InTag("td", new string[] { "style=\"text-align:right;\"" })
                  ).InTag("tr");
                invoiceBlockInfo += _line;
            }
            invoiceBlockInfo = invoiceBlockInfo.InTag("tbody");

            return invoiceBlockInfo;
        }

        public static string GetTransportBlock(this List<ProjectPlanningGridDto> planningGroups)
        {
            string invoiceBlockInfo = "";
            string tbody = "";
            invoiceBlockInfo += "Транспорт".InTag("h3").InTag("td", new string[] { "callspan = 3" }).InTag("tr");

            string _titleLine = ("Имя".InTag("th", new string[] { "colspan = \"3\"" })
            //+ "Стоимость".InTag("th", new string[] { "style=\"text-align:right;\"" })
            + "Количество".InTag("th", new string[] { "style=\"text-align:right;\"" })
            + "Цена".InTag("th", new string[] { "style=\"text-align:right;\"" })
            ).InTag("tr", new string[] { "class=\"header-style\"" });

            invoiceBlockInfo += _titleLine;

            foreach (var group in planningGroups)
            {
                var price = 0;
                string _line = (
                    group.Name.InTag("td", new string[] { "colspan=\"3\"" })
                  + group.Quantity.InTag("td", new string[] { "style=\"text-align:right;\"" })
                  //+ group.PlanFrom?.ToShortDateString().InTag("td", new string[] { "style=\"text-align:right;\"" })
                  //+ group.PlanUntil?.ToShortDateString().InTag("td", new string[] { "style=\"text-align:right;\"" })
                  + price.InTag("td", new string[] { "style=\"text-align:right;\"" })
                  ).InTag("tr");
                invoiceBlockInfo += _line;
            }

            invoiceBlockInfo = invoiceBlockInfo.InTag("tbody");

            return invoiceBlockInfo;
        }
        public static string GetCompanyBlockInfo(this CompanyDetailsDto company)
        {
            string companyBlockInfo = "";
            string tbody = "";

            string companyAddress = $"{company?.AdditionalOffice?.HouseNumber} {company?.AdditionalOffice?.Street} {company?.AdditionalOffice?.City} {company?.AdditionalOffice?.Postcode}";
            string phoneInfo = string.Join("/", company.Phones);
            string mailInfo = company.InvoiceEmail;
            string website = company.Website;

            companyBlockInfo += companyAddress.InTag("div", new string[] { "style=\"display: block;\"" });
            companyBlockInfo += phoneInfo.InTag("div", new string[] { "style=\"display: block;\"" });
            companyBlockInfo += mailInfo.InTag("div", new string[] { "style=\"display: block;\"" });
            companyBlockInfo += website.InTag("div", new string[] { "style=\"display: block;\"" });
        
            return companyBlockInfo.InTag("div", new string[] { "style=\"float: right; display: block;\"" });
        }
        public static string GetInvoiceBlockInfo(this InvoiceDto invoice)
        {
            string invoiceBlockInfo = "";
            string tbody = "";

            tbody += "Счет".InTag("h3").InTag("td", new string[] { "callspan = 3" }).InTag("tr");

            tbody += ("Номер счета:".InTag("td")
                + invoice.Document?.Number?.InTag("td")
                + "Период:".InTag("td")
                + ($"{invoice?.Document?.Date?.ToShortDateString()} - {invoice.DueDate?.ToShortDateString()}").InTag("td"))
                .InTag("tr");

            tbody += ("Дата выставления счета:".InTag("td")
                + invoice.Document?.Date?.ToShortDateString().InTag("td")
                + "Оплата:".InTag("td")
                + invoice.PaymentCondition?.Name.InTag("td", new string[] { "style=\"word-wrap: break-word; width: 20%\"" })).InTag("tr");

            tbody += ("Дата исполнения:".InTag("td")
                + invoice.DueDate?.ToShortDateString().InTag("td")
                + "Номер клиента".InTag("td")
                + "".InTag("td")).InTag("tr");

            tbody = tbody.InTag("tbody");

            return tbody;
        }
        public static string GetInvoiceLineBlockInfo(this List<InvoiceLineDto> invoiceLines)
        {
            string invoiceLineBlockInfo = "";
            foreach (var line in invoiceLines)
            {

                string _line = (line.Description.InTag("td", new string[] { "colspan=\"3\"" })
                    + line.Price.InTag("td", new string[] { "style=\"text-align:right;\"" })
                    ).InTag("tr");

                invoiceLineBlockInfo += _line;
            }
            invoiceLineBlockInfo = invoiceLineBlockInfo.InTag("tbody");

            return invoiceLineBlockInfo;
        }

        public static string GetInvoiceTotalInfo(this InvoiceDto invoice)
        {
            string invoiceTotalInfo = "";
            string tbody = "";

            tbody += ("".InTag("td", new string[] { "colspan=\"2\"" })
                + "Цена Без НДС*:".InTag("td", new string[] { "class=\"text-align-right border_top\"" })
                + invoice.PriceExcludeVAT?.InTag("td", new string[] { "class=\"text-align-right border_top\"" })).InTag("tr");

            tbody += ("".InTag("td", new string[] { "colspan=\"2\"" })
                + invoice.VatScheme?.Name.InTag("td", new string[] { "class=\"text-align-right\"" })
                + invoice.VAT?.InTag("td", new string[] { "class=\"text-align-right\"" })).InTag("tr");

            tbody += ("".InTag("td", new string[] { "colspan=\"2\"" })
                + "Цена включающая НДС:".InTag("td", new string[] { "class=\"text-align-right border_top\"" })
                + invoice.PriceIncludeVAT?.InTag("td", new string[] { "class=\"text-align-right border_top\"" })).InTag("tr");

            tbody = tbody.InTag("tbody");
            return tbody;
        }

        public static string GetInvoiceTemplate(this string invoiceBody)
        {
            string head = @"<head>
                            <style>
                            .text-align-right{
	                            text-align:right;
                            }
                            .text-align-left {
	                            text-align:left;
                            }
                            tr.border_top td,
                            td.border_top
                             {
                              border-top:1pt solid black;
                            }
                            .header-style th {
                                font-weight: 700;
                                line-height: 20px;
                                text-align: left;
                                border-top: 1px solid #000;
                                border-bottom: 1px solid #000;
                            }
                            .header-style th:first-child {
                                border-left: 1px solid #000;
                            }
                             .header-style th:last-child {
                                border-right: 1px solid #000;
                            }   
                            </style>
                            </head>";

            string body = invoiceBody.InTag("table").InTag("body");

            string html = (head + body).InTag("html");


            return html;
        }

        public static string InTag(this object value, string tag, string[] attributes = null)
        {
            return $"<{tag}{(attributes == null ? string.Empty : " "+string.Join(" ", attributes))}>{value}</{tag}>";
        }

        public static string NewLine()
        {
            return $"</br>";
        }
    }
}
