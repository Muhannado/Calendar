using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Calendar.Contracts;

namespace CalendarDemo.ViewModels
{
    public class CalendarItemViewModel : ICalendarItem
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _startTime;
        private DateTime? _endTime;

        public string DisplayName { get; set; }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value == _startTime) return;
                _startTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime? EndTime
        {
            get => _endTime;
            set
            {
                if (value == _endTime) return;
                _endTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsFullDay { get; }
        public bool IsEditable { get; }
        public bool IsDraggable { get; }

        public CalendarItemViewModel(string displayName, DateTime startTime, DateTime? endTime)
        {
            DisplayName = displayName;
            StartTime = startTime;
            EndTime = endTime;
            IsFullDay = false;
            IsEditable = true;
            IsDraggable = true;
        }
        
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
