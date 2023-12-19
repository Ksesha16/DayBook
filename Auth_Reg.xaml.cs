﻿using EvrydayBook.Additional_methods;
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
using System.Windows.Shapes;

namespace EvrydayBook
{
    /// <summary>
    /// Логика взаимодействия для Auth_Reg.xaml
    /// </summary>
    public partial class Auth_Reg : Window
    {
        private bool isEyeOpen = true;
        string confirmationCode = GenerateConfirmationCode.GenerateCode();

        public Auth_Reg()
        {
            InitializeComponent();
        }

        //Для переключения видимости пароля
        private void EyeButton_Click(object sender, RoutedEventArgs e)
        {
            isEyeOpen = !isEyeOpen;

            string imagePath = isEyeOpen ? "/Resourse/Pictures/eye2.png" : "/Resourse/Pictures/eye1.png";
            EyeImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            EyeImage1.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            EyeImage2.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            if (isEyeOpen)
            {
                VisiblePasswordTextBox.Text = PasswordBox.Password;
                VisiblePasswordTextBox.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Collapsed;

                VisibleFirstPasswordTextBox.Text = FirstPasswordBox.Password;
                VisibleFirstPasswordTextBox.Visibility = Visibility.Visible;
                FirstPasswordBox.Visibility = Visibility.Collapsed;

                VisibleSecondPasswordTextBox.Text = SecondPasswordBox.Password;
                VisibleSecondPasswordTextBox.Visibility = Visibility.Visible;
                SecondPasswordBox.Visibility = Visibility.Collapsed;

            }
            else
            {
                PasswordBox.Password = VisiblePasswordTextBox.Text;
                PasswordBox.Visibility = Visibility.Visible;
                VisiblePasswordTextBox.Visibility = Visibility.Collapsed;

                FirstPasswordBox.Password = VisibleFirstPasswordTextBox.Text;
                FirstPasswordBox.Visibility = Visibility.Visible;
                VisibleFirstPasswordTextBox.Visibility = Visibility.Collapsed;

                SecondPasswordBox.Password = VisibleSecondPasswordTextBox.Text;
                SecondPasswordBox.Visibility = Visibility.Visible;
                VisibleSecondPasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }

        //Изменение цвета у заголовков (Автор/Регистр) при клике
        private void AuthRegTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var authHeader = AuthWindow.Header as StackPanel;
                var regHeader = RegWindow.Header as StackPanel;
                SetLabelColor(authHeader, Colors.Gray);
                SetLabelColor(regHeader, Colors.Gray);

                if (AuthRegTabControl.SelectedItem == AuthWindow)
                {
                    SetLabelColor(authHeader, Colors.Black);
                }
                else if (AuthRegTabControl.SelectedItem == RegWindow)
                {
                    SetLabelColor(regHeader, Colors.Black);
                }
            }
        }

        private void SetLabelColor(StackPanel panel, Color color)
        {
            if (panel != null && panel.Children.Count > 0)
            {
                var label = panel.Children[0] as Label;
                if (label != null)
                {
                    label.Foreground = new SolidColorBrush(color);
                }
            }
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();

        }
        //Работа с Email
        private async void CodeButton_Click(object sender, RoutedEventArgs e)
        {

            string email = EmailTextBox.Text;
            EmailService emailService = new EmailService();
            string errorMessage = await emailService.SendConfirmationEmail(email, confirmationCode);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                ErrorMessage.Content = errorMessage;
                ErrorMessage.Visibility = Visibility.Visible;
                CodeTextBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Код подтверждения отправлен на вашу почту.");
                CodeTextBox.Visibility = Visibility.Visible;
                ErrorMessage.Visibility = Visibility.Collapsed;

            }
        }
        private void CodeTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (confirmationCode == Convert.ToString(CodeTextBox.Text))
                {
                    ErrorMessage.Content = "Код верен";
                    ErrorMessage.Foreground = new SolidColorBrush(Colors.Green);
                    ErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    ErrorMessage.Content = "Введен неверный код подтверждения.";
                    ErrorMessage.Foreground = new SolidColorBrush(Colors.Red);
                    ErrorMessage.Visibility = Visibility.Visible;
                }
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CodeTextBox.Text != confirmationCode)
                {
                    MessageBox.Show("Неверный код подтверждения.", "Ошибка");
                    ErrorMessage.Content = "Введен неверный код подтверждения.";
                    ErrorMessage.Foreground = new SolidColorBrush(Colors.Red);
                    ErrorMessage.Visibility = Visibility.Visible;
                    if (confirmationCode == Convert.ToString(CodeTextBox.Text))
                    {
                        ErrorMessage.Content = "Код верен";
                        ErrorMessage.Foreground = new SolidColorBrush(Colors.Green);
                        ErrorMessage.Visibility = Visibility.Visible;
                    }
                    return;
                }
                if (FirstPasswordBox.Password != SecondPasswordBox.Password)
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка");
                    return;
                }
                if (VisibleFirstPasswordTextBox.Text.Length < 8 || VisibleFirstPasswordTextBox.Text == "")
                {
                    MessageBox.Show($"{FirstPasswordBox.Password} b {VisibleFirstPasswordTextBox.Text} Пароль должен содержать минимум 8 символов.", "Ошибка");
                    return;
                }
                if (!HasValidPasswordCharacters(FirstPasswordBox.Password))
                {
                    MessageBox.Show("Пароль должен содержать строчные и заглавные буквы, а также специальные символы (!, @, #, $, %).", "Ошибка");
                    return;
                }

                // Регистрация
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка");
            }
        }

        private bool HasValidPasswordCharacters(string password)
        {
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasSpecialChar = password.IndexOfAny(new char[] { '!', '.', '#', '$', '%' }) != -1;

            return hasUpperCase && hasLowerCase && hasSpecialChar;
        }

    }
}
