﻿using System;
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

        public void EditCalendarItem(CalendarItemEventArgs eventArgs)
        {
            var message = new StringBuilder();
            if (eventArgs.IsNewItem)
            {
                var calendarItemViewModel =
                    new CalendarItemViewModel("New Item", eventArgs.StartTime, eventArgs.EndTime);
                CalendarItems.Add(calendarItemViewModel);
            }
            else
            {
                message.AppendLine("Edit Calendar Item:");
                message.AppendLine($"{eventArgs.CalendarItem.DisplayName}");
                message.AppendLine($"{eventArgs.CalendarItem.StartTime}");
                message.AppendLine($"{eventArgs.CalendarItem.EndTime}");
                MessageBox.Show(message.ToString());
            }
        }

        public void UpdateCalendarItem(UpdateCalendarItemEventArgs eventArgs)
        {
            if (!(eventArgs.UpdatedCalendarItem is CalendarItemViewModel draggedCalendarItem))
                return;
            CalendarItems.Remove(draggedCalendarItem);
            CalendarItems.Add(draggedCalendarItem);
        }


        private static DateTime CalculateStartOfWeekDate(DateTime dt)
        {
            var startOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            var diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}