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

namespace ChatApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeTopicWindow.xaml
    /// </summary>
    public partial class ChangeTopicWindow : Window
    {
        public ChangeTopicWindow()
        {
            InitializeComponent();
        }

        private async void ChangeTopic(object sender, RoutedEventArgs e)
        {
            ChatSelectionWindow.chatRoom.topic = TopicTB.Text;
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(ChatSelectionWindow.chatRoom), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await SignInWindow.httpClient.PutAsync($"http://localhost:61876/api/Chatrooms/{ChatSelectionWindow.chatRoom.id}", httpContent);
            this.Title = ChatSelectionWindow.chatRoom.topic;
            ChatWindow chatWindow = new ChatWindow();
            chatWindow.Show();
            this.Close();
        }
    }
}
