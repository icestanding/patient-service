using CodingChallange.Shared.Models.Patient;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodingChallange.Repositories.Patient.EFCore
{
    public class PatientDbContext: DbContext
    {
        public DbSet<PatientModel> Patients { get; set; }

        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.HasDefaultSchema("ClinicData");

            base.OnModelCreating(modelBuilder);
        }


    }
}
