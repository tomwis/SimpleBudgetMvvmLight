using SimpleBudgetMvvmLight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmLight.Services
{
    public class MonthService : IMonthService
    {
        MonthItem _selectedMonth;
        public MonthItem SelectedMonth
        {
            get => _selectedMonth;
            set
            {
                _selectedMonth = value;
                OnSelectedMonthChanged?.Invoke(this, value);
            }
        }

        public event EventHandler<MonthItem> OnSelectedMonthChanged;
    }
}
