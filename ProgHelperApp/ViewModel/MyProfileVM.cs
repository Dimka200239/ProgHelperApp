using ProgHelperApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgHelperApp.ViewModel
{
    public class MyProfileVM : INotifyPropertyChanged
    {
        private string _profileName;
        private string _profileSerName;
        private string _profilePatronymic;
        private string _profileLogin;
        private string _profilePhone;
        private string _profileEmail;
        private string _profilePosition;

        public event PropertyChangedEventHandler PropertyChanged;

        public MyProfileVM(Employee employee)
        {
            ProfileName = employee.Name_F;
            ProfileSerName = employee.SerName_F;
            ProfilePatronymic = employee.Patronymic_F;
            ProfileLogin = employee.Login_F;
            ProfilePhone = employee.Phone_F;
            ProfileEmail = employee.Email_F;
            ProfilePosition = employee.Position_F;
        }

        public string ProfileName
        {
            get { return _profileName; }
            set
            {
                _profileName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileName)));
            }
        }

        public string ProfileSerName
        {
            get { return _profileSerName; }
            set
            {
                _profileSerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileSerName)));
            }
        }

        public string ProfilePatronymic
        {
            get { return _profilePatronymic; }
            set
            {
                _profilePatronymic = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfilePatronymic)));
            }
        }

        public string ProfileLogin
        {
            get { return _profileLogin; }
            set
            {
                _profileLogin = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileLogin)));
            }
        }

        public string ProfilePhone
        {
            get { return _profilePhone; }
            set
            {
                _profilePhone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfilePhone)));
            }
        }

        public string ProfileEmail
        {
            get { return _profileEmail; }
            set
            {
                _profileEmail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfileEmail)));
            }
        }

        public string ProfilePosition
        {
            get { return _profilePosition; }
            set
            {
                _profilePosition = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProfilePosition)));
            }
        }
    }
}
