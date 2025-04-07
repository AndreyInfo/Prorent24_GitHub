using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Subhire
{
    public class SubhireListDto
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public List<SubhireShortDto> Subhires { get; set; }

    }
}
