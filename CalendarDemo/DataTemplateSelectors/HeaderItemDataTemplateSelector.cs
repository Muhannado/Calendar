using System;
using System.Windows;
using System.Windows.Controls;

namespace CalendarDemo.DataTemplateSelectors
{
    public class HeaderItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultDay { get; set; }
        public DataTemplate CurrentDay { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is DateTime headerDate))
                return DefaultDay;
            return headerDate.Date == DateTime.Today ? CurrentDay : DefaultDay;
        }
    }
}