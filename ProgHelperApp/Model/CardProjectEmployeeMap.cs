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
    public class CardProjectEmployeeMap
    {
        private Guid id_CardProject;
        private Guid id_Employee;

        [DisplayName("id_CardProject")]
        [Key]
        [Column(Order = 1)]
        public Guid id_CardProject_F
        {
            get { return id_CardProject; }
            set { id_CardProject = value; }
        }

        [DisplayName("id_Employee")]
        [Key]
        [Column(Order = 2)]
        public Guid id_Employee_F
        {
            get { return id_Employee; }
            set { id_Employee = value; }
        }

        public Employee Employee { get; set; }
        public CardProject CardProject { get; set; }
    }
}
