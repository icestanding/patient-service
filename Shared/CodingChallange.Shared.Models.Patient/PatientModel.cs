using CodingChallange.Shared.Patient;
using System;

namespace CodingChallange.Shared.Models.Patient
{
    public class PatientModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastNanme { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
}
