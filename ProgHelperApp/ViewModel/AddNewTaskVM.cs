using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ProgHelperApp.ViewModel
{
    public class AddNewTaskVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand FindEmployeeCommand { get; }
        public ICommand ChooseEmployeeCommand { get; }
        public ICommand AddNewTaskCommand { get; }
        public ICommand AddNewTaskAddCommand { get; }
        public ObservableCollection<Button> TextBlocks { get; set; }

        private string _findFieldTaskManager;
        private string _addNewTaskIdManager;
        private string _addNewTaskStatus;
        private string _addNewTaskDateOfBegining;
        private string _addNewTaskDescription;
        private string _addNewTaskTitle;

        public ObservableCollection<Button> TextBlocksTask { get; set; }
        private string _titleTask;
        private string _descriptionTask;
        private string _daysTask;
        private string _statusTask;

        private List<ProgHelperApp.Model.Task> newTasks = new List<ProgHelperApp.Model.Task>();

        public AddNewTaskVM()
        {
            FindEmployeeCommand = new RelayCommand(FindEmployee);
            TextBlocks = new ObservableCollection<Button>();
            TextBlocksTask = new ObservableCollection<Button>();
            TextBlocksTask.Clear();
            newTasks.Clear();
            ChooseEmployeeCommand = new RelayCommand(ChooseEmployee);
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            AddNewTaskAddCommand = new RelayCommand(AddNewTaskAdd);

            AddNewTaskStatus = "Открыта";
            StatusTask = "Открыта";
            AddNewTaskDateOfBegining = DateTime.UtcNow.ToString();
        }

        public string FindFieldTaskManager
        {
            get { return _findFieldTaskManager; }
            set
            {
                _findFieldTaskManager = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FindFieldTaskManager)));
            }
        }

        public string AddNewTaskIdManager
        {
            get { return _addNewTaskIdManager; }
            set
            {
                _addNewTaskIdManager = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewTaskIdManager)));
            }
        }

        public string AddNewTaskStatus
        {
            get { return _addNewTaskStatus; }
            set
            {
                _addNewTaskStatus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewTaskStatus)));
            }
        }

        public string AddNewTaskDateOfBegining
        {
            get { return _addNewTaskDateOfBegining; }
            set
            {
                _addNewTaskDateOfBegining = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewTaskDateOfBegining)));
            }
        }

        public string AddNewTaskDescription
        {
            get { return _addNewTaskDescription; }
            set
            {
                _addNewTaskDescription = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewTaskDescription)));
            }
        }

        public string AddNewTaskTitle
        {
            get { return _addNewTaskTitle; }
            set
            {
                _addNewTaskTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddNewTaskTitle)));
            }
        }

        public string TitleTask
        {
            get { return _titleTask; }
            set
            {
                _titleTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TitleTask)));
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

        public string DaysTask
        {
            get { return _daysTask; }
            set
            {
                _daysTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DaysTask)));
            }
        }

        public string StatusTask
        {
            get { return _statusTask; }
            set
            {
                _statusTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusTask)));
            }
        }

        private void FindEmployee()
        {
            TextBlocks.Clear();

            List<Employee> result = null;//AddNewTaskRepository.FindEmployee(FindFieldTaskManager);

            if (result != null)
            {
                foreach (Employee employee in result)
                {
                    var newButton = new Button
                    {
                        Content = employee.Name_F + " " + employee.SerName_F + " " + employee.Patronymic_F + ":" + employee.id_Employee_F,
                        Command = ChooseEmployeeCommand
                    };

                    newButton.Click += Click_Button;

                    TextBlocks.Add(newButton);
                }
            }
        }

        private void ChooseEmployee()
        {

        }

        private void Click_Button(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            AddNewTaskIdManager = btn.Content.ToString();
        }

        private void AddNewTaskAdd()
        {
            try
            {
                var newTask = new ProgHelperApp.Model.Task();
                newTask.id_Task_F = Guid.NewGuid();
                newTask.Title_F = TitleTask;
                newTask.Description_F = DescriptionTask;
                newTask.Deadline_F = DateTime.UtcNow.AddDays(int.Parse(DaysTask)).ToString();
                newTask.Status_F = StatusTask;

                new Common.DataValidationContext().Validate(newTask);

                var newButton = new Button
                {
                    Content = newTask.Title_F,
                };

                TextBlocksTask.Add(newButton);

                newTasks.Add(newTask);

                TitleTask = "";
                DescriptionTask = "";
                DaysTask = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddNewTask()
        {
            try
            {
                var managerId = "";
                if (_addNewTaskIdManager != "" && _addNewTaskIdManager != null)
                {
                    managerId = _addNewTaskIdManager.Split(':')[1];
                }

                var cardProject = new CardProject();
                cardProject.id_CardProject_F = Guid.NewGuid();
                cardProject.Title_F = AddNewTaskTitle;
                cardProject.Description_F = AddNewTaskDescription;
                cardProject.DateOfBegining_F = AddNewTaskDateOfBegining;
                cardProject.Status_F = AddNewTaskStatus;
                if (managerId != "")
                {
                    cardProject.id_Employee_F = new Guid(managerId);
                }

                new Common.DataValidationContext().Validate(cardProject);

                var result = true;//AddNewTaskRepository.AddNewTask(cardProject, newTasks);

                if (result == true)
                {
                    MessageBox.Show("Успешное добавление");
                    AddNewTaskTitle = "";
                    AddNewTaskDescription = "";
                    AddNewTaskDateOfBegining = "";
                    AddNewTaskStatus = "";
                    AddNewTaskIdManager = "";
                    TextBlocks.Clear();
                    TextBlocksTask.Clear();
                }
                else
                {
                    MessageBox.Show("При добавлении произошла ошибка...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
