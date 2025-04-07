using Prorent24.Common.Attributes;
using Prorent24.Enum.Directory;

namespace Prorent24.BLL.Models.General.Address
{
    public class AddressDto // : BaseDto<string> // некоректно будувати саме таку вкладеність (
    {
        public int? AddressId { get; set; }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 4, ColumnGroup = ColumnGroupEnum.Address)]
        public string Address { get; set; }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 1, ColumnGroup = ColumnGroupEnum.Address)]
        public string Country { get; set; }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 2, ColumnGroup = ColumnGroupEnum.Address)]
        public string City { get; set; }

        //[IncludeToGrid(IsDisplay = false, ColumnGroup = ColumnGroupEnum.Address)]
        public string Region { get; set; }


        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 5, ColumnGroup = ColumnGroupEnum.Address)]
        public string Number { get; set; }

        [IncludeToGrid(TreeColumn = true, TreeColumnOrder = 3, ColumnGroup = ColumnGroupEnum.Address)]
        public string PostalCode { get; set; }

       // [IncludeToGrid(IsDisplay = false, ColumnGroup = ColumnGroupEnum.Address)]
        public string AdditionalAddress { get; set; }

        [IncludeToGrid(IsDisplay = false)]
        public bool View { get; set; } = true;
        [IncludeToGrid(IsDisplay = false)]
        public bool Edit { get; set; } = true;
        [IncludeToGrid(IsDisplay = false)]
        public bool Delete { get; set; } = true;

        public int? CountryId { get; set; }
        public long? Lat { get; set; }
        public long? Long { get; set; }
        public long? Alt { get; set; }
    }
}
