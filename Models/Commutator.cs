namespace CommutatorAccounting.Models
{
    public class Commutator
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Ip { get; set; }
        public string? Mac { get; set; }
        public string? Vlan { get; set; }
        public string? SerialNumber { get; set; }
        public string? StockNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string? InstallationFloor { get; set; }
        public string? Comment { get; set; }
    }
}
