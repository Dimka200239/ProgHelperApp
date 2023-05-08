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

namespace ServerWebApiDB.Model
{
    public class CardProject
    {
        [DisplayName("id")]
        [Key]
        public Guid id_CardProject_F { get; set; }

        [DisplayName("Title")]
        [Required(ErrorMessage = "Требуется заголовок проекта")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Заголовок проекта должен содержать минимум 3 символов")]
        public string Title_F { get; set; }

            [DisplayName("Description")]
        [Required(ErrorMessage = "Требуется описание проекта")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "Описание проекта должно содержать минимум 3 символов")]
        public string Description_F { get; set; }

        [DisplayName("DateOfBegining")]
        [Required(ErrorMessage = "Требуется дата начала проекта")]
        [RegularExpression(@"^[0-9]{1,2}.[0-9]{1,2}.[0-9]{4} [0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}$", ErrorMessage = "Дата должна иметь вид ДД/ММ/ГГГГ")]
        public string DateOfBegining_F { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Требуется статус проекта")]
        public string Status_F { get; set; }

        [DisplayName("id_Employee")]
        public Guid id_Employee_F { get; set; }

        public List<CardProjectEmployeeMap> CardProjectEmployeeMaps { get; set; }
        public List<TaskCardProjectMap> TaskCardProjectMaps { get; set; }
        public List<EmployeeTaskCardProjectMap> EmployeeTaskCardProjectMaps { get; set; }
        public List<CardComplete> CardCompletes { get; set; }

        [ForeignKey("id_Employee_F")]
        public Employee Employee { get; set; }
    }
}
