using Prorent24.Entity.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Prorent24.Entity.General
{
    [Table("dbo_addresses")]
    public class AddressEntity: BaseEntity
    {
        public string Address { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public int? CountryId { get; set; }
        public string AdditionalAddress { get; set; }
        public long? Lat { get; set; }
        public long? Long { get; set; }
        public long? Alt { get; set; }

        public void TransferFromEntity()
        {
            throw new NotImplementedException();
        }
    }
}
