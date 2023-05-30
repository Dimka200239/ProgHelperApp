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

namespace ProgHelperApp.ViewModel
{
    public class AddEmployeeInTaskVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Employee employee;

        public ICommand FindProjectTaskByNameCommand { get; }
        public ICommand FindEmployeeByNameCommand { get; }

        public ObservableCollection<Button> TextBlocksProjectTask { get; set; }
        public ObservableCollection<Button> TextBlocksTask { get; set; }
        public ObservableCollection<Button> TextBlocksFindEmployee { get; set; }
        public ObservableCollection<Button> TextBlocksEmployee { get; set; }

        public AddEmployeeInTaskVM(Employee employee)
        {
            this.employee = employee;

            FindProjectTaskByNameCommand = new RelayCommand(FindProjectTaskByName);
            FindEmployeeByNameCommand = new RelayCommand(FindEmployeeByName);

            TextBlocksProjectTask = new ObservableCollection<Button>();
            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksFindEmployee = new ObservableCollection<Button>();
            TextBlocksEmployee = new ObservableCollection<Button>();

            TextBlocksProjectTask.Clear();
            TextBlocksTask.Clear();
            TextBlocksFindEmployee.Clear();
            TextBlocksEmployee.Clear();

            AddNewPosition = "Исполнитель 1-ой категории";
        }

        private string _findFieldProjectTask;
        private string _findProjectTaskId;

