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
using Newtonsoft.Json;

namespace ChatApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChatSelectionWindow.xaml
    /// </summary>
    public partial class ChatSelectionWindow : Window
    {
        public List<ChatRoom> chatRooms = new List<ChatRoom>();
        public List<ChatroomEmployee> chatroomEmployees = new List<ChatroomEmployee>();
        public static ChatRoom chatRoom;
        public ChatSelectionWindow()
        {
            InitializeComponent();
            HelloGrid.DataContext = SignInWindow.user;
            GetRooms();
        }

        public async void GetRooms()
        {
            HttpResponseMessage httpResponseMessage = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/Chatrooms");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            HttpResponseMessage httpResponseMessageEmployees = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/ChatroomEmployees");
            var employee = await httpResponseMessageEmployees.Content.ReadAsStringAsync();
            var x = JsonConvert.DeserializeObject<List<ChatroomEmployee>>(employee).Where(p => p.employeeId == SignInWindow.user.id).FirstOrDefault();
        
            if(x == null)
            {
            }
            else
            {
                ChatRoomsList.ItemsSource = JsonConvert.DeserializeObject<List<ChatRoom>>(content).Where(p => p.id == x.chatroomId);
            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void EmployeeFind(object sender, RoutedEventArgs e)
        {
            EmployeeFindWindow employeeFindWindow = new EmployeeFindWindow();
            employeeFindWindow.Show();
            this.Close();
        }

        private void ChatRoomsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            chatRoom = ChatRoomsList.SelectedItem as ChatRoom;
            ChatWindow chatWindow = new ChatWindow();
            chatWindow.Show();
            this.Close();
        }
    }
}
