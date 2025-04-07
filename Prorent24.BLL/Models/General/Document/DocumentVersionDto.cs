using System;

namespace Prorent24.BLL.Models.General.Document
{
    public class DocumentVersionDto
    {
        public int? NumberSeriesId { get; set; }
        public string Number { get; set; }
        public int Version { get; set; }
        public DateTime Date { get; set; }
    }
}