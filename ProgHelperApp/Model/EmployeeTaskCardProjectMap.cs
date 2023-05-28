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
    public class EmployeeTaskCardProjectMap
    {
        [DisplayName("id_CardProject")]
        [Key]
        public Guid id_CardProject_F { get; set; }
        [ForeignKey(nameof(id_CardProject_F))]
        public CardProject CardProject { get; set; }

        [DisplayName("id_Task")]
        [Key]
        public Guid id_Task_F { get; set; }
        [ForeignKey(nameof(id_Task_F))]
        public Task Task { get; set; }

        [DisplayName("id_Employee")]
        [Key]
        public Guid id_Employee_F { get; set; }
        [ForeignKey(nameof(id_Employee_F))]
        public Employee Employee { get; set; }

        [DisplayName("id_Forwarded_Employee")]
        [Key]
        public Guid id_Forwarded_Employee_F { get; set; }
        [ForeignKey(nameof(id_Forwarded_Employee_F))]
        public Employee Forwarded_Employee { get; set; }
    }
}
