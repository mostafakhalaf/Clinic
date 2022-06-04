/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ticket
    {
        [Key]
        public Guid ID { get; set; }
        public string? Number { get; set; }
        public DateTime Time { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey(nameof(PatientID))]
        public virtual Patient Patient { get; set; }
        public Guid PatientID { get; set; }
    }
}
