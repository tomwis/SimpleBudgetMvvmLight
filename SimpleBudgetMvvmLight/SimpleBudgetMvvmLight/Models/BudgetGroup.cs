using SimpleBudgetMvvmLight.Models.Enums;
using System.Collections.ObjectModel;

namespace SimpleBudgetMvvmLight.Models
{
    public class BudgetGroup : ObservableCollection<BudgetItem>
    {
        public string Title { get; set; }
        public BudgetItemType Type { get; set; }
    }
}
