/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Patient
    {
        [Key]
        public Guid ID { get; set; }
        public string? Name { get; set; }
        public string phoneNumber { get; set; }
        public bool IsDelete { get; set; }

    }
}
