using CodingChallange.Shared.Models.Patient;
using CodingChallange.Shared.Patient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingChallange.Repositories.Patient.Tests
{
    public static class TestDataGenerateHelper
    {
        public static List<PatientModel> createPatientModelData()
        {

            return new List<PatientModel>() {
                new PatientModel() {
                    Id = new Guid(),
                    FirstName = "John",
                    LastName = "Hu",
                    Gender = Gender.male,
                    DateOfBirth = new DateTime(1992, 2, 14),
                    Email = "test@test.com",
                    Phone = "0425789632",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                },
                new PatientModel() {
                    Id = new Guid(),
                    FirstName = "Ann",
                    LastName = "Hu",
                    Gender = Gender.male,
                    DateOfBirth = new DateTime(1992, 2, 14),
                    Email = "test@test.com",
                    Phone = "0425789632",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                },
                new PatientModel() {
                    Id = new Guid(),
                    FirstName = "Lee",
                    LastName = "Hu",
                    Gender = Gender.male,
                    DateOfBirth = new DateTime(1992, 2, 14),
                    Email = "test@test.com",
                    Phone = "0425789632",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                },
                new PatientModel() {
                    Id = new Guid(),
                    FirstName = "See",
                    LastName = "Hu",
                    Gender = Gender.male,
                    DateOfBirth = new DateTime(1992, 2, 14),
                    Email = "test@test.com",
                    Phone = "0425789632",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                },
                new PatientModel() {
                    Id = new Guid(),
                    FirstName = "Ge",
                    LastName = "Hu",
                    Gender = Gender.male,
                    DateOfBirth = new DateTime(1992, 2, 14),
                    Email = "test@test.com",
                    Phone = "0425789632",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                },
            };
        }
    }
}
