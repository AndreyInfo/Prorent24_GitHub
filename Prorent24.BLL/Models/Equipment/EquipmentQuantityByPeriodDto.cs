using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentQuantityByPeriodDto
    {
        public int Id { get; set; }

        public List<QuantityByPerid> QuantityByPerids { get;set;}
    }

    public class QuantityByPerid
    {
        public int Quantity { get; set; }

        public DateTime DateInPeriod { get; set; }

        //public DateTime DateTo { get; set; }
    }
}
