using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight.Services
{
    public interface INavigationService
    {
        INavigation Navigation { get; set; }
        Task Navigate<T>();
        Task GoBack();
    }
}
