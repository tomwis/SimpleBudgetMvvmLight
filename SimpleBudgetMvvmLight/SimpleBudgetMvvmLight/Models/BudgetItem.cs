using GalaSoft.MvvmLight;
using SimpleBudgetMvvmLight.Models.Enums;
using SQLite;
using System.ComponentModel;

namespace SimpleBudgetMvvmLight.Models
{
    [Table("BudgetItem")]
    public class BudgetItem : ViewModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        private double _amount;
        public double Amount
        {
            get { return _amount; }

            set
            {
                if (_amount == value) return;

                _amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public BudgetItemType Type { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