        public string FindFieldProjectTask
        {
            get { return _findFieldProjectTask; }
            set
            {
                _findFieldProjectTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindFieldProjectTask)));
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

        private string _findTaskIdTextBoxInputField;
        private string _findTaskTitleTextBoxInputField;
        private string _findTaskStatusTextBoxInputField;

        public string FindTaskIdTextBoxInputField
        {
            get { return _findTaskIdTextBoxInputField; }
            set
            {
                _findTaskIdTextBoxInputField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskIdTextBoxInputField)));
            }
        }

        public string FindTaskTitleTextBoxInputField
        {
            get { return _findTaskTitleTextBoxInputField; }
            set
            {
                _findTaskTitleTextBoxInputField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskTitleTextBoxInputField)));
            }
        }

        public string FindTaskStatusTextBoxInputField
        {
            get { return _findTaskStatusTextBoxInputField; }
            set
            {
                _findTaskStatusTextBoxInputField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskStatusTextBoxInputField)));
            }
        }

        private string _findFieldEmployee;
        private string _addNewPosition;

        public string FindFieldEmployee
        {
            get { return _findFieldEmployee; }
            set
            {
                _findFieldEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindFieldEmployee)));
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

        private async void FindProjectTaskByName()
        {
            TextBlocksProjectTask.Clear();
            TextBlocksTask.Clear();
            TextBlocksFindEmployee.Clear();
            TextBlocksEmployee.Clear();
            FindProjectTaskId = null;
            FindTaskIdTextBoxInputField = null;
            FindTaskTitleTextBoxInputField = null;
            FindTaskStatusTextBoxInputField = null;
            FindFieldEmployee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/editEmployee/FindProjectByNameAndEmployeeId/{FindFieldProjectTask}/{this.employee.id_Employee_F}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<CardProject>>(responseContent);


                    if (result != null)
                    {
                        foreach (CardProject project in result)
                        {
                            var newButton = new Button
                            {
                                Content = project.id_CardProject_F + ";" +
                                          project.Title_F + ";" +
                                          project.Description_F + ";" +
                                          project.DateOfBegining_F + ";" +
                                          project.Status_F + ";" +
                                          project.id_Employee_F
                            };

                            newButton.Click += Click_Button_Project;

                            TextBlocksProjectTask.Add(newButton);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void Click_Button_Project(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');
            FindProjectTaskId = infoProject[0];

            TextBlocksTask.Clear();
            TextBlocksFindEmployee.Clear();
            TextBlocksEmployee.Clear();
            FindTaskIdTextBoxInputField = null;
            FindTaskTitleTextBoxInputField = null;
            FindTaskStatusTextBoxInputField = null;
            FindFieldEmployee = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/editTaskEmployee/FindTaskByProjectName/{FindProjectTaskId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Model.Task>>(responseContent);

                    if (result != null)
                    {
                        foreach (Model.Task project in result)
                        {
                            var newButton = new Button
                            {
                                Content = project.id_Task_F + ";" +
                                          project.Title_F + ";" +
                                          project.Description_F + ";" +
                                          project.Deadline_F + ";" +
                                          project.Status_F
                            };

                            newButton.Click += Click_Button_Task;

                            TextBlocksTask.Add(newButton);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void Click_Button_Task(object sender, RoutedEventArgs e)
        {
            TextBlocksFindEmployee.Clear();
            TextBlocksEmployee.Clear();
            FindTaskIdTextBoxInputField = null;
            FindTaskTitleTextBoxInputField = null;
            FindTaskStatusTextBoxInputField = null;
            FindFieldEmployee = null;

            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');

            FindTaskIdTextBoxInputField = infoProject[0];
            FindTaskTitleTextBoxInputField = infoProject[1];
            FindTaskStatusTextBoxInputField = infoProject[4];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/addEmployeeInTask/GetAllEmployee/{FindProjectTaskId}/{FindTaskIdTextBoxInputField}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Model.EmployeeTaskCardProjectMap>>(responseContent);

                    if (result != null)
                    {
                        foreach (Model.EmployeeTaskCardProjectMap employeeTaskCardProject in result)
                        {
                            var secondResponse = await client.GetAsync($"/api/addEmployeeInTask/GetEmployee/{employeeTaskCardProject.id_Employee_F}");

                            if (secondResponse.IsSuccessStatusCode)
                            {
                                string secondResponseContent = await secondResponse.Content.ReadAsStringAsync();
                                var secondResult = JsonConvert.DeserializeObject<Model.Employee>(secondResponseContent);

                                var newButton = new Button
                                {
                                    Content = secondResult.id_Employee_F + ";" +
                                          secondResult.SerName_F + ";" +
                                          secondResult.Name_F + ";" +
                                          secondResult.Patronymic_F + ";"
                                };

                                newButton.MouseDoubleClick += MouseDoubleClick_Button_Employee;

                                TextBlocksEmployee.Add(newButton);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void MouseDoubleClick_Button_Employee(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');

            if (MessageBox.Show($"{infoProject[1]} {infoProject[2]} {infoProject[3]} будет откреплен от задачи", "Согласны продолжить?",
                                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes
                                            && FindProjectTaskId != null
                                            && FindTaskIdTextBoxInputField != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44392");

                    //var response = await client.DeleteAsync($"/api/addEmployeeInTask/deleteEmployeeCardProject/{FindProjectTaskId}/{infoProject[0]}");

                    var secondResponse = await client.DeleteAsync($"/api/addEmployeeInTask/deleteEmployeeTaskCardProject/{FindProjectTaskId}/{FindTaskIdTextBoxInputField}/{infoProject[0]}/{infoProject[0]}");

                    if (secondResponse.IsSuccessStatusCode) // && response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"{infoProject[1]} {infoProject[2]} {infoProject[3]} успешно откреплен от задачи!");

                        TextBlocksEmployee.Remove(btn);
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных");
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных");
            }
        }

        private async void FindEmployeeByName()
        {
            TextBlocksFindEmployee.Clear();

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
                                          employee.Patronymic_F + ";"
                            };

                            newButton.MouseDoubleClick += MouseDoubleClick_Button_AddEmployee;

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

        private async void MouseDoubleClick_Button_AddEmployee(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');

            if (MessageBox.Show($"{infoProject[1]} {infoProject[2]} {infoProject[3]} будет приклеплен к задаче.", "Согласны продолжить?",
                                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes
                                            && FindProjectTaskId != null
                                            && FindTaskIdTextBoxInputField != null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44392");

                    var employeeTaskCardProject = new EmployeeTaskCardProjectMap();
                    employeeTaskCardProject.id_CardProject_F = new Guid(FindProjectTaskId);
                    employeeTaskCardProject.id_Task_F = new Guid(FindTaskIdTextBoxInputField);
                    employeeTaskCardProject.id_Employee_F = new Guid(infoProject[0]);
                    employeeTaskCardProject.id_Forwarded_Employee_F = new Guid(infoProject[0]);

                    var response = await client.PostAsJsonAsync($"/api/addEmployeeInTask/addEmployeeTaskCardProject", employeeTaskCardProject);

                    //var employeeCardProject = new Model.CardProjectEmployeeMap();
                    //employeeCardProject.id_CardProject_F = new Guid(FindProjectTaskId);
                    //employeeCardProject.id_Employee_F = new Guid(infoProject[0]);
                    //var secondResponse = await client.PostAsJsonAsync($"/api/addEmployeeInTask/addEmployeeCardProject", employeeCardProject);

                    if (response.IsSuccessStatusCode) // && secondResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"{infoProject[1]} {infoProject[2]} {infoProject[3]} успешно приклеплен к задаче!");

                        var newButton = new Button
                        {
                            Content = infoProject[0] + ";" +
                                          infoProject[1] + ";" +
                                          infoProject[2] + ";" +
                                          infoProject[3] + ";"
                        };

                        newButton.MouseDoubleClick += MouseDoubleClick_Button_Employee;

                        TextBlocksEmployee.Add(newButton);
                    }
                }
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных");
            }
        }
    }
}
