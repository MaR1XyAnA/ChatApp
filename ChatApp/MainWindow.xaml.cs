﻿using ChatApp.ClassFolder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace ChatApp
{
    public partial class MainWindow : Window
    {
        public HttpClient httpClient = new HttpClient(); // Подключили программу к сити интернет
        public EmplyeeClass emplyeeClass; // Подключили класс
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SignIn(object sender, RoutedEventArgs e) // Ассинхронный метод
        {
            httpClient.DefaultRequestHeaders.Accept.Add
                (new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")); // WPF дружит с JSON
            var Content = new UserData { username = LoginTextBox.Text, password = PasswordPasswordBox.Password };
            HttpContent httpContent = new StringContent
                (JsonConvert.SerializeObject(Content), Encoding.UTF8, "application/json"); // подключаем HttpContent
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:50203/api/Login", httpContent);
            if (message.IsSuccessStatusCode)
            {
                MessageBox.Show("Я куми кудасай");
            }
            else
            {
                MessageBox.Show("Ты умный? \nLOGIN \nили \nPASSWORD неверный");
            }
        }

        public class UserData // создали класс, который будет получать данные из LoginTextBox и  PasswordPasswordBox
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
