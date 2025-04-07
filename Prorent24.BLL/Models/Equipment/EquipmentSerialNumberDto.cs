using Newtonsoft.Json;
using Prorent24.BLL.Models.Equipment.Supplier;
using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prorent24.BLL.Models.Equipment
{
    public class EquipmentSerialNumberDto : BaseDto<int>
    {
        public int EquipmentId { get; set; }

        [IncludeToGrid(Order = 5, ColumnGroup = ColumnGroupEnum.General)]
        public string SerialNumber { get; set; }

        [IncludeToGrid(Order = 6, ColumnGroup = ColumnGroupEnum.General, IsDisplay = false)]
        public string InternalReference { get; set; }

        [IncludeToGrid(Order = 7, ColumnGroup = ColumnGroupEnum.General)]
        //public DateTime PurchaseDate { get; set; }
        public string PurchaseDate { get; set; }

        [IncludeToGrid(Order = 8, ColumnGroup = ColumnGroupEnum.General)]
        public decimal PurchasePrice { get; set; }
        public bool CalculateBookValueAutomatically { get; set; }
        // CalculateBookValueAutomatically == true
        public decimal DepreciationPerMonth { get; set; }
        // CalculateBookValueAutomatically == false
        public decimal BookValue { get; set; }
        public string Remark { get; set; }

        [IncludeToGrid(Order = 9, ColumnGroup = ColumnGroupEnum.General)]
        public bool Active { get; set; }
        public string SuppliersInfoJson { get; set; }
        public List<SupplierInfoDto> SuppliersInfo
        {
            get
            {
                if (this.SuppliersInfoJson != null)
                    return JsonConvert.DeserializeObject<List<SupplierInfoDto>>(this.SuppliersInfoJson);
                else
                    return null;
            }
        }
    }

}
