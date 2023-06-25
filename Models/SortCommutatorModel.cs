using CommutatorAccounting.Models.SortColumns;

namespace CommutatorAccounting.Models
{
    public class SortCommutatorModel
    {
        public SortCommutatorColumns IdSort { get; }
        public SortCommutatorColumns ModelSort { get; }
        public SortCommutatorColumns IpSort { get; }
        public SortCommutatorColumns MacSort { get; }
        public SortCommutatorColumns VlanSort { get; }
        public SortCommutatorColumns SerialNumberSort { get; }
        public SortCommutatorColumns StockNumberSort { get; }
        public SortCommutatorColumns PurchaseDateSort { get; }
        public SortCommutatorColumns InstallationDateSort { get; }
        public SortCommutatorColumns InstallationFloorSort { get; }
        public SortCommutatorColumns CommentSort { get; }
        public SortCommutatorColumns Current { get; }
        public SortCommutatorModel(SortCommutatorColumns sortColumn)
        {
            IdSort = sortColumn == SortCommutatorColumns.IdAsc ? SortCommutatorColumns.IdDesc : SortCommutatorColumns.IdAsc;
            ModelSort = sortColumn == SortCommutatorColumns.ModelAsc ? SortCommutatorColumns.ModelDesc : SortCommutatorColumns.ModelAsc;
            IpSort = sortColumn == SortCommutatorColumns.IpAsc ? SortCommutatorColumns.IpDesc : SortCommutatorColumns.IpAsc;
            MacSort = sortColumn == SortCommutatorColumns.MacAsc ? SortCommutatorColumns.MacDesc : SortCommutatorColumns.MacAsc;
            VlanSort = sortColumn == SortCommutatorColumns.VlanAsc ? SortCommutatorColumns.VlanDesc : SortCommutatorColumns.VlanAsc;
            SerialNumberSort = sortColumn == SortCommutatorColumns.SerialNumberAsc ? SortCommutatorColumns.SerialNumberDesc : SortCommutatorColumns.SerialNumberAsc;
            StockNumberSort = sortColumn == SortCommutatorColumns.StockNumberAsc ? SortCommutatorColumns.StockNumberDesc : SortCommutatorColumns.StockNumberAsc;
            PurchaseDateSort = sortColumn == SortCommutatorColumns.PurchaseDateAsc ? SortCommutatorColumns.PurchaseDateDesc : SortCommutatorColumns.PurchaseDateAsc;
            InstallationDateSort = sortColumn == SortCommutatorColumns.InstallationDateAsc ? SortCommutatorColumns.InstallationDateDesc : SortCommutatorColumns.InstallationDateAsc;
            InstallationFloorSort = sortColumn == SortCommutatorColumns.InstallationFloorAsc ? SortCommutatorColumns.InstallationFloorDesc : SortCommutatorColumns.InstallationFloorAsc;
            CommentSort = sortColumn == SortCommutatorColumns.CommentAsc ? SortCommutatorColumns.CommentDesc : SortCommutatorColumns.CommentAsc;
            Current = sortColumn;
        }
    }
}
