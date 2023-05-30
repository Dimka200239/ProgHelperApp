using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProgHelperApp.ViewModel
{
    public class ForwardingOrdersVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Employee employee;
        private string posForwardEmployee;
        private string idForwardEmployee;

        public ICommand UpdateCommand { get; }
        public ICommand FindEmployeeByNameCommand { get; }
        public ICommand ForwardTaskCommand { get; }

        public ObservableCollection<Button> TextBlocksTask { get; set; }
        public ObservableCollection<Button> TextBlocksFindEmployee { get; set; }

        public ForwardingOrdersVM(Employee employee)
        {
            this.employee = employee;

            UpdateCommand = new RelayCommand(Update);
            FindEmployeeByNameCommand = new RelayCommand(FindEmployeeByName);
            ForwardTaskCommand = new RelayCommand(ForwardTask);

            AddNewPosition = "Исполнитель 1-ой категории";

            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksFindEmployee = new ObservableCollection<Button>();
            TextBlocksTask.Clear();
            TextBlocksFindEmployee.Clear();
        }

        private string _findTaskId;
        private string _findProjectTaskId;
        private string _findFieldEmployee;
        private string _idEmployee;
        private string _addNewPosition;

        public string FindTaskId
        {
            get { return _findTaskId; }
            set
            {
                _findTaskId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskId)));
            }
        }

        public string FindProjectTaskId
        {
            get { return _findProjectTaskId; }
            set
            {
                _findProjectTaskId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectTaskId)));
            }
        }

        public string FindFieldEmployee
        {
            get { return _findFieldEmployee; }
            set
            {
                _findFieldEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindFieldEmployee)));
            }
        }

        public string IdEmployee
        {
            get { return _idEmployee; }
            set
            {
                _idEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IdEmployee)));
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

        private async void Update()
        {
            TextBlocksTask.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/employeeTask/GetAllTasksById/{employee.id_Employee_F}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<EmployeeTaskCardProjectMap>>(responseContent);

                    if (result != null)
                    {
                        foreach (EmployeeTaskCardProjectMap el in result)
                        {
                            var secondResponse = await client.GetAsync($"/api/employeeTask/GetTaskById/{el.id_Task_F}/{"true"}");

                            string secondResponseContent = await secondResponse.Content.ReadAsStringAsync();
                            var secondResult = JsonConvert.DeserializeObject<Model.Task>(secondResponseContent);

                            if (secondResult.Status_F == "Открыта")
                            {
                                var newButton = new Button
                                {
                                    Content = secondResult.Title_F + ";" +
                                              el.id_CardProject_F + ";" +
                                              el.id_Task_F + ";" +
                                              el.id_Employee_F + ";" +
                                              el.id_Forwarded_Employee_F + ";"
                                };

                                newButton.Click += Click_Button_Task;

                                TextBlocksTask.Add(newButton);
                            }
                        }
                    }

                    FindTaskId = null;
                    FindProjectTaskId = null;
                    FindFieldEmployee = null;
                    IdEmployee = null;
                    posForwardEmployee = null;
                    idForwardEmployee = null;
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private void Click_Button_Task(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoTask = btn.Content.ToString().Split(';');

            FindTaskId = infoTask[2];
            FindProjectTaskId = infoTask[1];
            idForwardEmployee = infoTask[4];
        }

        private async void FindEmployeeByName()
        {
            TextBlocksFindEmployee.Clear();
            posForwardEmployee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/addEmployeeInTask/GetEmployeeByFIOAndPos/{FindFieldEmployee}/{AddNewPosition}/{"true"}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Employee>>(responseContent);

                    if (result != null)
                    {
                        foreach (Employee employee in result)
                        {
                            var newButton = new Button
                            {
                                Content = employee.id_Employee_F + ";" +
                                          employee.SerName_F + ";" +
                                          employee.Name_F + ";" +
                                          employee.Patronymic_F + ";" +
                                          employee.Position_F + ";"
                            };

                            newButton.Click += Click_Button_AddEmployee;

                            TextBlocksFindEmployee.Add(newButton);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private void Click_Button_AddEmployee(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');

            IdEmployee = infoProject[0];
            posForwardEmployee = infoProject[4];
        }

        private async void ForwardTask()
        {
            try
            {
                var check = true;

                if (employee.Position_F == "Исполнитель 1-ой категории")
                {
                    check = true;
                }
                else if (employee.Position_F == "Исполнитель 2-ой категории")
                {
                    if (posForwardEmployee == "Исполнитель 1-ой категории")
                    {
                        MessageBox.Show("Вы не можете переадресовать задачу исполнителю 1-ой категории, т.к вы исполнитель 2-ой категории");
                        check = false;
                    }
                    else
                    {
                        check = true;
                    }
                }
                else if (employee.Position_F == "Исполнитель 3-ой категории")
                {
                    if (posForwardEmployee == "Исполнитель 3-ой категории")
                    {
                        check = true;
                    }
                    else
                    {
                        MessageBox.Show("Вы не можете переадресовать задачу исполнителю 1-ой и 2-ой категории, т.к вы исполнитель 3-ой категории");
                        check = false;
                    }
                }

                if (check == true)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44392");

                        var newTask = new EmployeeTaskCardProjectMap();
                        newTask.id_CardProject_F = new Guid(FindProjectTaskId);
                        newTask.id_Task_F = new Guid(FindTaskId);
                        newTask.id_Employee_F = employee.id_Employee_F;
                        newTask.id_Forwarded_Employee_F = new Guid(idForwardEmployee);

                        MessageBox.Show($"{newTask.id_CardProject_F}/{newTask.id_Task_F}/{newTask.id_Employee_F}/{newTask.id_Forwarded_Employee_F}    /    {IdEmployee}");
                        var response = await client.PutAsJsonAsync($"/api/forwardEmployee/updateTask/{IdEmployee}", newTask);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Задача успешно переадресована");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных");
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
