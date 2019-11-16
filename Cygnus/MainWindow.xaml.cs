﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Cygnus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Calendar tab
            Activity activity = new Activity("0001", "Rua 1", new DateTime(2019, 11, 4), 2, "Freq");
            Activities.Instance.Add(activity);
            List<Activity> activities = Activities.Instance.ToList;
        }

        void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabCalendarData.CreateCalendar();
            }
        }
    }
}
