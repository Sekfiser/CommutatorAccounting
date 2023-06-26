using System.ComponentModel.DataAnnotations;

namespace CommutatorAccounting.Models
{
    public class Commutator
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "Не задана модель")]
        public string? Model { get; set; }
        [Required(ErrorMessage = "IP пуст"), RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$", ErrorMessage = "IP не соответствует шаблону")]
        public string? Ip { get; set; }
        [Required(ErrorMessage = "MAC пуст"), RegularExpression(@"^([0-9A-Fa-f]{2}[:]){5}([0-9A-Fa-f]{2})$", ErrorMessage = "MAC не соответствует шаблону")]
        public string? Mac { get; set; }
        [Required(ErrorMessage = "Не задан VLAN")]
        public string? Vlan { get; set; }
        [Required(ErrorMessage = "Серийный номер пуст"), RegularExpression(@"^(\d|\w){8,}$", ErrorMessage = "Серийный номер не соответствует шаблону")]
        public string? SerialNumber { get; set; }
        [Required(ErrorMessage = "Инвентарный номер пуст"), RegularExpression(@"^(\d){8,}$", ErrorMessage = "Инвентарный номер не соответствует шаблону")]
        public string? StockNumber { get; set; }
        [Required(ErrorMessage = "Дата покупки пуста")]
        public DateTime? PurchaseDate { get; set; }
        public DateTime? InstallationDate { get; set; }
        public string? InstallationFloor { get; set; }
        public string? Comment { get; set; }
    }
}
