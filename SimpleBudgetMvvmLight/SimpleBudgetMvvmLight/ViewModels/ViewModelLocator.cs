using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using SimpleBudgetMvvmLight.PlatformServices;
using SimpleBudgetMvvmLight.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<MonthEditViewModel>();
            SimpleIoc.Default.Register<IDbService>(() => new DbService(DependencyService.Get<IFileService>()));
            SimpleIoc.Default.Register<Services.INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IMonthService, MonthService>();
        }

        public void SetDialogService(IDialogService dialogService)
        {
            SimpleIoc.Default.Register(() => dialogService);
        }

        public DashboardViewModel Dashboard => ServiceLocator.Current.GetInstance<DashboardViewModel>();
        public MonthEditViewModel MonthEdit => ServiceLocator.Current.GetInstance<MonthEditViewModel>();
    }
}
