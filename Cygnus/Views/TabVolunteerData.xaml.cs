using System.Windows.Controls;
using Cygnus.ViewModels;

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
