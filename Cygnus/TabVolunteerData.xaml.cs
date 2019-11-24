using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;

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

        private void AddVolunteer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WindowAddVolunteer windowAddVolunteer = new WindowAddVolunteer();
            windowAddVolunteer.Show();
            windowAddVolunteer.Closed += (s, eventarg) =>
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
                view.Refresh();
            };
        }
        private void EditVolunteer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedItems.Count > 0)
            {
                Volunteer selectedVolunteer = (Volunteer)lvDataBinding.SelectedItems[0];
                WindowAddVolunteer windowAddVolunteer = new WindowAddVolunteer(selectedVolunteer);
                windowAddVolunteer.Show();
                windowAddVolunteer.Closed += (s, eventarg) =>
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(lvDataBinding.ItemsSource);
                    view.Refresh();
                };
            }
        }
    }
}
