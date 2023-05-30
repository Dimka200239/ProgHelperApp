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
    public class FindAndEditProjectVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SaveProjectCommand { get; }
        public ICommand FindProjectByNameCommand { get; }
        public ICommand FindEmployeeByNameCommand { get; }

        public ObservableCollection<Button> TextBlocksProject { get; set; }
        public ObservableCollection<Button> TextBlocksEmployee { get; set; }

        public FindAndEditProjectVM()
        {
            TextBlocksProject = new ObservableCollection<Button>();
            TextBlocksEmployee = new ObservableCollection<Button>();
            TextBlocksProject.Clear();
            TextBlocksEmployee.Clear();

            SaveProjectCommand = new RelayCommand(SaveProject);
            FindProjectByNameCommand = new RelayCommand(FindProjectByName);
            FindEmployeeByNameCommand = new RelayCommand(FindEmployeeByName);
        }

        private string _findFieldProject;
        private string _findProjectId;
        private string _findProjectTitle;
        private string _findProjectDescription;
        private string _findProjectDateOfBegining;
        private string _findProjectStatus;
        private string _findProjectManager;
        private string _findProjectManagerField;
        private string _lastIdController = "last";

        public string FindFieldProject
        {
            get { return _findFieldProject; }
            set
            {
                _findFieldProject = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindFieldProject)));
            }
        }

        public string FindProjectId
        {
            get { return _findProjectId; }
            set
            {
                _findProjectId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectId)));
            }
        }

        public string FindProjectTitle
        {
            get { return _findProjectTitle; }
            set
            {
                _findProjectTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectTitle)));
            }
        }

        public string FindProjectDescription
        {
            get { return _findProjectDescription; }
            set
            {
                _findProjectDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectDescription)));
            }
        }

        public string FindProjectDateOfBegining
        {
            get { return _findProjectDateOfBegining; }
            set
            {
                _findProjectDateOfBegining = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectDateOfBegining)));
            }
        }

        public string FindProjectStatus
        {
            get { return _findProjectStatus; }
            set
            {
                _findProjectStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectStatus)));
            }
        }

        public string FindProjectManager
        {
            get { return _findProjectManager; }
            set
            {
                _findProjectManager = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectManager)));
            }
        }

        public string FindProjectManagerField
        {
            get { return _findProjectManagerField; }
            set
            {
                _findProjectManagerField = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindProjectManagerField)));
            }
        }

        public string LastIdController { get { return _lastIdController; } set { _lastIdController = value; } }

        private async void SaveProject()
        {
            try
            {
                var cardProject = new CardProject();
                cardProject.id_CardProject_F = new Guid(FindProjectId);
                cardProject.Title_F = FindProjectTitle;
                cardProject.Description_F = FindProjectDescription;
                cardProject.DateOfBegining_F = FindProjectDateOfBegining;
                cardProject.Status_F = FindProjectStatus;
                cardProject.id_Employee_F = new Guid(FindProjectManagerField);

                new Common.DataValidationContext().Validate(cardProject);

                var cardProjectEmployeeMap = new CardProjectEmployeeMap();
                cardProjectEmployeeMap.id_CardProject_F = cardProject.id_CardProject_F;
                cardProjectEmployeeMap.id_Employee_F = cardProject.id_Employee_F;

                new Common.DataValidationContext().Validate(cardProjectEmployeeMap);

                var oldCardProjectEmployeeMap = new CardProjectEmployeeMap();
                oldCardProjectEmployeeMap.id_CardProject_F = cardProject.id_CardProject_F;
                oldCardProjectEmployeeMap.id_Employee_F = new Guid(LastIdController);

                new Common.DataValidationContext().Validate(oldCardProjectEmployeeMap);

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44392");

                    var firstResponse = await client.PutAsJsonAsync($"/api/editEmployee/UpdateInfoAboutProject", cardProject);
                    var secondResponse = await client.DeleteAsync($"/api/editEmployee/DeleteInfoAboutProjectEmployeeMap/{oldCardProjectEmployeeMap.id_CardProject_F}/{oldCardProjectEmployeeMap.id_Employee_F}");
                    var thirdResponse = await client.PostAsJsonAsync($"/api/editEmployee/UpdateInfoAboutProjectEmployeeMap", cardProjectEmployeeMap);

                    if (firstResponse.IsSuccessStatusCode && secondResponse.IsSuccessStatusCode && thirdResponse.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Успешное обновление");

                        LastIdController = "last";

                        TextBlocksProject.Clear();
                        TextBlocksEmployee.Clear();
                        FindFieldProject = null;
                        FindProjectId = null;
                        FindProjectTitle = null;
                        FindProjectDescription = null;
                        FindProjectDateOfBegining = null;
                        FindProjectStatus = null;
                        FindProjectManager = null;
                        FindProjectManagerField = null;
                    }
                    else
                    {
                        MessageBox.Show("При обновлении произошла ошибка...");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Проверьте корректно ли заполнены все поля!");
            }
        }

        private async void FindProjectByName()
        {
            TextBlocksProject.Clear();
            TextBlocksEmployee.Clear();
            FindProjectId = null;
            FindProjectTitle = null;
            FindProjectDescription = null;
            FindProjectDateOfBegining = null;
            FindProjectStatus = null;
            FindProjectManager = null;
            FindProjectManagerField = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/editEmployee/FindProjectByName/{FindFieldProject}");

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

                            TextBlocksProject.Add(newButton);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private void Click_Button_Project(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var infoProject = btn.Content.ToString().Split(';');
            FindProjectId = infoProject[0];
            FindProjectTitle = infoProject[1];
            FindProjectDescription = infoProject[2];
            FindProjectDateOfBegining = infoProject[3];
            FindProjectStatus = infoProject[4];
            FindProjectManagerField = infoProject[5];

            LastIdController = infoProject[5];
        }

        private async void FindEmployeeByName()
        {
            TextBlocksEmployee.Clear();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44392");

                var response = await client.GetAsync($"/api/employee/FindController/{FindProjectManager}");

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
                                Content = employee.Name_F + " " + employee.SerName_F + " " + employee.Patronymic_F + ":" + employee.id_Employee_F
                            };

                            newButton.Click += Click_Button_Employee;

                            TextBlocksEmployee.Add(newButton);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных");
                }
            }
        }

        private void Click_Button_Employee(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            FindProjectManagerField = btn.Content.ToString().Split(':')[1];
        }
    }
}
