using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation Navigation { get; set; }

        public async Task Navigate<T>()
        {
            var page = Activator.CreateInstance(typeof(T)) as Page;
            await Navigation?.PushAsync(page);
        }

        public async Task GoBack()
        {
            await Navigation?.PopAsync();
        }
    }
}
