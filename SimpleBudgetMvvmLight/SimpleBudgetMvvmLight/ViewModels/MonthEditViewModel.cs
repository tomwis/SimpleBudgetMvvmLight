using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleBudgetMvvmLight.Helpers;
using SimpleBudgetMvvmLight.Models;
using SimpleBudgetMvvmLight.Models.Enums;
using SimpleBudgetMvvmLight.Resources;
using SimpleBudgetMvvmLight.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight.ViewModels
{
    public class MonthEditViewModel : ViewModelBase
    {
        IDbService _dbService;
        INavigationService _navigationService;
        IMonthService _monthService;
        bool _isEditing;

        public MonthEditViewModel(IDbService dbService, INavigationService navigationService, IMonthService monthService)
        {
            _dbService = dbService;
            _navigationService = navigationService;
            _monthService = monthService;
            Init(_monthService.SelectedMonth);
            _monthService.OnSelectedMonthChanged += _monthService_OnSelectedMonthChanged;
        }

        private void _monthService_OnSelectedMonthChanged(object sender, MonthItem e)
        {
            Device.BeginInvokeOnMainThread(() => Init(e));
        }

        void Init(MonthItem month)
        {
            FillData();
            if (month is MonthItem monthItem)
            {
                _isEditing = true;
                MonthItem = monthItem;
                var (_, budgetItems) = _dbService.LoadMonth(monthItem.Month, monthItem.Year);

                foreach (var group in BudgetItems)
                {
                    foreach (var item in budgetItems.Where(s => s.Type == group.Type))
                    {
                        group.Add(item);
                    }
                }
            }
            else
            {
                _isEditing = false;
                var now = DateTime.Now;
                MonthItem = new MonthItem
                {
                    Month = now.Month,
                    Year = now.Year
                };
            }

            SelectedMonth = Months.FirstOrDefault(s => s.Number == MonthItem.Month);
            PropertyChanged += MonthEditViewModel_PropertyChanged;
            MonthItem.PropertyChanged += MonthItem_PropertyChanged;
            foreach (var group in BudgetItems)
            {
                foreach (var item in group)
                {
                    item.PropertyChanged += BudgetItem_PropertyChanged;
                }
            }
        }

        void FillData()
        {
            BudgetItems = new ObservableCollection<BudgetGroup>
            {
                new BudgetGroup { Title = AppResources.RecurringItemsGroupLabel, Type = BudgetItemType.Recurring },
                new BudgetGroup { Title = AppResources.OneTimeItemsGroupLabel, Type = BudgetItemType.OneTime }
            };
            int yearsToShow = 10;
            Years = Enumerable.Range(DateTime.Now.Year - yearsToShow, yearsToShow + 1).ToList();
            Months = new List<MonthDisplay>(DateTimeFormatInfo.CurrentInfo.MonthNames.Where(s => !string.IsNullOrEmpty(s)).Select((s, i) => new MonthDisplay(s, i + 1)));
        }

        private void BudgetItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(BudgetItem.Amount):
                    CalculateMoneyLeft();
                    break;
            }
        }

        private void MonthItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(MonthItem.LastMonthEarnings):
                    CalculateMoneyLeft();
                    break;
            }
        }

        void CalculateMoneyLeft()
        {
            var moneyLeft = MonthItem.LastMonthEarnings;
            foreach (var group in BudgetItems)
            {
                foreach (var item in group)
                {
                    moneyLeft -= item.Amount;
                }
            }
            MonthItem.MoneyLeft = moneyLeft;
        }

        public void OnViewDisappearing()
        {
            PropertyChanged -= MonthEditViewModel_PropertyChanged;
            MonthItem.PropertyChanged -= MonthItem_PropertyChanged;
            MonthItem = null;
            foreach (var group in BudgetItems)
            {
                foreach (var item in group)
                {
                    item.PropertyChanged -= BudgetItem_PropertyChanged;
                }
            }
        }

        private void MonthEditViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SelectedMonth):
                    if (SelectedMonth != null)
                    {
                        MonthItem.Month = SelectedMonth.Number;
                    }
                    break;
            }
        }

        public RelayCommand<BudgetItemType> AddExpeseCmd => new RelayCommand<BudgetItemType>(t => 
        {
            var newItem = new BudgetItem { Type = t };
            newItem.PropertyChanged += BudgetItem_PropertyChanged;
            BudgetItems.First(s => s.Type == t).Add(newItem);
        });

        public RelayCommand<BudgetItem> DeleteExpeseCmd => new RelayCommand<BudgetItem>(b => 
        {
            var group = BudgetItems.First(s => s.Type == b.Type);
            b.PropertyChanged -= BudgetItem_PropertyChanged;
            group.Remove(b);
            CalculateMoneyLeft();
        });

        public RelayCommand SaveCmd => new RelayCommand(async () => 
        {
            // save
            var items = new List<BudgetItem>(BudgetItems[0]);
            items.AddRange(BudgetItems[1]);

            foreach (var item in items)
            {
                item.Month = MonthItem.Month;
                item.Year = MonthItem.Year;
            }

            if (!_isEditing && _dbService.HasMonth(MonthItem.Month, MonthItem.Year))
            {
                await App.Current.MainPage.DisplayAlert("", AppResources.MothAlreadyExistsMessage, AppResources.Ok);
            }
            else
            {
                MonthItem.BackgroundColor = ColorHelper.GenerateRandomPleasingColor();
                _dbService.SaveMonth(MonthItem, items, _isEditing);
                await _navigationService.GoBack();
            }
        });
        
        private MonthItem _monthItem;
        public MonthItem MonthItem
        {
            get { return _monthItem; }

            set
            {
                if (_monthItem == value) return;

                _monthItem = value;
                RaisePropertyChanged(nameof(MonthItem));
            }
        }

        private ObservableCollection<BudgetGroup> _budgetItems;
        public ObservableCollection<BudgetGroup> BudgetItems
        {
            get { return _budgetItems; }

            set
            {
                if (_budgetItems == value) return;

                _budgetItems = value;
                RaisePropertyChanged(nameof(BudgetItems));
            }
        }

        private List<int> _years;
        public List<int> Years
        {
            get { return _years; }

            set
            {
                if (_years == value) return;

                _years = value;
                RaisePropertyChanged(nameof(Years));
            }
        }

        private List<MonthDisplay> _months;
        public List<MonthDisplay> Months
        {
            get { return _months; }

            set
            {
                if (_months == value) return;

                _months = value;
                RaisePropertyChanged(nameof(Months));
            }
        }

        private MonthDisplay _selectedMonth;
        public MonthDisplay SelectedMonth
        {
            get { return _selectedMonth; }

            set
            {
                if (_selectedMonth == value) return;

                _selectedMonth = value;
                RaisePropertyChanged(nameof(SelectedMonth));
            }
        }
    }
}
