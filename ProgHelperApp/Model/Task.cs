using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgHelperApp.Model
{
    public class Task
    {
        private Guid id_Task;
        private string Title;
        private string Description;
        private DateTime Deadline;
        private string Status;

        [DisplayName("id")]
        [Key]
        public Guid id_Task_F
        {
            get { return id_Task; }
            set { id_Task = value; }
        }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Требуется заголовок задачи")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Заголовок задачи должен содержать минимум 3 символов")]
        public string Title_F
        {
            get { return Title; }
            set { Title = value; }
        }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Требуется описание задачи")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Описание задачи должно содержать минимум 3 символов")]
        public string Description_F
        {
            get { return Description; }
            set { Description = value; }
        }

        [DisplayName("Deadline")]
        [Required(ErrorMessage = "Требуется дата кокнчания выполнения задачи")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.] (0[1-9]|1[012])[- /.] (19|20)\d\d$", ErrorMessage = "Дата должна иметь вид ДД/ММ/ГГГГ")]
        public DateTime Deadline_F
        {
            get { return Deadline; }
            set { Deadline = value; }
        }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Требуется статус задачи")]
        public string Status_F
        {
            get { return Status; }
            set { Status = value; }
        }

        public List<TaskCardProjectMap> TaskCardProjectMaps { get; set; }
        public List<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; set; }
        public List<CardComplete> CardCompletes { get; set; }
    }
}
