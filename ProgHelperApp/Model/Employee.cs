using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgHelperApp.Model
{
    public class Employee
    {
        private Guid id_Employee;
        private string Name;
        private string SerName;
        private string Patronymic;
        private string Login;
        private string Password;
        private string Phone;
        private string Email;
        private string Position;

        [DisplayName("id")]
        public Guid id_Employee_F
        {
            get { return id_Employee; }
            set { id_Employee = value; }
        }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Требуется имя сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя сотрудника должно содержать минимум 3 символов")]
        public string Name_F
        {
            get { return Name; }
            set { Name = value; }
        }

        [DisplayName("SerName")]
        [Required(ErrorMessage = "Требуется фамилия сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Фамилия сотрудника должно содержать минимум 3 символов")]
        public string SerName_F
        {
            get { return SerName; }
            set { SerName = value; }
        }

        [DisplayName("Patronymic")]
        [Required(ErrorMessage = "Требуется отчество сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Отчество сотрудника должно содержать минимум 3 символов")]
        public string Patronymic_F
        {
            get { return Patronymic; }
            set { Patronymic = value; }
        }

        [DisplayName("Login")]
        [Required(ErrorMessage = "Требуется логин сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин сотрудника должно содержать минимум 3 символов")]
        public string Login_F
        {
            get { return Login; }
            set { Login = value; }
        }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Требуется пароль сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Пароль сотрудника должно содержать минимум 3 символов")]
        public string Password_F
        {
            get { return Password; }
            set { Password = value; }
        }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Требуется номер телефона сотрудника")]
        [RegularExpression(@"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$", ErrorMessage = "Номер телефона должен состоять из 11 цифр и начинаться с кода страны без \"+\"")]
        public string Phone_F
        {
            get { return Phone; }
            set { Phone = value; }
        }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Требуется email сотрудника")]
        [RegularExpression(@"^?*@mail.ru$", ErrorMessage = "Email должен иметь формат: _@mail.ru")]
        public string Email_F
        {
            get { return Email; }
            set { Email = value; }
        }

        [DisplayName("Position")]
        [Required(ErrorMessage = "Требуется должность сотрудника")]
        public string Position_F
        {
            get { return Position; }
            set { Position = value; }
        }
    }
}
