/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Requests
{
    public class APIReturnObj<T>
    {
        public T ReturnValue { get; set; }
        public APIReturnStatus Status { get; set; }
    }
}
