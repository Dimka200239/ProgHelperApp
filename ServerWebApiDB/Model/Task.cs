using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerWebApiDB.Model
{
    public class Task
    {
        [DisplayName("id")]
        [Key]
        public Guid id_Task_F { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Требуется заголовок задачи")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Заголовок задачи должен содержать минимум 3 символов")]
        public string Title_F { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Требуется описание задачи")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Описание задачи должно содержать минимум 3 символов")]
        public string Description_F { get; set; }

        [DisplayName("Deadline")]
        [Required(ErrorMessage = "Требуется дата кокнчания выполнения задачи")]
        [RegularExpression(@"^[0-9]{1,2}.[0-9]{1,2}.[0-9]{4} [0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Дата должна иметь вид ДД/ММ/ГГГГ")]
        public string Deadline_F { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Требуется статус задачи")]
        public string Status_F { get; set; }

        public List<TaskCardProjectMap> TaskCardProjectMaps { get; set; }
        public List<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; set; }
        public List<CardComplete> CardCompletes { get; set; }
    }
}
