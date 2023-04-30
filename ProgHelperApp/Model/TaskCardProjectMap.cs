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
    public class TaskCardProjectMap
    {
        private Guid id_Task;
        private Guid id_CardProject;

        [DisplayName("id_Task")]
        [Key]
        [Column(Order = 1)]
        public Guid id_Task_F
        {
            get { return id_Task; }
            set { id_Task = value; }
        }

        [DisplayName("id_CardProject")]
        [Key]
        [Column(Order = 2)]
        public Guid id_CardProject_F
        {
            get { return id_CardProject; }
            set { id_CardProject = value; }
        }

        public Task Task { get; set; }
        public CardProject CardProject { get; set; }
    }
}
