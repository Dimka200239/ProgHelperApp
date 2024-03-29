﻿using GalaSoft.MvvmLight.Command;
using ProgHelperApp.Model;
using ProgHelperApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProgHelperApp.ViewModel
{
    public class DirectorProfileVM : INotifyPropertyChanged
    {
        private readonly Frame MainFrame;
        public event PropertyChangedEventHandler PropertyChanged;

        private Page MyProfile;
        private Page AddNewEmployee;
        private Page AddNewTask;
        private Page FindAndEditProject;
        private Page FindAndEditTask;
        private Page FindCompletedProject;

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPage)));
            }
        }

        private double _frameOpacity;
        public double FrameOpacity
        {
            get { return _frameOpacity; }
            set
            {
                _frameOpacity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FrameOpacity)));
            }
        }

        public ICommand ExitFromProfileCommand { get; }
        public ICommand MyProfileCommand { get; }
        public ICommand AddNewEmployeeCommand { get; }
        public ICommand AddNewTaskCommand { get; }
        public ICommand FindAndEditProjectCommand { get; }
        public ICommand FindAndEditTaskCommand { get; }
        public ICommand FindCompletedProjectCommand { get; }

        public DirectorProfileVM(Frame MainFrame, Employee employee)
        {
            MyProfile = new MyProfileView(employee);
            AddNewEmployee = new AddNewEmployeeView();
            AddNewTask = new AddNewTaskView();
            FindAndEditProject = new FindAndEditProjectView();
            FindAndEditTask = new FindAndEditTaskView();
            FindCompletedProject = new FindCompletedProjectView();

            FrameOpacity = 1;
            CurrentPage = null;

            this.MainFrame = MainFrame;
            ExitFromProfileCommand = new RelayCommand(GoToExitFromProfile);
            MyProfileCommand = new RelayCommand(GoToMyProfile);
            AddNewEmployeeCommand = new RelayCommand(GoToAddNewEmployee);
            AddNewTaskCommand = new RelayCommand(GoToAddNewTask);
            FindAndEditProjectCommand = new RelayCommand(GoToFindAndEditProject);
            FindAndEditTaskCommand = new RelayCommand(GoToFindAndEditTask);
            FindCompletedProjectCommand = new RelayCommand(GoToFindCompletedProject);
        }

        private void GoToExitFromProfile()
        {
            if (MessageBox.Show("Выйти из профиля?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                MainFrame.Content = new UserLoginAndRegistrationPageView(MainFrame);
            }
        }

        private void GoToMyProfile()
        {
            SlowOpacity(MyProfile);
        }

        private void GoToAddNewEmployee()
        {
            SlowOpacity(AddNewEmployee);
        }

        private void GoToAddNewTask()
        {
            SlowOpacity(AddNewTask);
        }

        private void GoToFindAndEditProject()
        {
            SlowOpacity(FindAndEditProject);
        }

        private void GoToFindAndEditTask()
        {
            SlowOpacity(FindAndEditTask);
        }
        
        private void GoToFindCompletedProject()
        {
            SlowOpacity(FindCompletedProject);
        }

        private async void SlowOpacity(Page page)
        {
            await System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.2)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }

                CurrentPage = page;

                for (double i = 0.0; i < 1.1; i += 0.2)
                {
                    FrameOpacity = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}
