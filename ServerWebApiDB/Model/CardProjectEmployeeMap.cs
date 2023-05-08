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
    public class CardProjectEmployeeMap
    {
        [DisplayName("id_CardProject")]
        [Key]
        public Guid id_CardProject_F { get; set; }
        [ForeignKey(nameof(id_CardProject_F))]
        public CardProject CardProject { get; set; }

        [DisplayName("id_Employee")]
        [Key]
        public Guid id_Employee_F { get; set; }
        [ForeignKey(nameof(id_Employee_F))]
        public Employee Employee { get; set; }
    }
}
