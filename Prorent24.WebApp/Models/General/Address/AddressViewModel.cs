namespace Prorent24.WebApp.Models.General.Address
{
    public class AddressViewModel
    {
        public int? AddressId { get; set; }
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
    }
}
