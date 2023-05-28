using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace ProgHelperApp.Model
{
    public class CardComplete
    {
        [DisplayName("id_CardComplete")]
        [Key]
        public Guid id_CardComplete_F { get; set; }

        [DisplayName("DateOfEnding")]
        [Required(ErrorMessage = "Требуется дата завершения выполнения задачи")]
        public string DateOfEnding_F { get; set; }

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

        [DisplayName("Description")]
        [Required(ErrorMessage = "Требуется отчет о выполненном задании")]
        [StringLength(5000, MinimumLength = 30, ErrorMessage = "Отчет должен иметь минимум 30 символов")]
        public string Description_F { get; set; }

        [ForeignKey(nameof(id_Employee_F))]
        public Employee Employee { get; set; }
    }
}
