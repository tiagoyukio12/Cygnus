using Cygnus.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Cygnus.Views
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
            string freqType = freqCmb.Text;
            string freqPeriod = "";
            switch (freqCmb.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    freqPeriod += (bool)sunChb.IsChecked ? 'T' : 'F';
                    freqPeriod += (bool)monChb.IsChecked ? 'T' : 'F';
                    freqPeriod += (bool)tueChb.IsChecked ? 'T' : 'F';
                    freqPeriod += (bool)wedChb.IsChecked ? 'T' : 'F';
                    freqPeriod += (bool)thuChb.IsChecked ? 'T' : 'F';
                    freqPeriod += (bool)friChb.IsChecked ? 'T' : 'F';
                    freqPeriod += (bool)satChb.IsChecked ? 'T' : 'F';
                    break;
                case 2:
                    if (date != null)
                    {
                        switch (monthDayCmb.SelectedIndex)
                        {
                            case 0:
                                freqPeriod = "D" + date.Day;
                                break;
                            case 1:
                                int dayOfWeekEnum = (int)date.DayOfWeek;
                                int weekOfMonth = (date.Day - 1) / 7 + 1;
                                freqPeriod = "W" + weekOfMonth + "D" + dayOfWeekEnum;
                                break;
                        }
                    }
                    break;
                case 3:
                    freqPeriod = date.ToString();
                    break;
            }

            string name = "Atividade foo";
            string description = "descrição";
            List<string> attributes = new List<string>();
            int[] time = new int[4];

            Activity activity = new Activity(id, name, description, attributes, pos, date, turn, time, new Frequency(freqType, freqPeriod));
            Volunteers.CollectionVolunteers[0].Schedule.AddActivity(activity);
        }

        private void FreqSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (periodText == null)
                return;
            switch (freqCmb.SelectedIndex)
            {
                case 0:
                    periodText.Text = "";
                    weekDayGrid.Visibility = Visibility.Hidden;
                    monthDayCmb.Visibility = Visibility.Hidden;
                    yearDayTxt.Visibility = Visibility.Hidden;
                    break;
                case 1:
                    periodText.Text = "Dia(s):";
                    weekDayGrid.Visibility = Visibility.Visible;
                    monthDayCmb.Visibility = Visibility.Hidden;
                    yearDayTxt.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    periodText.Text = "Dia:";
                    weekDayGrid.Visibility = Visibility.Hidden;
                    monthDayCmb.Visibility = Visibility.Visible;
                    yearDayTxt.Visibility = Visibility.Hidden;
                    if (dateCalendar.SelectedDate != null)
                    {
                        DateTime selectedDate = (DateTime)dateCalendar.SelectedDate;
                        int dayOfMonth = selectedDate.Day;
                        var culture = new System.Globalization.CultureInfo("pt-BR");
                        string dayOfWeek = culture.DateTimeFormat.GetDayName(selectedDate.DayOfWeek);
                        int weekOfMonth = (dayOfMonth - 1) / 7 + 1;

                        monthDayCmb.Items.Clear();
                        ComboBoxItem newItem = new ComboBoxItem
                        {
                            IsSelected = true,
                            Content = "Mensalmente no dia " + dayOfMonth
                        };
                        monthDayCmb.Items.Add(newItem);
                        newItem = new ComboBoxItem();
                        if (selectedDate.DayOfWeek == DayOfWeek.Monday || selectedDate.DayOfWeek == DayOfWeek.Saturday)
                            newItem.Content = "Todo " + weekOfMonth + "º " + dayOfWeek + " do mês";
                        else
                            newItem.Content = "Toda " + weekOfMonth + "ª " + dayOfWeek + " do mês";
                        monthDayCmb.Items.Add(newItem);
                    }
                    break;
                case 3:
                    periodText.Text = "Dia:";
                    weekDayGrid.Visibility = Visibility.Hidden;
                    monthDayCmb.Visibility = Visibility.Hidden;
                    yearDayTxt.Visibility = Visibility.Visible;
                    if (dateCalendar.SelectedDate != null)
                        yearDayTxt.Text = "Todos os anos no dia " + ((DateTime)dateCalendar.SelectedDate).ToString("dd/MM");
                    break;
            }
        }
    }
}
