using Prorent24.BLL.Models.General.Document;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Prorent24.BLL.Extentions
{
    public static class DocumentTemplatesExtention
    {
        static string _invoiceTemplateOld = @"<html>
<head>
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
</style>
</head>
<body>
<div style='width:50%; padding-top:15px;padding-bottom:15px;'> 
<b>{invoice.Client?.Name}</b>
<br/>
{invoice.Client?.BillingAddress?.Address} 
<br/>
{invoice.Client?.BillingAddress?.Number}
<br/>
{invoice.Client?.BillingAddress?.PostalCode}
<br/>
{invoice.Client?.BillingAddress?.City}
<br/>
</div>
<div style='width:100%'>
<table style='width:100%'>  
<tbody>
<tr>
<td>
<h3>
Счет
</h3>
</td>
<td>
</td>
<td>
</td>
<td>
</td>
</tr>
<tr>
<td>
Номер счета:
</td>
<td>
{invoice.Number} 
</td>
<td>
Период:
</td>
<td>
{invoice.Date} - {invoice.DueDate}
</td>
</tr>
<tr>
<td>
Дата выставления счета:
</td>
<td>
{invoice.Date}
</td>
<td>
Оплата:
</td>
<td style='word-wrap: break-word; width: 20%'>
{invoice.PaymentCondition?.Name}
</td>
</tr>
<tr>
<td>
Дата исполнения:
</td>
<td>
{invoice.DueDate}
</td>
<td>
Номер клиента
</td>
<td>
</td>
</tr>
</tbody>
<div style='padding:15px'></div>
<tbody>
<tr>
<th colspan='3' class='text-align-left' style='padding:15px 0px;'>Описание</th>
<th class='text-align-right'>Цена</th>
</tr>
[InvoiceLineTemplate]
<tr>
<td colspan='3'>
{invoice.InvoiceLine[].Name}
</td>	
<td style='text-align:right;'>
{invoice.InvoiceLine[].Price}
</td>
</tr>
[/InvoiceLineTemplate]
</tbody>
<div style='padding:15px'></div>
<tbody>
<tr>
<td colspan='2'>
</td>
<td class='text-align-right border_top'>
Цена Без НДС*:
</td>
<td class='text-align-right border_top'>
{invoice.PriceExcludeVAT}
</td>
</tr>
<tr>
<td colspan='2'>
</td>
<td class='text-align-right'>
{invoice.VatScheme.Name}
</td>
<td class='text-align-right'>
{invoice.VAT}
</td>
</tr>
<tr>
<td colspan='2'>
</td>
<td class='text-align-right border_top'>
Цена включающая НДС:
</td>
<td class='text-align-right border_top'>
{invoice.PriceIncudeVAT}
</td>
</tr>
</tbody>
</table>
</div>
</body>
</html>";
        static string _invoiceTemplate = @"<html>
<head>
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
.description-block p {
    margin-top: 4px;
    margin-bottom: 4px;
}
.project-total p {
    text-align: right;
    font-weight: bold;
    margin-bottom: 6px;
    font-size: 14px;
}
.block-title {
    text-align: left;
    font-weight: bold;
    font-size: 14px;
}
</style>
</head>
<body>
<div> 
<table style='width:100%; padding-top:15px;padding-bottom:15px;'>
<tr><td colspan='4'>{bankName}</td><td>БИК</td><td>{bikNumber}</td></tr>
<tr><td colspan='4'>Банк получателя</td><td>Сч. №</td><td>{billNumber}</td></tr>
<tr><td>ИНН</td><td>{inn}</td><td>КПП</td><td>{kpp}</td><td>Сч. №</td><td>{billNumber2}</td></tr>
<tr><td colspan='4'>{company}</td><td></td><td></td></tr>
<tr><td colspan='4'>Получатель</td><td></td><td></td></tr>
</table>
</div>
<div style='border-color: black;border-style: solid;border-width: 2px;padding: 15px 10px;font-weight: bold;font-size: 20px;'> 
Счет на оплату № {invoiceNumber} от {invoiceDate}
</div>
<div>
<table style='width:100%;'>
<tr><td style='width:140px;'>Поставщик</td><td colspan='5' rowspan='2'>{supplierInfo}</td></tr>
<tr><td>(Исполнитель):</td></tr>
<tr><td>Покупатель</td><td colspan='5' rowspan='2'>{buyerInfo}</td></tr>
<tr><td>(Заказчик):</td></tr>
<tr><td>Основание:</td><td colspan='5' rowspan='2'>{reasonInfo}</td></tr>
</table>
</div>
[divTableBody]
<div class='block-title' style='width:100%; padding-top:15px; padding-bottom:15px'>{blockTitle}</div>
<div style='border-color: black;border-style: solid;border-width: 2px;padding: 15px'> 
<table style='width: 100%;'>
<thead class='text-align-left'>
<tr><th>№</th><th>Код</th><th>Товары(работы, услуги)</th><th>Кол-во</th><th>Ед.</th><th>Цена</th><th>Сумма</th></tr>
</thead>
<tbody class='text-align-left'>
[equipmentListBody]
<tr><td>{num}</td><td>{code}</td><td>{name}</td><td>{count}</td><td>{numId}</td><td>{price}</td><td>{totalPrice}</td></tr>
[/equipmentListBody]
</tbody>
</table>
</div>
[/divTableBody]
<div style='padding-left: 10px;padding-right: 10px;' class='project-total'>
<p>Итого: {totalPrice}</p>
<p>В том числе НДС: {totalVATPrice}</p>
<p>Всего к оплате: {totalPayment}</p>
<p></p>
</div>
<div class='description-block'>
<p>Всего наименований {totalCount}, на сумму {totalProjectPrice}</p>
<p style='font-weight: bold;'>Проект: {projectName}</p>
<p>Внимание!</p>
<p>Оплата данного счета означает согласие с условиями поставки товара. Уведомление об оплате обязательно, в противном случае не гарантируется наличие товара на складе.</p>
<p>Товар отпускается по факту прихода денег на р/с Поставщика, самовывозом, при наличии доверенности и паспорта.</p>
</div>
<div style='width:95%; padding-top:20px; padding-bottom:20px;'>
<table style='width:100%;'>
<tbody>
<tr>
<td>Руководитель</td>
<td>{projectManager}</td>
<td>Бухгалтер</td>
<td>{accountant}</td>
</tr>
</tbody>
</table>
</div>
</body>
</html>";
        static string _qoutationTemplate = @"<html>
<head>
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
.description-block p {
    margin-top: 4px;
    margin-bottom: 4px;
}
.project-total p {
    text-align: right;
    font-weight: bold;
    margin-bottom: 6px;
    font-size: 14px;
}
.block-title {
    text-align: left;
    font-size: 14px;
}
.company-title {
	font-weight: bold;
	font-size: 20px !important;
	color: darkblue;
}
.bottom-block p{
	font-weight: bold;
}
.table-content {
table {
  border-collapse: collapse;
}

table, th, td {
  border: 1px solid black;
}
}

</style>
</head>
<body>
<div class='project-total'> 
<p class='company-title'>{company}</p>
<p>Адрес: {supplierInfo}</p>
<p>Тел: {supplierPhone}</p>
<p>Email: {supplierEmail}, Сайт {suplierSite}</p>
</div>
<div style='padding: 15px 10px;font-weight: bold;font-size: 24px; text-align: center;'> 
Коммерческое предложение
</div>
<div style='width:100%;'>
<table style='width:100%;'>
<tr><td style='text-align: left'>{invoiceDate}</td><td style='text-align: right'>Кому: {buyerInfo}</td></tr>
</table>
</div>
[divTableBody]
<div class='block-title' style='width:100%; padding-top:15px; padding-bottom:15px'>{blockTitle}</div>
<div> 
<table class='table-content' style='width: 100%; border-collapse: collapse;' cellspacing='2' border='1' cellpadding='5'>
<thead class='text-align-left'>
<tr><th>№</th><th>Код</th><th>Товары(работы, услуги)</th><th>Кол-во</th><th>Ед.</th><th>Цена</th><th>Сумма</th></tr>
</thead>
<tbody class='text-align-left'>
[equipmentListBody]
<tr><td>{num}</td><td>{code}</td><td>{name}</td><td>{count}</td><td>{numId}</td><td>{price}</td><td>{totalPrice}</td></tr>
[/equipmentListBody]
</tbody>
</table>
</div>
[/divTableBody]
<div style='padding-left: 10px;padding-right: 10px;' class='project-total'>
<p>Итого: {totalPrice}</p>
<p>В том числе НДС: {totalVATPrice}</p>
<p>Всего к оплате: {totalPayment}</p>
<p></p>
</div>
<div class='description-block'>
<p>Всего наименований {totalCount}, на сумму {totalProjectPrice}</p>
<p style='font-weight: bold;'>Проект: {projectName}</p>
</div>
<div  class='bottom-block' style='width:95%; padding-top:20px; padding-bottom:20px;'>
<p>Итого cумма: {totalProjectPrice}.</p>
<p>Данное коммерческое предложение действительно до {dateValid}.</p>
<p>Генеральный директор {projectManager}</p>
</div>
</body>
</html>";
        public static string GetInvoiceHTML(DocumentDto document)
        {
            string content = _invoiceTemplate;

            List<string> _supplierInfo = new List<string>();
            if (document.Company?.CompanyName != null) _supplierInfo.Add(document.Company?.CompanyName);
            if (document.Company?.Inn != null) _supplierInfo.Add($"ИНН {document.Company?.Inn}");
            if (document.Company?.Kpp != null) _supplierInfo.Add($"КПП {document.Company?.Kpp}");
            if (document.Company?.Postcode != null) _supplierInfo.Add(document.Company?.Postcode);
            if (document.Company?.City != null) _supplierInfo.Add(document.Company?.City);
            if (document.Company?.Street != null) _supplierInfo.Add(document.Company?.Street);
            if (document.Company?.HouseNumber != null) _supplierInfo.Add(document.Company?.HouseNumber);

            string supplierInfo = string.Join(",", _supplierInfo);

            /*string supplierInfo = $@"{document.Company?.CompanyName}, ИНН {document.Company?.Inn},
                                    КПП {document.Company?.Kpp}, 
                                    {document.Company?.Postcode},
                                    {document.Company?.City}, 
                                    {document.Company?.Street}, 
                                    {document.Company?.HouseNumber}";*/


            List<string> _buyerInfo = new List<string>();
            if (document.Project?.ClientContact?.CompanyName != null) _buyerInfo.Add(document.Project?.ClientContact?.CompanyName);
            if (document.Project?.ClientContact?.Inn != null) _buyerInfo.Add($"ИНН {document.Project?.ClientContact?.Inn}");
            if (document.Project?.ClientContact?.Kpp != null) _buyerInfo.Add($"КПП {document.Project?.ClientContact?.Kpp}");
            if (document.Project?.ClientContact?.PostalAddress?.PostalCode != null) _buyerInfo.Add(document.Project?.ClientContact?.PostalAddress?.PostalCode);
            if (document.Project?.ClientContact?.PostalAddress?.City != null) _buyerInfo.Add(document.Project?.ClientContact?.PostalAddress?.City);
            if (document.Project?.ClientContact?.PostalAddress?.Address != null) _buyerInfo.Add(document.Project?.ClientContact?.PostalAddress?.Address);
            if (document.Project?.ClientContact?.PostalAddress?.Number != null) _buyerInfo.Add(document.Project?.ClientContact?.PostalAddress?.Number);

            string buyerInfo = string.Join(",", _buyerInfo);

            /*string buyerInfo = $@"{document.Project?.ClientContact?.CompanyName}, 
                                    ИНН {document.Project?.ClientContact?.Inn},
                                    КПП {document.Project?.ClientContact?.Kpp}, 
                                    {document.Project?.ClientContact?.PostalAddress?.PostalCode},
                                    {document.Project?.ClientContact?.PostalAddress?.City}, 
                                    {document.Project?.ClientContact?.PostalAddress?.Address}, 
                                    {document.Project?.ClientContact?.PostalAddress?.Number}";*/
            string reasonInfo = "";


            int startTableBlockWith = content.IndexOf("[divTableBody]");
            int endTableBlockWith = content.IndexOf("[/divTableBody]");
            string divTemplate = content.Substring(startTableBlockWith + "[divTableBody]".Length, endTableBlockWith - (startTableBlockWith + "[divTableBody]".Length));


            int startWith = divTemplate.IndexOf("[equipmentListBody]");
            int endWith = divTemplate.IndexOf("[/equipmentListBody]");
            string tableTemplate = divTemplate.Substring(startWith + "[equipmentListBody]".Length, endWith - (startWith + "[equipmentListBody]".Length));

            string equipmentString = "";
            int totalCount = 0;
            int count = 0;
            decimal totalPrice = 0;
            string blocksHTML = "";
            string currency = " руб";
            if (document.Equipments != null && document.Equipments.Count > 0)
            {
                foreach (var group in document.Equipments)
                {
                    foreach (var equipment in group.Childrens)
                    {
                        totalCount++;
                        count++;
                        equipmentString = $@"{equipmentString} 
                        {tableTemplate
                            .Replace("{num}", count.ToString())
                            .Replace("{code}", "")
                            .Replace("{name}", equipment.EquipmentName)
                            .Replace("{count}", equipment.Quantity.ToString())
                            .Replace("{numId}", "")
                            .Replace("{price}", $"{equipment.Price?.ToString("F")} {currency}") // Currency
                            .Replace("{totalPrice}", $"{equipment.TotalPrice?.ToString("F")} {currency}")}";
                        totalPrice += equipment.TotalPrice.Value;
                    };
                };


                blocksHTML = $@"{blocksHTML}{divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Оборудование")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            };

            count = 0;
            equipmentString = String.Empty;

            if (document.PlannedCrewMembers != null && document.PlannedCrewMembers.Count > 0)
            {
                foreach (var group in document.PlannedCrewMembers)
                {
                    //if (group.Childrens != null && group.Childrens.Count > 0)
                    //{
                    //    foreach (var crewMember in group.Childrens)
                    //    {
                    //        totalCount++;
                    //        equipmentString = $@"{equipmentString} 
                    //    {tableTemplate
                    //            .Replace("{num}", totalCount.ToString())
                    //            .Replace("{code}", "")
                    //            .Replace("{name}", crewMember.Name)
                    //            .Replace("{count}", group.Quantity.ToString())
                    //            .Replace("{numId}", "")
                    //            .Replace("{price}", $"{crewMember?.ProjectFunction?.RentalFixed.ToString("F")}") // Currency
                    //            .Replace("{totalPrice}", $"{crewMember?.ProjectFunction?.TotalCosts.ToString("F")}")}";
                    //    };
                    //}
                    //else
                    //{
                    totalCount++;
                    count++;
                    decimal price = group?.ProjectFunction?.Quantity > 0 ? (group?.ProjectFunction?.TotalCosts / group?.ProjectFunction?.Quantity).Value : 0;
                    //totalPrice += total;
                    equipmentString = $@"{equipmentString} 
                        {tableTemplate
                        .Replace("{num}", count.ToString())
                        .Replace("{code}", "")
                        .Replace("{name}", group.Name)
                        .Replace("{count}", group.Quantity.ToString())
                        .Replace("{numId}", "")
                        .Replace("{price}", $"{price.ToString("F")} {currency}") // Currency
                        .Replace("{totalPrice}", $"{group?.ProjectFunction?.TotalCosts.ToString("F")} {currency}")}";

                    //}
                };

                blocksHTML = $@"{blocksHTML} {divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Сотрудники")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            };

            count = 0;
            equipmentString = String.Empty;

            if (document.PlannedTransport != null && document.PlannedTransport.Count > 0)
            {
                foreach (var group in document.PlannedTransport)
                {
                    //if (group.Childrens != null && group.Childrens.Count > 0)
                    //{
                    //    foreach (var crewMember in group.Childrens)
                    //    {
                    //        totalCount++;
                    //        equipmentString = $@"{equipmentString} 
                    //    {tableTemplate
                    //            .Replace("{num}", totalCount.ToString())
                    //            .Replace("{code}", "")
                    //            .Replace("{name}", crewMember.Name)
                    //            .Replace("{count}", group.Quantity.ToString())
                    //            .Replace("{numId}", "")
                    //            .Replace("{price}", $"{crewMember?.ProjectFunction?.RentalFixed.ToString("F")}{currency}") // Currency
                    //            .Replace("{totalPrice}", $"{crewMember?.ProjectFunction?.TotalCosts.ToString("F")}{currency}")}";
                    //    };
                    //}
                    //else
                    //{
                    decimal price = group?.ProjectFunction?.Quantity > 0 ? (group?.ProjectFunction?.TotalCosts / group?.ProjectFunction?.Quantity).Value : 0;
                    totalCount++;
                    count++;
                    equipmentString = $@"{equipmentString} 
                        {tableTemplate
                        .Replace("{num}", count.ToString())
                        .Replace("{code}", "")
                        .Replace("{name}", group.Name)
                        .Replace("{count}", group.Quantity.ToString())
                        .Replace("{numId}", "")
                        .Replace("{price}", $"{price.ToString("F")}{currency}") // Currency
                        .Replace("{totalPrice}", $"{group?.ProjectFunction?.TotalCosts.ToString("F")}{currency}")}";

                    //}
                };

                blocksHTML = $@"{blocksHTML} {divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Транспорт")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            };

            count = 0;
            equipmentString = String.Empty;

            if (document.AdditionalCosts != null && document.AdditionalCosts.Count > 0)
            {
                foreach (var additional in document.AdditionalCosts)
                {
                    totalCount++;
                    count++;
                    //totalPrice += total;
                    equipmentString = $@"{equipmentString} 
                        {tableTemplate
                        .Replace("{num}", count.ToString())
                        .Replace("{code}", "")
                        .Replace("{name}", additional.Name)
                        .Replace("{count}", "")
                        .Replace("{numId}", "")
                        .Replace("{price}", "") // Currency
                        .Replace("{totalPrice}", $"{additional.SalePrice.ToString("F")} {currency}")}";

                }

                blocksHTML = $@"{blocksHTML} {divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Дополнительные расходы")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            }

            count = 0;
            equipmentString = String.Empty;

            content = $@"{ content.Substring(0, startTableBlockWith)}
                    { blocksHTML}
                    { content.Substring(endTableBlockWith + "[/divTableBody]".Length)}";

            content = content
                .Replace("{bankName}", document.Company?.Bank)
                .Replace("{bikNumber}", document.Company?.Bic)
                .Replace("{inn}", document.Company?.Inn)
                .Replace("{company}", document.Company?.CompanyName)
                .Replace("{billNumber}", document.Company?.CheckingAccount)
                .Replace("{billNumber2}", document.Company?.CorrespondentAccount)
                .Replace("{kpp}", document.Company?.Kpp)
                .Replace("{projectManager}", $"{document.Company?.DirectorInfo?.LastName} {(string.IsNullOrEmpty(document.Company?.DirectorInfo?.FirstName) ? "" : document.Company?.DirectorInfo?.FirstName?.Substring(0, 1))}{(string.IsNullOrEmpty(document.Company?.DirectorInfo?.MiddleName) ? "" : document.Company?.DirectorInfo?.MiddleName?.Substring(0, 1))}")
                .Replace("{accountant}", $"{document.Company?.AccountantInfo?.LastName} {(string.IsNullOrEmpty(document.Company?.AccountantInfo?.FirstName) ? "" : document.Company?.AccountantInfo?.FirstName?.Substring(0, 1))}{(string.IsNullOrEmpty(document.Company?.AccountantInfo?.MiddleName) ? "" : document.Company?.AccountantInfo?.MiddleName?.Substring(0, 1))}")
                .Replace("{invoiceNumber}", document.Number)
                .Replace("{invoiceDate}", document.Invoice?.Date?.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU")))
                .Replace("{supplierInfo}", supplierInfo)
                .Replace("{buyerInfo}", buyerInfo)
                .Replace("{reasonInfo}", reasonInfo)
                .Replace("{totalCount}", totalCount.ToString())
                .Replace("{projectName}", document.Project?.Name)

                //.Replace("{totalPrice}", totalPrice.ToString("F"))
                //.Replace("{totalVATPrice}", (totalPrice * 20 / 120).ToString("F")))
                //.Replace("{totalPayment}", totalPrice.ToString("F"))
                //.Replace("{totalProjectPrice}", totalPrice.ToString("F))

                .Replace("{totalPrice}", $"{document?.ProjectFinancialCategory?.Total.ToString("F")} {currency}")
                .Replace("{totalVATPrice}", $"{(document?.FinancialInfo?.TotalIncVat - document?.ProjectFinancialCategory?.Total)?.ToString("F")} {currency}")
                .Replace("{totalPayment}", $"{document?.FinancialInfo?.TotalIncVat.ToString("F")} {currency}")
                .Replace("{totalProjectPrice}", $"{document?.FinancialInfo?.TotalIncVat.ToString("F")} {currency}");

            return content;
        }
        public static string GetQoutatinHTML(DocumentDto document)
        {
            string content = _qoutationTemplate;
            /*string supplierInfo = $@"{document.Company?.Postcode},
                                    {document.Company?.City}, {document.Company?.Street}, 
                                    {document.Company?.HouseNumber}";*/

            List<string> _supplierInfo = new List<string>();
            if (document.Company?.Postcode != null) _supplierInfo.Add(document.Company?.Postcode);
            if (document.Company?.City != null) _supplierInfo.Add(document.Company?.City);
            if (document.Company?.Street != null) _supplierInfo.Add(document.Company?.Street);
            if (document.Company?.HouseNumber != null) _supplierInfo.Add(document.Company?.HouseNumber);

            string supplierInfo = string.Join(",", _supplierInfo);

            string buyerInfo = $@"{document.Project?.ClientContact?.CompanyName}";
            string reasonInfo = "";


            int startTableBlockWith = content.IndexOf("[divTableBody]");
            int endTableBlockWith = content.IndexOf("[/divTableBody]");
            string divTemplate = content.Substring(startTableBlockWith + "[divTableBody]".Length, endTableBlockWith - (startTableBlockWith + "[divTableBody]".Length));


            int startWith = divTemplate.IndexOf("[equipmentListBody]");
            int endWith = divTemplate.IndexOf("[/equipmentListBody]");
            string tableTemplate = divTemplate.Substring(startWith + "[equipmentListBody]".Length, endWith - (startWith + "[equipmentListBody]".Length));

            string equipmentString = "";
            int totalCount = 0;
            int count = 0;
            decimal totalPrice = 0;
            string blocksHTML = "";
            string currency = "  руб";
            if (document.Equipments != null && document.Equipments.Count > 0)
            {
                foreach (var group in document.Equipments)
                {
                    foreach (var equipment in group.Childrens)
                    {
                        totalCount++;
                        count++;
                        equipmentString = $@"{equipmentString} 
                        {tableTemplate
                            .Replace("{num}", count.ToString())
                            .Replace("{code}", "")
                            .Replace("{name}", equipment.EquipmentName)
                            .Replace("{count}", equipment.Quantity.ToString())
                            .Replace("{numId}", "")
                            .Replace("{price}", $"{equipment.Price?.ToString("F")} {currency}") // Currency
                            .Replace("{totalPrice}", $"{equipment.TotalPrice?.ToString("F")} {currency}")}";
                        totalPrice += equipment.TotalPrice.Value;
                    };
                };


                blocksHTML = $@"{blocksHTML}{divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Оборудование")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            };

            count = 0;
            equipmentString = String.Empty;

            if (document.PlannedCrewMembers != null && document.PlannedCrewMembers.Count > 0)
            {
                foreach (var group in document.PlannedCrewMembers)
                {
                    //if (group.Childrens != null && group.Childrens.Count > 0)
                    //{
                    //    foreach (var crewMember in group.Childrens)
                    //    {
                    //        totalCount++;
                    //        equipmentString = $@"{equipmentString} 
                    //    {tableTemplate
                    //            .Replace("{num}", totalCount.ToString())
                    //            .Replace("{code}", "")
                    //            .Replace("{name}", crewMember.Name)
                    //            .Replace("{count}", group.Quantity.ToString())
                    //            .Replace("{numId}", "")
                    //            .Replace("{price}", $"{crewMember?.ProjectFunction?.RentalFixed.ToString("F")}") // Currency
                    //            .Replace("{totalPrice}", $"{crewMember?.ProjectFunction?.TotalCosts.ToString("F")}")}";
                    //    };
                    //}
                    //else
                    //{
                    totalCount++;
                    count++;
                    decimal price = group?.ProjectFunction?.Quantity > 0 ? (group?.ProjectFunction?.TotalCosts / group?.ProjectFunction?.Quantity).Value : 0;
                    //totalPrice += total;
                    equipmentString = $@"{equipmentString} 
                        {tableTemplate
                        .Replace("{num}", count.ToString())
                        .Replace("{code}", "")
                        .Replace("{name}", group.Name)
                        .Replace("{count}", group.Quantity.ToString())
                        .Replace("{numId}", "")
                        .Replace("{price}", $"{price.ToString("F")} {currency}") // Currency
                        .Replace("{totalPrice}", $"{group?.ProjectFunction?.TotalCosts.ToString("F")} {currency}")}";

                    //}
                };

                blocksHTML = $@"{blocksHTML} {divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Сотрудники")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            };

            count = 0;
            equipmentString = String.Empty;

            if (document.PlannedTransport != null && document.PlannedTransport.Count > 0)
            {
                foreach (var group in document.PlannedTransport)
                {
                    //if (group.Childrens != null && group.Childrens.Count > 0)
                    //{
                    //    foreach (var crewMember in group.Childrens)
                    //    {
                    //        totalCount++;
                    //        equipmentString = $@"{equipmentString} 
                    //    {tableTemplate
                    //            .Replace("{num}", totalCount.ToString())
                    //            .Replace("{code}", "")
                    //            .Replace("{name}", crewMember.Name)
                    //            .Replace("{count}", group.Quantity.ToString())
                    //            .Replace("{numId}", "")
                    //            .Replace("{price}", $"{crewMember?.ProjectFunction?.RentalFixed.ToString("F")}{currency}") // Currency
                    //            .Replace("{totalPrice}", $"{crewMember?.ProjectFunction?.TotalCosts.ToString("F")}{currency}")}";
                    //    };
                    //}
                    //else
                    //{
                    decimal price = group?.ProjectFunction?.Quantity > 0 ? (group?.ProjectFunction?.TotalCosts / group?.ProjectFunction?.Quantity).Value : 0;
                    totalCount++;
                    count++;
                    equipmentString = $@"{equipmentString} 
                        {tableTemplate
                        .Replace("{num}", count.ToString())
                        .Replace("{code}", "")
                        .Replace("{name}", group.Name)
                        .Replace("{count}", group.Quantity.ToString())
                        .Replace("{numId}", "")
                        .Replace("{price}", $"{price.ToString("F")}{currency}") // Currency
                        .Replace("{totalPrice}", $"{group?.ProjectFunction?.TotalCosts.ToString("F")}{currency}")}";

                    //}
                };

                blocksHTML = $@"{blocksHTML} {divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Транспорт")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            };

            count = 0;
            equipmentString = String.Empty;

            if (document.AdditionalCosts != null && document.AdditionalCosts.Count > 0)
            {
                foreach (var additional in document.AdditionalCosts)
                {
                    totalCount++;
                    count++;
                    //totalPrice += total;
                    equipmentString = $@"{equipmentString} 
                        {tableTemplate
                        .Replace("{num}", count.ToString())
                        .Replace("{code}", "")
                        .Replace("{name}", additional.Name)
                        .Replace("{count}", "")
                        .Replace("{numId}", "")
                        .Replace("{price}", "") // Currency
                        .Replace("{totalPrice}", $"{additional.SalePrice.ToString("F")} {currency}")}";

                }

                blocksHTML = $@"{blocksHTML} {divTemplate.Substring(0, startWith).Replace("{blockTitle}", "Дополнительные расходы")}
                    { equipmentString}
                    { divTemplate.Substring(endWith + "[/equipmentListBody]".Length)}";
            }

            count = 0;
            equipmentString = String.Empty;

            content = $@"{ content.Substring(0, startTableBlockWith)}
                    { blocksHTML}
                    { content.Substring(endTableBlockWith + "[/divTableBody]".Length)}";
            var date = DateTime.Now;
            content = content
                .Replace("{bankName}", document.Company?.Bank)
                .Replace("{bikNumber}", document.Company?.Bic)
                .Replace("{inn}", document.Company?.Inn)
                .Replace("{company}", document.Company?.CompanyName)
                .Replace("{billNumber}", document.Company?.CheckingAccount)
                .Replace("{billNumber2}", document.Company?.CorrespondentAccount)
                .Replace("{kpp}", document.Company?.Kpp)
                .Replace("{projectManager}", $"{document.Company?.DirectorInfo?.LastName} {(string.IsNullOrEmpty(document.Company?.DirectorInfo?.FirstName) ? "" : document.Company?.DirectorInfo?.FirstName?.Substring(0, 1))}{(string.IsNullOrEmpty(document.Company?.DirectorInfo?.MiddleName) ? "" : document.Company?.DirectorInfo?.MiddleName?.Substring(0, 1))}")
                .Replace("{accountant}", $"{document.Company?.AccountantInfo?.LastName} {(string.IsNullOrEmpty(document.Company?.AccountantInfo?.FirstName) ? "" : document.Company?.AccountantInfo?.FirstName?.Substring(0, 1))}{(string.IsNullOrEmpty(document.Company?.AccountantInfo?.MiddleName) ? "" : document.Company?.AccountantInfo?.MiddleName?.Substring(0, 1))}")
                .Replace("{invoiceNumber}", document.Number)
                .Replace("{invoiceDate}", date.ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU")))
                .Replace("{supplierInfo}", supplierInfo)
                .Replace("{buyerInfo}", buyerInfo)
                .Replace("{reasonInfo}", reasonInfo)
                .Replace("{totalCount}", totalCount.ToString())
                .Replace("{projectName}", document.Project?.Name)
                .Replace("{supplierPhone}", String.Join(",", document.Company?.Phones))
                .Replace("{supplierEmail}", document.Company?.InvoiceEmail)
                .Replace("{suplierSite}", document.Company?.Website)
                .Replace("{dateValid}", date.AddMonths(1).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU")))
                //.Replace("{totalPrice}", totalPrice.ToString("F"))
                //.Replace("{totalVATPrice}", (totalPrice * 20 / 120).ToString("F")))
                //.Replace("{totalPayment}", totalPrice.ToString("F"))
                //.Replace("{totalProjectPrice}", totalPrice.ToString("F))

                .Replace("{totalPrice}", $"{document?.ProjectFinancialCategory?.Total.ToString("F")} {currency}")
                .Replace("{totalVATPrice}", $"{(document?.FinancialInfo?.TotalIncVat - document?.ProjectFinancialCategory?.Total)?.ToString("F")} {currency}")
                .Replace("{totalPayment}", $"{document?.FinancialInfo?.TotalIncVat.ToString("F")} {currency}")
                .Replace("{totalProjectPrice}", $"{document?.FinancialInfo?.TotalIncVat.ToString("F")} {currency}");

            return content;
        }
    }
}
