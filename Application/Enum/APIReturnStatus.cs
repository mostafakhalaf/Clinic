/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enum
{
    public enum APIReturnStatus
    {
        Success = 1,
        Failure = 2,
        NotFound = 3,
        BadRequest = 4,
        NotActive = 5,
        NotAvailable = 6,
        WrongUser = 7,
        WrongPassword = 8,
        Exist = 9,
        UserEmailIsExist = 10,
        UserPhoneIsExist = 11,
        UserNameIsExist = 12,
        InternalError = 500,
    }
}
