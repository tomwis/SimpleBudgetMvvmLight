using GalaSoft.MvvmLight;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SimpleBudgetMvvmLight.Models
{
    [Table("MonthItem")]
    public class MonthItem : ViewModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private int _year;
        [Indexed(Name = "MonthId", Order = 1, Unique = true)]
        public int Year
        {
            get { return _year; }

            set
            {
                if (_year == value) return;

                _year = value;
                RaisePropertyChanged(nameof(Year));
            }
        }

        [Indexed(Name = "MonthId", Order = 2, Unique = true)]
        public int Month { get; set; }

        private double _lastMonthEarnings;
        public double LastMonthEarnings
        {
            get { return _lastMonthEarnings; }

            set
            {
                if (_lastMonthEarnings == value) return;

                _lastMonthEarnings = value;
                RaisePropertyChanged(nameof(LastMonthEarnings));
            }
        }

        private double _moneyLeft;
        public double MoneyLeft
        {
            get { return _moneyLeft; }

            set
            {
                if (_moneyLeft == value) return;

                _moneyLeft = value;
                RaisePropertyChanged(nameof(MoneyLeft));
            }
        }

        public string MonthName { get; set; }
        public string BackgroundColor { get; set; } = "#00000000";
    }
}
