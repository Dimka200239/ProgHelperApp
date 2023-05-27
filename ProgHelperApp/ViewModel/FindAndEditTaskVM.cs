using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    public class FindAndEditTaskVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FindProjectTaskByNameCommand { get; }
        public ICommand SaveTaskCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public ObservableCollection<Button> TextBlocksProjectTask { get; set; }
        public ObservableCollection<Button> TextBlocksTask { get; set; }


        public FindAndEditTaskVM()
        {
            FindProjectTaskByNameCommand = new RelayCommand(FindProjectTaskByName);
            SaveTaskCommand = new RelayCommand(SaveTask);
            AddTaskCommand = new RelayCommand(AddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);

            TextBlocksProjectTask = new ObservableCollection<Button>();
            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksProjectTask.Clear();
            TextBlocksTask.Clear();
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
        private string _findTaskDescriptionTextBoxInputField;
        private string _findTaskDeadlineTextBoxInputField;
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

        public string FindTaskDescriptionTextBoxInputField
        {
            get { return _findTaskDescriptionTextBoxInputField; }
            set
            {
                _findTaskDescriptionTextBoxInputField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskDescriptionTextBoxInputField)));
            }
        }

        public string FindTaskDeadlineTextBoxInputField
        {
            get { return _findTaskDeadlineTextBoxInputField; }
            set
            {
                _findTaskDeadlineTextBoxInputField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskDeadlineTextBoxInputField)));
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

        private async void FindProjectTaskByName()
        {
            TextBlocksProjectTask.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/editEmployee/FindProjectByName/{FindFieldProjectTask}");

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

        private void Click_Button_Task(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');

            FindTaskIdTextBoxInputField = infoProject[0];
            FindTaskTitleTextBoxInputField = infoProject[1];
            FindTaskDescriptionTextBoxInputField = infoProject[2];
            FindTaskDeadlineTextBoxInputField = DateTime.Parse(infoProject[3]).Subtract(DateTime.UtcNow).Days.ToString();
            FindTaskStatusTextBoxInputField = infoProject[4];
        }

        private async void SaveTask()
        {
            if (MessageBox.Show("Данные о задаче будут обновлены!", "Согласны продолжить?",
                                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var updateTask = new Model.Task();
                updateTask.id_Task_F = new Guid(FindTaskIdTextBoxInputField);
                updateTask.Title_F = FindTaskTitleTextBoxInputField;
                updateTask.Description_F = FindTaskDescriptionTextBoxInputField;
                updateTask.Deadline_F = DateTime.UtcNow.AddDays(int.Parse(FindTaskDeadlineTextBoxInputField)).ToString();
                updateTask.Status_F = FindTaskStatusTextBoxInputField;

                try
                {
                    new Common.DataValidationContext().Validate(updateTask);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44392");

                        var response = await client.PutAsJsonAsync($"/api/editTaskEmployee/UpdateTaskInfo", updateTask);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Успешное обновление данных!");
                            FindTaskIdTextBoxInputField = "";
                            FindTaskTitleTextBoxInputField = "";
                            FindTaskDescriptionTextBoxInputField = "";
                            FindTaskDeadlineTextBoxInputField = "";
                            FindTaskStatusTextBoxInputField = "";
                            TextBlocksTask.Clear();
                        }
                        else
                        {
                            MessageBox.Show("При обновлении данных произошла ошибка...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Вы отменили обновленние данных задачи");
            }
        }

        private async void AddTask()
        {
            if (FindTaskIdTextBoxInputField == "null")
            {
                var newTask = new Model.Task();
                newTask.id_Task_F = Guid.NewGuid();
                newTask.Title_F = FindTaskTitleTextBoxInputField;
                newTask.Description_F = FindTaskDescriptionTextBoxInputField;
                newTask.Deadline_F = DateTime.UtcNow.AddDays(int.Parse(FindTaskDeadlineTextBoxInputField)).ToString();
                newTask.Status_F = FindTaskStatusTextBoxInputField;

                try
                {
                    new Common.DataValidationContext().Validate(newTask);

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44392");

                        var response = await client.PostAsJsonAsync($"/api/editTaskEmployee/AddNewTask/{FindProjectTaskId}", newTask);

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Успешное добавление данных!");
                            FindTaskIdTextBoxInputField = "";
                            FindTaskTitleTextBoxInputField = "";
                            FindTaskDescriptionTextBoxInputField = "";
                            FindTaskDeadlineTextBoxInputField = "";
                            FindTaskStatusTextBoxInputField = "";
                            TextBlocksTask.Clear();
                        }
                        else
                        {
                            MessageBox.Show("При добавлении данных произошла ошибка...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                if (MessageBox.Show("Для добавления поле ID задачи должно иметь значение null!", "Согласны продолжить?",
                                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    FindTaskIdTextBoxInputField = "null";
                }
                else
                {
                    MessageBox.Show("Вы отменили добавление новой задачи");
                }
            }
        }

        private void DeleteTask()
        {
            if (MessageBox.Show("(В РАЗРАБОТКЕ) Данная задача будет удалена из проекта!", "Согласны продолжить?",
                                            MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                MessageBox.Show("(В РАЗРАБОТКЕ) Вы отменили удаление данной задачи");
            }
            else
            {
                MessageBox.Show("(В РАЗРАБОТКЕ) Задача удалена");
            }
        }
    }
}
