/*
Created By Mostafa Khalaf 
mostafakhalafmohamed@gmail.com

*/
using Core.Entities;
using Core.Repositories.UnitOfwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DatabaseInitializer
    {
        private readonly ModelBuilder modelBuilder;

        public DatabaseInitializer(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {

            modelBuilder.Entity<Patient>()
            .HasData(
                          new Patient { ID = new Guid("DDE4BA50-808E-479F-BE8B-72F69913442F"),Name="ahmed1",phoneNumber = "01025882940",IsDelete=false },
                          new Patient { ID = new Guid("DDE4BA51-808E-479F-BE8B-72F69913442F"), Name = "ahmed2", phoneNumber = "01025882941", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA52-808E-479F-BE8B-72F69913442F"), Name = "ahmed3", phoneNumber = "01025882942", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA53-808E-479F-BE8B-72F69913442F"), Name = "ahmed4", phoneNumber = "01025882943", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA54-808E-479F-BE8B-72F69913442F"), Name = "ahmed5", phoneNumber = "01025882944", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"), Name = "ahmed6", phoneNumber = "01025882945", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA56-808E-479F-BE8B-72F69913442F"), Name = "ahmed7", phoneNumber = "01025882946", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA57-808E-479F-BE8B-72F69913442F"), Name = "ahmed8", phoneNumber = "01025882947", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA58-808E-479F-BE8B-72F69913442F"), Name = "ahmed9", phoneNumber = "01025882948", IsDelete = false },
                          new Patient { ID = new Guid("DDE4BA59-808E-479F-BE8B-72F69913442F"), Name = "ahmed10", phoneNumber = "01025882949", IsDelete = false });
            modelBuilder.Entity<Ticket>()
            .HasData(

                  
                      new Ticket { ID = Guid.NewGuid(), Number = "1",Time=System.DateTime.Now, PatientID = new Guid("DDE4BA50-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "2", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA51-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "3", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA52-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "4", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA53-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "5", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA54-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "6", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA55-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "7", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA56-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "8", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA57-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "9", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA58-808E-479F-BE8B-72F69913442F"), IsDelete = false },
                      new Ticket { ID = Guid.NewGuid(), Number = "10", Time = System.DateTime.Now, PatientID = new Guid("DDE4BA59-808E-479F-BE8B-72F69913442F"), IsDelete = false }

                );
           var Password = BCrypt.Net.BCrypt.HashPassword("123");
            modelBuilder.Entity<User>()
                       .HasData(
                                 new User { ID = Guid.NewGuid(),UserName="admin" ,Password=Password,IsDelete=false,IsActive=true}
                           );





        }

    }
}
