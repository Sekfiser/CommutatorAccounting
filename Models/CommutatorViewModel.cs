using CommutatorAccounting.Models.SortColumns;

namespace CommutatorAccounting.Models
{
    public class CommutatorViewModel
    {
        public IEnumerable<Commutator>? Commutators { get; }
        public PageCommutatorModel? PageViewModel { get; }
        public FilterCommutatorModel FilterViewModel { get; }
        public SortCommutatorColumns SortViewModel { get; }
        public CommutatorViewModel(IEnumerable<Commutator> commutators, PageCommutatorModel pageViewModel,
            FilterCommutatorModel filterViewModel, SortCommutatorColumns sortViewModel)
        {
            Commutators = commutators;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
