/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Static
{
    public static class CheckerFolder
    {
        public static void CheckFolderExist(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
    }
}
