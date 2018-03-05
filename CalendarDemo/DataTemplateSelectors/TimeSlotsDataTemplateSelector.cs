using System;
using System.Windows;
using System.Windows.Controls;

namespace CalendarDemo.DataTemplateSelectors
{
    public class TimeSlotsDataTemplateSelector : DataTemplateSelector
    {
        public int MinimumWorkHour { get; set; }
        public int MaximumWorkHour { get; set; }

        public DataTemplate Default { get; set; }
        public DataTemplate Weekend { get; set; }
        public DataTemplate NonWorkHours { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is DateTime timeSlotDate))
                return Default;

            if (timeSlotDate.DayOfWeek == DayOfWeek.Friday || timeSlotDate.DayOfWeek == DayOfWeek.Saturday)
                return Weekend;

            if (timeSlotDate.Hour < MinimumWorkHour || timeSlotDate.Hour > MaximumWorkHour)
                return NonWorkHours;
            return Default;
        }
    }
}