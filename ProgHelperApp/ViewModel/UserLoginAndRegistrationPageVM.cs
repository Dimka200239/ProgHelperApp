using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProgHelperApp.Common;
using ProgHelperApp.Model;
using ProgHelperApp.View;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Page = System.Windows.Controls.Page;

namespace ProgHelperApp.ViewModel
{
    public class UserLoginAndRegistrationPageVM : INotifyPropertyChanged
    {
        private string _login;
        private string _password;
        private readonly Frame MainFrame;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoginCommand { get; }

        public UserLoginAndRegistrationPageVM(Frame MainFrame)
        {
            this.MainFrame = MainFrame;
            LoginCommand = new RelayCommand(Authorize);
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
            }
        }

        private async void Authorize(object parameter)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/employee/{Login}/{Password}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Employee>(responseContent);

                    //MainFrame.Navigate(new DirectorProfileView());
                    if (result.Position_F == "Директор" || result.Position_F == "Секретарь")
                    {
                        MainFrame.Content = new DirectorProfileView(MainFrame, result);
                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }
    }
}
