using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProgHelperApp.Model
{
    public class CardProject
    {
        private Guid id_CardProject;
        private string Title;
        private string Description;
        private DateTime DateOfBegining;
        private string Status;
        private Guid id_ProjectManager;

        [DisplayName("id")]
        [Key]
        public Guid id_CardProject_F
        {
            get { return id_CardProject; }
            set { id_CardProject = value; }
        }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Требуется заголовок проекта")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Заголовок проекта должен содержать минимум 3 символов")]
        public string Title_F
        {
            get { return Title; }
            set { Title = value; }
        }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Требуется описание проекта")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Описание проекта должно содержать минимум 3 символов")]
        public string Description_F
        {
            get { return Description; }
            set { Description = value; }
        }

        [DisplayName("DateOfBegining")]
        [Required(ErrorMessage = "Требуется дата начала проекта")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.] (0[1-9]|1[012])[- /.] (19|20)\d\d$", ErrorMessage = "Дата должна иметь вид ДД/ММ/ГГГГ")]
        public DateTime DateOfBegining_F
        {
            get { return DateOfBegining; }
            set { DateOfBegining = value; }
        }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Требуется статус проекта")]
        public string Status_F
        {
            get { return Status; }
            set { Status = value; }
        }

        [DisplayName("id_ProjectManager")]
        public Guid id_ProjectManager_F
        {
            get { return id_ProjectManager; }
            set { id_ProjectManager = value; }
        }

        public Employee Employee { get; set; }
        public List<CardProjectEmployeeMap> CardProjectEmployeeMaps { get; set; }
        public List<TaskCardProjectMap> TaskCardProjectMaps { get; set; }
        public List<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; set; }
        public List<CardComplete> CardCompletes { get; set; }
    }
}
