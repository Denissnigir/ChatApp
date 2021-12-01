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
using System.Windows.Shapes;
using ChatApp.Models;

namespace ChatApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public static readonly HttpClient httpClient = new HttpClient();
        public static User user;
        
        public SignInWindow()
        {
            InitializeComponent();
            if(!string.IsNullOrEmpty(Properties.Settings.Default.Login) && !string.IsNullOrEmpty(Properties.Settings.Default.Password))
            {
                Enter();
            }
        }

        private async void Enter()
        {
            UsernameTB.Text = Properties.Settings.Default.Login;
            PasswordTB.Text = Properties.Settings.Default.Password;
            //httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //var content = new userData { password = Properties.Settings.Default.Password, username = Properties.Settings.Default.Login };
            //HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            //HttpResponseMessage message = await httpClient.PostAsync("http://localhost:61876/api/Login", httpContent);
            //if (message.IsSuccessStatusCode)
            //{
            //    var curContent = await message.Content.ReadAsStringAsync();
            //    user = JsonConvert.DeserializeObject<User>(curContent);
            //    ChatSelectionWindow chatSelectionWindow = new ChatSelectionWindow();
            //    chatSelectionWindow.Show();
            //    this.Close();
            //}
            //else
            //{
            //    MessageBox.Show("Пользователь не найден!");
            //}
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void SignIn(object sender, RoutedEventArgs e)
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var content = new userData { password = PasswordTB.Text, username = UsernameTB.Text };
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await httpClient.PostAsync("http://localhost:61876/api/Login", httpContent);
            if (message.IsSuccessStatusCode)
            {
                var curContent = await message.Content.ReadAsStringAsync();
                user = JsonConvert.DeserializeObject<User>(curContent);
                if ((bool)RememberMeCB.IsChecked)
                {
                    Properties.Settings.Default.Login = UsernameTB.Text;
                    Properties.Settings.Default.Password = PasswordTB.Text;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.Login = string.Empty;
                    Properties.Settings.Default.Password = string.Empty;
                    Properties.Settings.Default.Save();
                }
                ChatSelectionWindow chatSelectionWindow = new ChatSelectionWindow();
                chatSelectionWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь не найден!");
            }
        }
    }

    public class userData
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
