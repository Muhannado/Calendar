using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Windows;
using Calendar.EventArgs;
using Caliburn.Micro;

namespace CalendarDemo.ViewModels
{
    public class CalendarViewModel : Screen
    {
        private ObservableCollection<CalendarItemViewModel> _calendarItems;

        public ObservableCollection<CalendarItemViewModel> CalendarItems
        {
            get => _calendarItems;
            set
            {
                _calendarItems = value;
                NotifyOfPropertyChange();
            }
        }

        public CalendarViewModel()
        {
            var calendarItems = new List<CalendarItemViewModel>();
            var startOfWeek = CalculateStartOfWeekDate(DateTime.Today);
            var startTime = startOfWeek.AddHours(8);
            var endTime = startTime.AddHours(9);

            for (int i = 0; i < 7; i++)
            {
                var calendarItem = new CalendarItemViewModel($"Calendar Item {i + 1}", startTime, endTime);
                startTime = startTime.AddDays(1);
                endTime = endTime.AddDays(1);
                calendarItems.Add(calendarItem);
            }
            CalendarItems = new ObservableCollection<CalendarItemViewModel>(calendarItems);
        }

        public void EditCalendarItemAsync(CalendarItemEventArgs eventArgs)
        {
            var message = new StringBuilder();
            if (eventArgs.IsNewItem)
            {
                message.AppendLine("New Calendar Item:");
                message.AppendLine($"{eventArgs.StartTime}");
                message.AppendLine($"{eventArgs.EndTime}");
            }
            else
            {
                message.AppendLine("Edit Calendar Item:");
                message.AppendLine($"{eventArgs.CalendarItem.DisplayName}");
                message.AppendLine($"{eventArgs.CalendarItem.StartTime}");
                message.AppendLine($"{eventArgs.CalendarItem.EndTime}");
            }
            MessageBox.Show(message.ToString());
        }

        public void UpdateCalendarItemAsync(UpdateCalendarItemEventArgs eventArgs)
        {
            if (!(eventArgs.UpdatedCalendarItem is CalendarItemViewModel draggedCalendarItem))
                return;
            var message = new StringBuilder();
            message.AppendLine("Updated Calendar Item:");
            message.AppendLine($"{draggedCalendarItem.DisplayName}");
            message.AppendLine($"{draggedCalendarItem.StartTime}");
            message.AppendLine($"{draggedCalendarItem.EndTime}");
            MessageBox.Show(message.ToString());
        }


        private static DateTime CalculateStartOfWeekDate(DateTime dt)
        {
            var startOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}