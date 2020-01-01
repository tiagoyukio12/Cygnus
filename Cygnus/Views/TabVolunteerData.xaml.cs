using Cygnus.ViewModels;
using System.Windows.Controls;

namespace Cygnus.Views
{
    /// <summary>
    /// Interaction logic for TabVolunteerData.xaml
    /// </summary>
    public partial class TabVolunteerData : UserControl
    {
        public TabVolunteerData()
        {
            InitializeComponent();
            DataContext = new TabVolunteerViewModel();
        }
    }
}
