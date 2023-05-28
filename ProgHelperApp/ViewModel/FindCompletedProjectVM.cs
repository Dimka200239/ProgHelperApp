using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProgHelperApp.ViewModel
{
    public class FindCompletedProjectVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand LoadAllCompleteProjectCommand { get; }
        public ICommand CloseCompleteProjectCommand { get; }

        public ObservableCollection<Button> TextBlocksProjectTask { get; set; }
        public ObservableCollection<Button> TextBlocksTask { get; set; }

        public FindCompletedProjectVM()
        {
            LoadAllCompleteProjectCommand = new RelayCommand(LoadAllCompleteProject);
            CloseCompleteProjectCommand = new RelayCommand(CloseCompleteProject);

            TextBlocksProjectTask = new ObservableCollection<Button>();
            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksProjectTask.Clear();
            TextBlocksTask.Clear();
        }

        private string _findProjectTaskId;
        private string _findTaskId;
        private string _descriptionTask;
        private string _findFieldProjectTask;

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

        public string DescriptionTask
        {
            get { return _descriptionTask; }
            set
            {
                _descriptionTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionTask)));
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

        private async void LoadAllCompleteProject()
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
        private async void Click_Button_Task(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoTask = btn.Content.ToString().Split(';');
            FindTaskId = infoTask[0];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/completedProject/GetCardComplite/{FindTaskId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Model.CardComplete>(responseContent);

                    if (result != null)
                    {
                        DescriptionTask = "Дата выполенния: " + result.DateOfEnding_F + "\n\n" + result.Description_F;
                    }
                    else
                    {
                        MessageBox.Show("Данная задача ещё не закрыта!");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void CloseCompleteProject()
        {
            var checkIsExist = true;

            foreach (var el in TextBlocksTask)
            {
                if (el.Content.ToString().Split(';')[4] != "Закрыта")
                {
                    checkIsExist = false;
                    
                    break;
                }
            }

            if (!checkIsExist)
            {
                MessageBox.Show("Данный проект не может быть закрыт, т.к. не все задачи в рамках проекта закрыты!");
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44392");

                    var response = await client.GetAsync($"/api/completedProject/UpdateCardProjectStatus/{FindProjectTaskId}");

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Проект успешно закрыт!");

                        string mainDirectoryPath = @"C:\reports";

                        if (!Directory.Exists(mainDirectoryPath))
                        {
                            Directory.CreateDirectory(mainDirectoryPath);
                        }

                        string directoryPath = @"C:\reports\" + FindProjectTaskId;

                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        foreach (var el in TextBlocksTask)
                        {
                            var idTask = el.Content.ToString().Split(';')[0];

                            var secondResponse = await client.GetAsync($"/api/completedProject/GetCardComplite/{idTask}");

                            if (secondResponse.IsSuccessStatusCode)
                            {
                                string responseContent = await secondResponse.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<Model.CardComplete>(responseContent);

                                string filePath = Path.Combine(directoryPath, $"{result.id_Task_F}.txt");

                                File.WriteAllText(filePath, "Дата выполенния: " + result.DateOfEnding_F + "\n\n" + result.Description_F);
                            }
                        }

                        MessageBox.Show($"Отчеты успешно выгружена! Путь: {directoryPath}");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных");
                    }
                }
            }
        }
    }
}
