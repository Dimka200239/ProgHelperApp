using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProgHelperApp.Model;
using ProgHelperApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProgHelperApp.ViewModel
{
    public class AddNewEmployeeVM : INotifyPropertyChanged
    {
        private string _id;
        private string _addNewName;
        private string _addNewSerName;
        private string _addNewPatronymic;
        private string _addNewLogin;
        private string _addNewPassword;
        private string _addNewPhone;
        private string _addNewEmail;
        private string _addNewPosition;

        public ICommand AddNewCommand { get; }

        public AddNewEmployeeVM()
        {
            AddNewCommand = new RelayCommand(AddNewEmployee);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string AddNewName
        {
            get { return _addNewName; }
            set
            {
                _addNewName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewName)));
            }
        }

        public string AddNewSerName
        {
            get { return _addNewSerName; }
            set
            {
                _addNewSerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewSerName)));
            }
        }

        public string AddNewPatronymic
        {
            get { return _addNewPatronymic; }
            set
            {
                _addNewPatronymic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewPatronymic)));
            }
        }

        public string AddNewLogin
        {
            get { return _addNewLogin; }
            set
            {
                _addNewLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewLogin)));
            }
        }

        public string AddNewPassword
        {
            get { return _addNewPassword; }
            set
            {
                _addNewPassword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewPassword)));
            }
        }

        public string AddNewPhone
        {
            get { return _addNewPhone; }
            set
            {
                _addNewPhone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewPhone)));
            }
        }

        public string AddNewEmail
        {
            get { return _addNewEmail; }
            set
            {
                _addNewEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewEmail)));
            }
        }

        public string AddNewPosition
        {
            get { return _addNewPosition; }
            set
            {
                _addNewPosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewPosition)));
            }
        }

        private async void AddNewEmployee()
        {
            try
            {
                var newEmployee = new Employee();
                newEmployee.id_Employee_F = Guid.NewGuid();
                newEmployee.Name_F = AddNewName;
                newEmployee.SerName_F = AddNewSerName;
                newEmployee.Patronymic_F = AddNewPatronymic;
                newEmployee.Login_F = AddNewLogin;
                newEmployee.Password_F = AddNewPassword;
                newEmployee.Phone_F = AddNewPhone;
                newEmployee.Email_F = AddNewEmail;
                newEmployee.Position_F = AddNewPosition;

                new Common.DataValidationContext().Validate(newEmployee);

                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://localhost:44392");

                    var response = await client.PostAsJsonAsync($"/api/employee/addNewEmployee", newEmployee);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Успешное добавление");
                        AddNewName = "";
                        AddNewSerName = "";
                        AddNewPatronymic = "";
                        AddNewLogin = "";
                        AddNewPassword = "";
                        AddNewPhone = "";
                        AddNewEmail = "";
                        AddNewPosition = "";
                    }
                    else
                    {
                        MessageBox.Show("При добавлении произошла ошибка...");
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
