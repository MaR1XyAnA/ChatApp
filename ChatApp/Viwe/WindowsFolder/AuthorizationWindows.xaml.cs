using ChatApp.ClassFolder;
using ChatApp.Viwe.WindowsFolder;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace ChatApp.Viwe.WindowsFolder
{
    public partial class AuthorizationWindows : Window
    {
        public static HttpClient httpClient = new HttpClient(); // Подключили программу к сити интернет
        public static EmplyeeClass emplyeeClass; // Подключили класс
        public AuthorizationWindows()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(LoginTextBox.Text = Properties.Settings.Default.LoginUser) && // Проверяет, если что-то в этих значениях
                !string.IsNullOrEmpty(PasswordPasswordBox.Password = Properties.Settings.Default.PasswordUser)) // Если да, то он их выводит в эл. управления
            {
                Enter();
            }
        }

        private async void Enter()
        {
            LoginTextBox.Text = Properties.Settings.Default.LoginUser; // Вытягиваем в LoginTextBox логин пользователя
            PasswordPasswordBox.Password = Properties.Settings.Default.PasswordUser; // Вытягиваем в PasswordPasswordBox пароль пользователя
        }

        private async void SignIn(object sender, RoutedEventArgs e) // Ассинхронный метод
        {
            #region АНИМАЦИЯ
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = 50;
            doubleAnimation.To = 0;
            doubleAnimation.EasingFunction = new QuadraticEase();
            #endregion

            string PasswordVisibleString;
            PasswordVisibleString = Convert.ToString(PasswordTextBox.Text);
            PasswordPasswordBox.Password = PasswordVisibleString;
            VisibilityPasswordFalseStackPanel.Visibility = Visibility.Visible;
            VisibilityPasswordTrueStackPanel.Visibility = Visibility.Collapsed;

            string ServerAddressString, JsonString;
            ServerAddressString = "http://localhost:11111/api/authorization_user";
            JsonString = "application/json";

            httpClient.DefaultRequestHeaders.Accept.Add
                (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(JsonString)); // WPF дружит с JSON
            var Content = new UserData { LoginUser = LoginTextBox.Text, PasswordUser = PasswordPasswordBox.Password };
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(Content), Encoding.UTF8, JsonString); // подключаем HttpContent
            HttpResponseMessage message = await httpClient.PostAsync(ServerAddressString, httpContent);
            if (message.IsSuccessStatusCode)
            {
                var curContebt = await message.Content.ReadAsStringAsync();
                emplyeeClass = JsonConvert.DeserializeObject<EmplyeeClass>(curContebt);

                if ((bool)RememberMeCheckBox.IsChecked) // Проверяем, нажат ли CheckBox
                {
                    Properties.Settings.Default.LoginUser = LoginTextBox.Text; // Сохраняем логин в приложении
                    Properties.Settings.Default.PasswordUser = PasswordPasswordBox.Password; // Сохраняем пароль в приложении
                    Properties.Settings.Default.Save(); // Сохраняем данные в приложении
                }
                MainWi mainWi = new MainWi();
                mainWi.Show();
                Close();
            }
            else
            {
                InfoeErrorBorder.Visibility = Visibility.Visible;
                InfoeErrorTextBlock.Visibility = Visibility.Visible;
                InfoeErrorBorder.BeginAnimation(HeightProperty, doubleAnimation);
                InfoeErrorTextBlock.BeginAnimation(HeightProperty, doubleAnimation);
                InfoeErrorTextBlock.Text = "ЛОГИН ИЛИ ПАРОЛЬ ВВЕДЕНЫ НЕВЕРНО";
            }
        }
        public class UserData // создали класс, который будет получать данные из LoginTextBox и  PasswordPasswordBox
        {
            public string LoginUser { get; set; } // Тип данных, как в БД
            public string PasswordUser { get; set; } // Тип данных, как в БД
        }

        // Обработчик события работы над окном
        #region WindowsEddit
        private void SpaseBarGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RollUpButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        #endregion
        // Опработчик события показать или скрыть пароль
        #region VisibilityPassword
        private void VisibilityPasswordFalseButton_Click(object sender, RoutedEventArgs e)
        {
            string PasswordVisibleString;
            PasswordVisibleString = Convert.ToString(PasswordPasswordBox.Password);
            PasswordTextBox.Text = PasswordVisibleString;
            VisibilityPasswordFalseStackPanel.Visibility = Visibility.Collapsed;
            VisibilityPasswordTrueStackPanel.Visibility = Visibility.Visible;
        }

        private void VisibilityPasswordTrueButton_Click(object sender, RoutedEventArgs e)
        {
            string PasswordVisibleString;
            PasswordVisibleString = Convert.ToString(PasswordTextBox.Text);
            PasswordPasswordBox.Password = PasswordVisibleString;
            VisibilityPasswordFalseStackPanel.Visibility = Visibility.Visible;
            VisibilityPasswordTrueStackPanel.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
