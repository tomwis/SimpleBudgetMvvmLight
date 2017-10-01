using GalaSoft.MvvmLight;
using SimpleBudgetMvvmLight.Helpers;
using SimpleBudgetMvvmLight.Models;
using SimpleBudgetMvvmLight.Resources;
using SimpleBudgetMvvmLight.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        IDbService _dbService;
        INavigationService _navigationService;
        IMonthService _monthService;

        public DashboardViewModel(IDbService dbService, INavigationService navigationService, IMonthService monthService)
        {
            _dbService = dbService;
            _navigationService = navigationService;
            _monthService = monthService;
        }

        public void OnViewAppearing()
        {
            var months = _dbService.LoadAllMonths();
            foreach (var month in months)
            {
                month.MonthName = CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[month.Month - 1];
                if (month.BackgroundColor == null)
                {
                    month.BackgroundColor = ColorHelper.GenerateRandomPleasingColor();
                }
            }
            Months = new List<MonthItem>(months.OrderByDescending(s => s.Year).ThenByDescending(m => m.Month));
            PropertyChanged += DashboardViewModel_PropertyChanged;
        }

        public void OnViewDisappearing()
        {
            PropertyChanged -= DashboardViewModel_PropertyChanged;
        }

        private async void DashboardViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(SelectedMonth):
                    if (SelectedMonth != null)
                    {
                        _monthService.SelectedMonth = SelectedMonth;
                        await _navigationService.Navigate<MonthEditPage>();
                        SelectedMonth = null;
                    }
                    break;
            }
        }

        public Command AddMonthCmd => new Command(async () =>
        {
            _monthService.SelectedMonth = null;
            await _navigationService.Navigate<MonthEditPage>();
        });

        private List<MonthItem> _months;
        public List<MonthItem> Months
        {
            get { return _months; }

            set
            {
                if (_months == value) return;

                _months = value;
                RaisePropertyChanged(nameof(Months));
            }
        }

        private MonthItem _selectedMonth;
        public MonthItem SelectedMonth
        {
            get { return _selectedMonth; }

            set
            {
                if (_selectedMonth == value) return;

                _selectedMonth = value;
                RaisePropertyChanged(nameof(SelectedMonth));
            }
        }

        public string Today
        {
            get
            {
                var now = DateTime.Now;
                var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                return $"{AppResources.DaysToPayout}: {daysInMonth - now.Day + 1}";
            }
        }
    }
}
