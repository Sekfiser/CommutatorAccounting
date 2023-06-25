namespace CommutatorAccounting.Models
{
    public class CommutatorExtendedModel
    {
        public IEnumerable<Commutator> Commutators { get; }
        public PageCommutatorModel PageCommutatorModel { get; }
        public FilterCommutatorModel FilterCommutatorModel { get; }
        public SortCommutatorModel SortCommutatorModel { get; }
        public Commutator? SearchCommutator { get; }
        public CommutatorExtendedModel(IEnumerable<Commutator> commutators, PageCommutatorModel pageCommutatorModel,
            FilterCommutatorModel filterCommutatorModel, SortCommutatorModel sortCommutatorModel, Commutator? searchCommutator = null)
        {
            Commutators = commutators;
            PageCommutatorModel = pageCommutatorModel;
            FilterCommutatorModel = filterCommutatorModel;
            SortCommutatorModel = sortCommutatorModel;
            SearchCommutator = searchCommutator;
        }
    }
}
