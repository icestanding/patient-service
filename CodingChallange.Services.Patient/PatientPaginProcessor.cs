using CodingChallange.Shared.Models.Patient;
using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallange.Services.Patient
{
    public class PatientPaginProcessor : SieveProcessor
    {
        public PatientPaginProcessor(
            IOptions<SieveOptions> options,
            ISieveCustomSortMethods customSortMethods,
            ISieveCustomFilterMethods customFilterMethods)
            : base(options, customSortMethods, customFilterMethods)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<PatientModel>(p => p.FirstName)
                .CanFilter()
                .CanSort()
                .HasName("first_name");
            mapper.Property<PatientModel>(p => p.LastName)
                .CanFilter()
                .CanSort()
                .HasName("last_name");
            mapper.Property<PatientModel>(p => p.Gender)
                .CanFilter()
                .CanSort()
                .HasName("gender");
            mapper.Property<PatientModel>(p => p.DateOfBirth)
                .CanFilter()
                .CanSort()
                .HasName("date_of_birth");
            mapper.Property<PatientModel>(p => p.IsActive)
                .CanFilter()
                .CanSort()
                .HasName("created_at");
            mapper.Property<PatientModel>(p => p.CreateTime)
                .CanFilter()
                .CanSort()
                .HasName("updated_at"); 
            mapper.Property<PatientModel>(p => p.UpdateTime)
                 .CanFilter()
                 .CanSort()
                 .HasName("is_active");

            return mapper;
        }
    }
}
