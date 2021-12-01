using ChatApp.Models;
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
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        List<ChatMessage> chatMessages = new List<ChatMessage>();
        List<ChatroomEmployee> users = new List<ChatroomEmployee>();
        public ChatWindow()
        {
            InitializeComponent();
            this.Title = ChatSelectionWindow.chatRoom.topic;
            GetMessage();
            GetUsers();
        }

        private async void SendMessage(object sender, RoutedEventArgs e)
        {            
            var content = new ChatMessage { chatroomId = ChatSelectionWindow.chatRoom.id, date = DateTime.Now, message = MessageTB.Text, senderId = SignInWindow.user.id };
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            HttpResponseMessage message = await SignInWindow.httpClient.PostAsync("http://localhost:61876/api/ChatMessages", httpContent);
            MessageTB.Text = string.Empty;
            GetMessage();
        }

        private async void GetMessage()
        {
            HttpResponseMessage message = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/ChatMessages");
            var content = await message.Content.ReadAsStringAsync();
            chatMessages = JsonConvert.DeserializeObject<List<ChatMessage>>(content);
            MessageList.ItemsSource = chatMessages.Where(p => p.chatroomId == ChatSelectionWindow.chatRoom.id);
            MessageList.ScrollIntoView(chatMessages.Where(p => p.chatroomId == ChatSelectionWindow.chatRoom.id).LastOrDefault());
        }

        private async void GetUsers()
        {
            HttpResponseMessage message = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/ChatroomEmployees");
            var content = await message.Content.ReadAsStringAsync();
            users = JsonConvert.DeserializeObject<List<ChatroomEmployee>>(content);
            UserList.ItemsSource = users.Where(p => p.chatroomId == ChatSelectionWindow.chatRoom.id);
        }

        private void AddUserToChat(object sender, RoutedEventArgs e)
        {
            EmployeeFindWindow employeeFindWindow = new EmployeeFindWindow();
            employeeFindWindow.Show();
            this.Close();
        }

        private async void ChangeTopic(object sender, RoutedEventArgs e)
        {
            ChangeTopicWindow changeTopicWindow = new ChangeTopicWindow();
            changeTopicWindow.Show();
            this.Close();
        }

        private async void LeaveChatroom(object sender, RoutedEventArgs e)
        {
            HttpResponseMessage message = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/ChatroomEmployees");
            var content = await message.Content.ReadAsStringAsync();
            users = JsonConvert.DeserializeObject<List<ChatroomEmployee>>(content);
            var x = users.Where(p => p.employeeId == SignInWindow.user.id).FirstOrDefault();
            HttpResponseMessage responseMessage = await SignInWindow.httpClient.DeleteAsync($"http://localhost:61876/api/ChatroomEmployees/{x.id}");

            ChatSelectionWindow chatSelectionWindow = new ChatSelectionWindow();
            chatSelectionWindow.Show();
            this.Close();
        }
    }
}
