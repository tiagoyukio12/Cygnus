using Cygnus.ViewModels;
using System.Windows.Controls;

namespace Cygnus.Views
{
    /// <summary>
    /// Interaction logic for TabCalendarData.xaml
    /// </summary>
    public partial class TabCalendarData : UserControl
    {
        public TabCalendarData()
        {
            InitializeComponent();
            DataContext = new TabCalendarViewModel(dataGrid, subGrid);
        }
    }
}
