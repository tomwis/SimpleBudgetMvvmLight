using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight
{
    public partial class MonthEditPage : ContentPage
    {
        public MonthEditPage()
        {
            InitializeComponent();
            BindingContext = App.Locator.MonthEdit;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            BindingContext = null;
            App.Locator.MonthEdit.OnViewDisappearing();
        }
    }
}
