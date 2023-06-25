using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommutatorAccounting.Models
{
    public class FilterCommutatorModel
    {
        public FilterCommutatorModel(List<Commutator> commutators) 
        {
            Commutators = new SelectList(commutators);
        }

        public SelectList Commutators { get; }
        public int SelectedCommutator { get; }
    }
}
