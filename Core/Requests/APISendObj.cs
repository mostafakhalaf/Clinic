/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class APISendObj<T>
    {
        public T SendValue { get; set; }
        public Guid UserID { get; set; }
    }
}
