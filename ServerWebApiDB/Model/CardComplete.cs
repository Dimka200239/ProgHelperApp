using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServerWebApiDB.Model
{
    public class CardComplete
    {
        [DisplayName("id_CardComplete")]
        [Key]
        public Guid id_CardComplete_F { get; set; }

        [DisplayName("DateOfEnding")]
        [Required(ErrorMessage = "Требуется дата завершения выполнения задачи")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.] (0[1-9]|1[012])[- /.] (19|20)\d\d$", ErrorMessage = "Дата должна иметь вид ДД/ММ/ГГГГ")]
        public DateTime DateOfEnding_F { get; set; }

        [DisplayName("id_CardProject")]
        public Guid id_CardProject_F { get; set; }
        [ForeignKey(nameof(id_CardProject_F))]
        public CardProject CardProject { get; set; }

        [DisplayName("id_Task")]
        public Guid id_Task_F { get; set; }
        [ForeignKey(nameof(id_Task_F))]
        public Task Task { get; set; }

        [DisplayName("id_Employee")]
        public Guid id_Employee_F { get; set; }
        [ForeignKey(nameof(id_Employee_F))]
        public Employee Employee { get; set; }
    }
}
