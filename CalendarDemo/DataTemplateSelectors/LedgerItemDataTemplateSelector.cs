using System.Windows;
using System.Windows.Controls;
using Calendar.Models;

namespace CalendarDemo.DataTemplateSelectors
{
    public class LedgerItemDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ZeroHour { get; set; }
        public DataTemplate HalfHour { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (!(item is LedgerItem ledgerTime) || ledgerTime.Time.Minute == 0)
                return ZeroHour;
            return HalfHour;
        }
    }
}