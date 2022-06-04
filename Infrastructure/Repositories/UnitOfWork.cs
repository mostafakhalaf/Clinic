/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using Core.Entities;
using Core.Repositories.Base;
using Core.Repositories.Interface;
using Core.Repositories.UnitOfwork;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ClinicDBContext _context;

        public IRepository<Patient> Patients { get; private set; }
        public IRepository<User> Users { get; private set; }
        public IRepository<Ticket> Tickets { get; private set; }

        public UnitOfWork(ClinicDBContext context)
        {
            _context = context;

            Users = new Repository<User>(_context);
            Patients = new Repository<Patient>(_context);
            Tickets = new Repository<Ticket>(_context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
