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
    /// Логика взаимодействия для EmployeeFindWindow.xaml
    /// </summary>
    public partial class EmployeeFindWindow : Window
    {
        List<User> users = new List<User>();
        public EmployeeFindWindow()
        {
            InitializeComponent();
            GetDepartment();
        }

        public async void GetDepartment()
        {
            HttpResponseMessage httpResponseMessage = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/Departments");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            DepartmentsList.ItemsSource = JsonConvert.DeserializeObject<List<Departments>>(content);
            GetEmployee();
        }

        public async void GetEmployee()
        {
            var roles = (DepartmentsList.ItemsSource as List<Departments>).Where(p => p.isChecked)
                                                                          .Select(p => p.id);
            HttpResponseMessage httpResponseMessage = await SignInWindow.httpClient.GetAsync("http://localhost:61876/api/Employees");
            var content = await httpResponseMessage.Content.ReadAsStringAsync();
            users = JsonConvert.DeserializeObject<List<User>>(content).Where(p => roles.Contains(p.departmentId))
                                                                      .ToList();
            users = users.Where(p => p.getName.Contains(SearchTB.Text))
                         .ToList();
            EmployeeList.ItemsSource = users;
        }

        private async void Filter(object sender, RoutedEventArgs e)
        {
            GetEmployee();
        }


        private async void AddUserToChat(object sender, MouseButtonEventArgs e)
        {
            if(ChatSelectionWindow.chatRoom == null)
            {
                var chatroomContent = new ChatRoom { topic = "Жопа", lastMessage = "Жопа" };
                HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(chatroomContent), Encoding.UTF8, "application/json");
                HttpResponseMessage message = await SignInWindow.httpClient.PostAsync("http://localhost:61876/api/Chatrooms", httpContent);
                var responseContent = await message.Content.ReadAsStringAsync();
                var chatroomInfo = JsonConvert.DeserializeObject<ChatRoom>(responseContent);
                var userContent = new ChatroomEmployee { chatroomId = chatroomInfo.id, employeeId = SignInWindow.user.id };
                var secondUserContent = new ChatroomEmployee { chatroomId = chatroomInfo.id, employeeId = (EmployeeList.SelectedItem as User).id };
                HttpContent userHttpContent = new StringContent(JsonConvert.SerializeObject(userContent), Encoding.UTF8, "application/json");
                HttpContent secondUserHttpContent = new StringContent(JsonConvert.SerializeObject(secondUserContent), Encoding.UTF8, "application/json");
                HttpResponseMessage userResponseMessage = await SignInWindow.httpClient.PostAsync("http://localhost:61876/api/ChatroomEmployees", userHttpContent);
                HttpResponseMessage secondUserResponseMessage = await SignInWindow.httpClient.PostAsync("http://localhost:61876/api/ChatroomEmployees", secondUserHttpContent);
                ChatSelectionWindow.chatRoom = chatroomInfo;
                var messageContent = new ChatMessage { chatroomId = chatroomInfo.id, date = DateTime.Now, message = "Чат создан!", senderId = 101 };
                HttpContent messageHttpContent = new StringContent(JsonConvert.SerializeObject(messageContent), Encoding.UTF8, "application/json");
                HttpResponseMessage messageResponseMessage = await SignInWindow.httpClient.PostAsync("http://localhost:61876/api/ChatMessages", messageHttpContent);
                ChatWindow chatWindow = new ChatWindow();
                chatWindow.Show();
                this.Close();
            }
            else
            {
                var secondUserContent = new ChatroomEmployee { chatroomId = ChatSelectionWindow.chatRoom.id, employeeId = (EmployeeList.SelectedItem as User).id };
                HttpContent secondUserHttpContent = new StringContent(JsonConvert.SerializeObject(secondUserContent), Encoding.UTF8, "application/json");
                HttpResponseMessage secondUserResponseMessage = await SignInWindow.httpClient.PostAsync("http://localhost:61876/api/ChatroomEmployees", secondUserHttpContent);
                ChatWindow chatWindow = new ChatWindow();
                chatWindow.Show();
                this.Close();
            }
        }
    }
}
