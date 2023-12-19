using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvrydayBook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentYear = 2023;
        private int currentMonth = 11;

        public MainWindow()
        {
            InitializeComponent();
            var today = DateTime.Today;
            currentYear = today.Year;
            currentMonth = today.Month;
            UpdateCalendar(currentYear, currentMonth);

        }

        private void TaskButton_Click(object sender, RoutedEventArgs e)
        {
            Task_Window task_Window = new Task_Window();
            task_Window.Show();
            this.Hide();

        }
        private void CalendarButton_Click(object sender, RoutedEventArgs e)
        {
            Calendar_Window calendar_Window = new Calendar_Window();
            calendar_Window.Show();
            this.Hide();
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Settings_Window settings_Window = new Settings_Window();
            settings_Window.Show();
            this.Hide();
        }
        //Рбаота с Календарем
        private void UpdateCalendar(int year, int month)
        {
            ClearCalendar();
            MonthLabel.Content = new DateTime(year, month, 1).ToString("MMMM yyyy");

            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            int dayOfWeek = ((int)firstDayOfMonth.DayOfWeek + 6) % 7;
            int daysInMonth = DateTime.DaysInMonth(year, month);

            int day = 1;
            for (int row = 2; row <= 7; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if ((row == 2 && col < dayOfWeek) || day > daysInMonth)
                    {
                        continue;
                    }

                    var dayLabel = (Label)this.FindName($"A_{row}_{col}");
                    if (dayLabel != null)
                    {
                        dayLabel.Content = day.ToString();

                        // Подсветка сегодняшнего дня
                        if (year == DateTime.Today.Year && month == DateTime.Today.Month && day == DateTime.Today.Day)
                        {
                            dayLabel.Background = Brushes.Blue;
                        }
                        else
                        {
                            dayLabel.Foreground = Brushes.Black; 
                        }
                    }
                    day++;
                }
            }
        }


        private void ClearCalendar()
        {
            for (int row = 2; row <= 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    var dayLabel = (Label)this.FindName($"A_{row}_{col}");
                    if (dayLabel != null)
                    {
                        dayLabel.Content = string.Empty;
                    }
                }
            }
        }
        private void PreviousMonth_Click(object sender, RoutedEventArgs e)
        {
            if (currentYear == 2023 && currentMonth == 11)
            {
                return;
            }
            if (currentMonth == 1)
            {
                currentMonth = 12;
                currentYear--;
            }
            else
            {
                currentMonth--;
            }
            UpdateCalendar(currentYear, currentMonth);
        }

        private void NextMonth_Click(object sender, RoutedEventArgs e)
        {
            if (currentMonth == 12)
            {
                currentMonth = 1;
                currentYear++;
            }
            else
            {
                currentMonth++;
            }
            UpdateCalendar(currentYear, currentMonth);
        }



    }
}
