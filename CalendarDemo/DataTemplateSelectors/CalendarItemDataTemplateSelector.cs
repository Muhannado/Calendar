using System.Windows;
using System.Windows.Controls;
using CalendarDemo.ViewModels;

namespace CalendarDemo.DataTemplateSelectors
{
    public class CalendarItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultItem { get; set; }
        public DataTemplate SpecialItem { get; set; }
        public DataTemplate MissingItem { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is CalendarItemViewModel calendarItem))
            {
                return MissingItem;
            }
            return calendarItem.StartTime.DayOfWeek == System.DayOfWeek.Friday ||
                   calendarItem.StartTime.DayOfWeek == System.DayOfWeek.Saturday ? SpecialItem : DefaultItem;
        }
    }
}