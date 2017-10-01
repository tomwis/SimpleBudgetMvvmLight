using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.Dashboard;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Locator.Dashboard.OnViewAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.Locator.Dashboard.OnViewDisappearing();
        }
    }
}
