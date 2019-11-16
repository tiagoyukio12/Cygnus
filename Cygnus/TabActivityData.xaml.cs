using System;
using System.Windows;
using System.Windows.Controls;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for TabVolunteerData.xaml
    /// </summary>
    public partial class TabActivityData : UserControl
    {
        public TabActivityData()
        {
            InitializeComponent();
        }

        private void AddActivity_Click(object sender, RoutedEventArgs e)
        {
            string id = idText.Text;
            string pos = posText.Text;
            DateTime date = (DateTime)dateCalendar.SelectedDate;
            int turn = -1;
            if ((bool)turnOneRadio.IsChecked)
                turn = 1;
            else if ((bool)turnTwoRadio.IsChecked)
                turn = 2;
            else if ((bool)turnThreeRadio.IsChecked)
                turn = 3;
            var freq = freqText.Text;

            Activity activity = new Activity(id, pos, date, turn, freq);
            Activities.Instance.Add(activity);
        }
    }
}
