using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerWebApiDB.Model
{
    public class Employee
    {
        [DisplayName("id")]
        [Key]
        public Guid id_Employee_F { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Требуется имя сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя сотрудника должно содержать минимум 3 символов")]
        public string Name_F { get; set; }

        [DisplayName("SerName")]
        [Required(ErrorMessage = "Требуется фамилия сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Фамилия сотрудника должно содержать минимум 3 символов")]
        public string SerName_F { get; set; }

        [DisplayName("Patronymic")]
        [Required(ErrorMessage = "Требуется отчество сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Отчество сотрудника должно содержать минимум 3 символов")]
        public string Patronymic_F { get; set; }

        [DisplayName("Login")]
        [Required(ErrorMessage = "Требуется логин сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Логин сотрудника должно содержать минимум 3 символов")]
        public string Login_F { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Требуется пароль сотрудника")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Пароль сотрудника должно содержать минимум 3 символов")]
        public string Password_F { get; set; }

        [DisplayName("Phone")]
        [Required(ErrorMessage = "Требуется номер телефона сотрудника")]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$", ErrorMessage = "Номер телефона должен состоять из 11 цифр и начинаться с кода страны без \"+\"")]
        public string Phone_F { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Требуется email сотрудника")]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$", ErrorMessage = "Email имеет неверный формат")]
        public string Email_F { get; set; }

        [DisplayName("Position")]
        [Required(ErrorMessage = "Требуется должность сотрудника")]
        public string Position_F { get; set; }

        public List<CardProjectEmployeeMap> CardProjectEmployeeMaps { get; set; }
        public List<CardProject> CardProjects { get; set; }
        public List<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; set; }
        public List<CardComplete> CardCompletes { get; set; }
    }
}
