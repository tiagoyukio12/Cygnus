using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for WindowEditActivity.xaml
    /// </summary>
    public partial class WindowEditActivity : Window
    {
        public Volunteer Owner;
        public Activity Activity;

        public WindowEditActivity(Volunteer owner, Activity activity)
        {
            InitializeComponent();
            Owner = owner;
            Activity = activity;
            idText.Text = Activity.Id;
            locationText.Text = Activity.Location;
            birthCalendar.SelectedDate = Activity.StartDate;
            birthCalendar.DisplayDate = Activity.StartDate;
            if (Activity.Turn == 1)
                turnOneRadio.IsChecked = true;
            else if (Activity.Turn == 2)
                turnTwoRadio.IsChecked = true;
            else if (Activity.Turn == 3)
                turnThreeRadio.IsChecked = true;
            frequencyText.Text = Activity.Frequency;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Activity.Id = idText.Text;
            Activity.Location = locationText.Text;
            Activity.StartDate = (DateTime) birthCalendar.SelectedDate;
            if ((bool) turnOneRadio.IsChecked)
                Activity.Turn = 1;
            else if ((bool)turnTwoRadio.IsChecked)
                Activity.Turn = 2;
            else if ((bool)turnThreeRadio.IsChecked)
                Activity.Turn = 3;
            Activity.Frequency = frequencyText.Text;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Owner.Activities.Remove(Activity);
            Close();
        }
    }
}
