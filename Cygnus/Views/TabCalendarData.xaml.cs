using Cygnus.ViewModels;
using System.Windows.Controls;
using System;
using System.Windows;

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

        private void FreqSelectionChanged(object sender, SelectionChangedEventArgs e)
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
