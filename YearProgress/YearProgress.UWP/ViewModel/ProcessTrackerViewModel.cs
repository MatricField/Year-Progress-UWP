﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace YearProgress.UWP.ViewModel
{
    public class ProgressTrackerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int Minimum { get; } = 0;

        private int _Maximum;

        public int Maximum
        {
            get => _Maximum;
            set
            {
                Interlocked.Exchange(ref _Maximum, value);
                OnPropertyChanged(nameof(Maximum));
            }
        }

        private int _Value;

        public int Value
        {
            get => _Value;
            set
            {
                Interlocked.Exchange(ref _Value, value);
                OnPropertyChanged(nameof(Value));
            }
        }

        public ProgressTrackerViewModel()
        {
            UpdateProgress();
        }


        public virtual void UpdateProgress()
        {
            var now = DateTime.Now;
            var yearBegin = new DateTime(now.Year, 1, 1, 0, 0, 0);
            var yearSpan = new DateTime(now.Year, 12, 31) - yearBegin;
            Maximum = yearSpan.Days;
            var elapsed = Convert.ToDouble((now - yearBegin).Ticks);
            var span = Convert.ToDouble(yearSpan.Ticks);
            Value =
                Convert.ToInt32(
                    (elapsed / span) * Maximum
                    );
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
