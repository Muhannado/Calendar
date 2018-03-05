using Caliburn.Micro;

namespace CalendarDemo.ViewModels
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public ShellViewModel()
        {
            base.DisplayName = "My Calendar";
            Items.Add(new CalendarViewModel());
        }

        public Screen CalendarViewModel => Items[0];
    }
}