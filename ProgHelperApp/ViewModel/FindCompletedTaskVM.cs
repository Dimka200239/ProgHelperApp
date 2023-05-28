using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProgHelperApp.ViewModel
{
    public class FindCompletedTaskVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Employee employee;

        public ICommand LoadAllCompleteProjectCommand { get; }
        public ICommand CloseCompleteTaskCommand { get; }

        public ObservableCollection<Button> TextBlocksProjectTask { get; set; }
        public ObservableCollection<Button> TextBlocksTask { get; set; }

        public FindCompletedTaskVM(Employee employee)
        {
            this.employee = employee;

            LoadAllCompleteProjectCommand = new RelayCommand(LoadAllCompleteProject);
            CloseCompleteTaskCommand = new RelayCommand(CloseCompleteTask);

            TextBlocksProjectTask = new ObservableCollection<Button>();
            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksProjectTask.Clear();
            TextBlocksTask.Clear();
        }

        private string _findProjectTaskId;
        private string _findTaskId;
        private string _findFieldProjectTask;
        private string _descriptionTask;

        public string FindProjectTaskId
        {
            get { return _findProjectTaskId; }
            set
            {
                _findProjectTaskId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectTaskId)));
            }
        }

        public string FindTaskId
        {
            get { return _findTaskId; }
            set
            {
                _findTaskId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindTaskId)));
            }
        }

        public string FindFieldProjectTask
        {
            get { return _findFieldProjectTask; }
            set
            {
                _findFieldProjectTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindFieldProjectTask)));
            }
        }

        public string DescriptionTask
        {
            get { return _descriptionTask; }
            set
            {
                _descriptionTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionTask)));
            }
        }

        private async void LoadAllCompleteProject()
        {
            TextBlocksProjectTask.Clear();

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
            DescriptionTask = null;

            var btn = sender as Button;
            var infoTask = btn.Content.ToString().Split(';');
            FindTaskId = infoTask[0];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/completedTask/GetCardComplite/{FindTaskId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<CardComplete>(responseContent);

                    if (result != null)
                    {
                        DescriptionTask = result.Description_F;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void CloseCompleteTask()
        {
            if (FindTaskId != null)
            {
                if (DescriptionTask != null)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:44392");

                        var response = await client.GetAsync($"/api/completedTask/UpdatetaskStatus/{FindTaskId}/{"true"}");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Задача успешно закрыта");

                            DescriptionTask = null;
                            FindTaskId = null;
                        }
                        else
                        {
                            MessageBox.Show("Ошибка при обновлении данных");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("У данной задачи еще нет карты выполнения");
                }
            }
            else
            {
                MessageBox.Show("Ошибка при обновлении данных");
            }
        }
    }
}
