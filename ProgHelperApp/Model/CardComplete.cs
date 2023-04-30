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
        private Guid id_Employee;
        private Guid id_Task;
        private Guid id_CardProject;
        private DateTime DateOfEnding;

        [DisplayName("id_Employee")]
        [Key]
        [Column(Order = 1)]
        public Guid id_Employee_F
        {
            get { return id_Employee; }
            set { id_Employee = value; }
        }

        [DisplayName("id_Task")]
        [Key]
        [Column(Order = 2)]
        public Guid id_Task_F
        {
            get { return id_Task; }
            set { id_Task = value; }
        }

        [DisplayName("id_CardProject")]
        [Key]
        [Column(Order = 3)]
        public Guid id_CardProject_F
        {
            get { return id_CardProject; }
            set { id_CardProject = value; }
        }

        [DisplayName("DateOfEnding")]
        [Required(ErrorMessage = "Требуется дата завершения выполнения задачи")]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])[- /.] (0[1-9]|1[012])[- /.] (19|20)\d\d$", ErrorMessage = "Дата должна иметь вид ДД/ММ/ГГГГ")]
        public DateTime DateOfEnding_F
        {
            get { return DateOfEnding; }
            set { DateOfEnding = value; }
        }

        public Task Task { get; set; }
        public CardProject CardProject { get; set; }
        public Employee Employee { get; set; }
    }
}
