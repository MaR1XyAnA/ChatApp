using ChatApp.ClassFolder;
using ChatApp.Viwe.WindowsFolder;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace ChatApp
{
    public partial class MainWindow : Window
    {
        public HttpClient httpClient = new HttpClient(); // Подключили программу к сити интернет
        public EmplyeeClass emplyeeClass; // Подключили класс
        public MainWindow()
        {
            InitializeComponent();
            if(!string.IsNullOrEmpty(LoginTextBox.Text = Properties.Settings.Default.LoginUser) && // Проверяет, если что-то в этих значениях
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
            string ServerAddressString, JsonString;
            ServerAddressString = "http://localhost:50203/api/Login";
            JsonString = "application/json";

            httpClient.DefaultRequestHeaders.Accept.Add
                (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(JsonString)); // WPF дружит с JSON
            var Content = new UserData { username = LoginTextBox.Text, password = PasswordPasswordBox.Password };
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(Content), Encoding.UTF8, JsonString); // подключаем HttpContent
            HttpResponseMessage message = await httpClient.PostAsync(ServerAddressString, httpContent);
            if (message.IsSuccessStatusCode)
            {
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
                Properties.Settings.Default.LoginUser = String.Empty; // Сохраняем логин в приложении
                Properties.Settings.Default.PasswordUser = String.Empty; // Сохраняем пароль в приложении
                Properties.Settings.Default.Save(); // Сохраняем данные в приложении
            }
        }

        public class UserData // создали класс, который будет получать данные из LoginTextBox и  PasswordPasswordBox
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
