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
    public class AddCardCompleteVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Employee employee;

        public ICommand UpdateCommand { get; }
        public ICommand CloseCompleteTaskCommand { get; }

        public ObservableCollection<Button> TextBlocksTask { get; set; }

        public AddCardCompleteVM(Employee employee)
        {
            this.employee = employee;

            UpdateCommand = new RelayCommand(Update);
            CloseCompleteTaskCommand = new RelayCommand(CloseCompleteTask);

            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksTask.Clear();
        }

        private string _findProjectTaskId;
        private string _findTaskId;
        private string _descriptionTask;
        private string _descriptionProjectTask;
        private string _titleProject;

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

        public string DescriptionProjectTask
        {
            get { return _descriptionProjectTask; }
            set
            {
                _descriptionProjectTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DescriptionProjectTask)));
            }
        }

        public string TitleProject
        {
            get { return _titleProject; }
            set
            {
                _titleProject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleProject)));
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

                                newButton.Click += MouseDoubleClick_Button_Task;

                                TextBlocksTask.Add(newButton);
                            }
                        }
                    }

                    FindProjectTaskId = null;
                    FindTaskId = null;
                    DescriptionTask = null;
                    DescriptionProjectTask = null;
                    TitleProject = null;
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void MouseDoubleClick_Button_Task(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoTask = btn.Content.ToString().Split(';');

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/employeeTask/GetTaskById/{infoTask[2]}/{"true"}");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<Model.Task>(responseContent);

                    FindTaskId = result.id_Task_F.ToString();
                    FindProjectTaskId = infoTask[1];
                    TitleProject = result.Title_F;
                    DescriptionProjectTask = result.Description_F;
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private async void CloseCompleteTask()
        {
            try
            {
                var newCardComplete = new CardComplete();
                newCardComplete.id_CardComplete_F = Guid.NewGuid();
                newCardComplete.DateOfEnding_F = DateTime.UtcNow.ToString();
                newCardComplete.id_CardProject_F = new Guid(FindProjectTaskId);
                newCardComplete.id_Task_F = new Guid(FindTaskId);
                newCardComplete.id_Employee_F = employee.id_Employee_F;
                newCardComplete.Description_F = DescriptionTask;

                new Common.DataValidationContext().Validate(newCardComplete);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44392");

                    var check = await client.GetAsync($"/api/employeeTask/CheckCardComplete/{newCardComplete.id_CardProject_F}/{newCardComplete.id_Task_F}/{newCardComplete.id_Employee_F}");

                    if (check.IsSuccessStatusCode)
                    {
                        string responseContent = await check.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Model.CardComplete>(responseContent);

                        if (result == null)
                        {
                            var response = await client.PostAsJsonAsync($"/api/employeeTask/AddCardComplete", newCardComplete);

                            if (response.IsSuccessStatusCode)
                            {
                                MessageBox.Show("Карта выполнения успешно сформирована");
                            }
                            else
                            {
                                MessageBox.Show("Ошибка при обновлении данных");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Карта выполенния уже создана для этой задачи!");
                        }

                        FindProjectTaskId = null;
                        FindTaskId = null;
                        DescriptionTask = null;
                        DescriptionProjectTask = null;
                        TitleProject = null;
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при обновлении данных");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
