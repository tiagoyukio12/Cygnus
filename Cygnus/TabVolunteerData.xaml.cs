using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for TabVolunteerData.xaml
    /// </summary>
    public partial class TabVolunteerData : UserControl
    {
        public TabVolunteerData()
        {
            InitializeComponent();
            CreateVolunteerGrid();
            
        }

        public void CreateVolunteerGrid()
        {
            lvDataBinding.ItemsSource = Volunteers.Instance.ToList;
        }
    }
}
