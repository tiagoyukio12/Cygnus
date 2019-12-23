using System;
using System.Windows;

namespace Cygnus.Views
{
    /// <summary>
    /// Interaction logic for WindowEditActivity.xaml
    /// </summary>
    public partial class WindowEditActivity : Window
    {
        public Volunteer ActivityOwner;
        public Activity Activity;

        public WindowEditActivity(Volunteer owner, Activity activity)
        {
            InitializeComponent();
            ActivityOwner = owner;
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
            ActivityOwner.Activities.Remove(Activity);
            Close();
        }
    }
}
