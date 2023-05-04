using GalaSoft.MvvmLight.Command;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using ProgHelperApp._Repository.Repository;
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
        public ObservableCollection<Button> TextBlocks { get; set; }

        private string _findFieldTaskManager;
        private string _addNewTaskIdManager;
        private string _addNewTaskStatus;
        private string _addNewTaskDateOfBegining;
        private string _addNewTaskDescription;
        private string _addNewTaskTitle;

        public AddNewTaskVM()
        {
            FindEmployeeCommand = new RelayCommand(FindEmployee);
            TextBlocks = new ObservableCollection<Button>();
            ChooseEmployeeCommand = new RelayCommand(ChooseEmployee);
            AddNewTaskCommand = new RelayCommand(AddNewTask);

            AddNewTaskStatus = "Открыта";
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

        private void FindEmployee()
        {
            TextBlocks.Clear();

            var result = AddNewTaskRepository.FindEmployee(FindFieldTaskManager);

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

        private void AddNewTask()
        {
            try
            {
                var managerId = _addNewTaskIdManager.Split(':')[1];

                var cardProject = new CardProject();
                cardProject.id_CardProject_F = Guid.NewGuid();
                cardProject.Title_F = AddNewTaskTitle;
                cardProject.Description_F = AddNewTaskDescription;
                cardProject.DateOfBegining_F = AddNewTaskDateOfBegining;
                cardProject.Status_F = AddNewTaskStatus;
                cardProject.id_Employee_F = new Guid(managerId);

                new Common.DataValidationContext().Validate(cardProject);

                var result = AddNewTaskRepository.AddNewTask(cardProject);

                if (result == true)
                {
                    MessageBox.Show("Успешное добавление");
                    AddNewTaskTitle = "";
                    AddNewTaskDescription = "";
                    AddNewTaskDateOfBegining = "";
                    AddNewTaskStatus = "";
                    AddNewTaskIdManager = "";
                    TextBlocks.Clear();
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
