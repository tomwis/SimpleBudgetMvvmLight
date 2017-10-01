using SimpleBudgetMvvmLight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBudgetMvvmLight.Services
{
    public interface IMonthService
    {
        event EventHandler<MonthItem> OnSelectedMonthChanged;
        MonthItem SelectedMonth { get; set; }
    }
}
