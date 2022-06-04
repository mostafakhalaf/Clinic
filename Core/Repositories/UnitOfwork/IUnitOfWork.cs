/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using Core.Entities;
using Core.Repositories.Base;
using Core.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.UnitOfwork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Patient> Patients { get; }
        IRepository<User> Users { get; }
        IRepository<Ticket> Tickets { get; }

         Task Complete();
    }
}
