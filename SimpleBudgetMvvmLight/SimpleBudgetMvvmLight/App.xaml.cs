using GalaSoft.MvvmLight.Ioc;
using SimpleBudgetMvvmLight.Services;
using SimpleBudgetMvvmLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace SimpleBudgetMvvmLight
{
    public partial class App : Application
    {
        static ViewModelLocator _locator;
        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public App()
        {
            InitializeComponent();

            Resources["Locator"] = Locator;

            MainPage = new NavigationPage(new DashboardPage());
            SimpleIoc.Default.GetInstance<INavigationService>().Navigation = MainPage.Navigation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
