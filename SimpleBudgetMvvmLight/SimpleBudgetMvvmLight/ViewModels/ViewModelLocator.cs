using GalaSoft.MvvmLight.Ioc;
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
            SimpleIoc.Default.Register<INavigationService, NavigationService>();
            SimpleIoc.Default.Register<IMonthService, MonthService>();
        }

        public DashboardViewModel Dashboard => ServiceLocator.Current.GetInstance<DashboardViewModel>();
        public MonthEditViewModel MonthEdit => ServiceLocator.Current.GetInstance<MonthEditViewModel>();
    }
}
