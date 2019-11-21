using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingChallange.Repositories.Patient.EFCore;
using CodingChallange.Shared.Models.Patient;
using CodingChallange.Shared.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingChallange.Repositories.Patient.Tests
{
    [TestClass]
    public class PatientRepositroyTests
    {
        private DbContextOptionsBuilder<PatientDbContext> dbOptionsBuilder;

        [TestInitialize]
        public void Initialisze() 
        {
            this.dbOptionsBuilder = new DbContextOptionsBuilder<PatientDbContext>()
              .UseInMemoryDatabase(databaseName: "Patients");
         
        }

        [TestMethod]
        public async Task PatientRepositroyGetPatientByIdNullTest()
        {

            using (var context = new PatientDbContext(dbOptionsBuilder.Options))
            {
                var repo = new PatientRepository(context);
                var result = await repo.GetPatientByIdAsync(Guid.Empty);

                Assert.IsNull(result);

            }
        }

        [TestMethod]
        public async Task PatientRepositroyGetPatientByIdSuccessTest()
        {
            var testData = TestDataGenerateHelper.createPatientModelData();
            using (var context = new PatientDbContext(dbOptionsBuilder.Options))
            {
                context.Patients.AddRange(testData);
                context.SaveChanges();

                var repo = new PatientRepository(context);
                var result = await repo.GetPatientByIdAsync(testData[0].Id);

                Assert.IsNotNull(result);

            }
        }

        [TestMethod]
        public void PatientRepositroyGetQueryablePatientTest()
        {
            var testData = TestDataGenerateHelper.createPatientModelData();
            using (var context = new PatientDbContext(dbOptionsBuilder.Options))
            {
                context.Patients.AddRange(testData);
                context.SaveChanges();
                var repo = new PatientRepository(context);
                var result = repo.GetQueryablePatient();

                Assert.IsNotNull(result);
               
            }
        }

        [TestMethod]
        public async Task PatientRepositroyAddPatientTest()
        {

            var testModel = new PatientModel()
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Hu",
                Gender = Gender.male,
                DateOfBirth = new DateTime(1992, 2, 14),
                Email = "test@test.com",
                Phone = "0425789632",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            using (var context = new PatientDbContext(dbOptionsBuilder.Options))
            {
                var dbContext = new PatientDbContext(dbOptionsBuilder.Options);
                var repo = new PatientRepository(dbContext);
                var result = await repo.AddPatientAsync(testModel);
                repo.SaveChanges();
                Assert.AreNotEqual(0, context.Patients.Select(q=> q.Id == testModel.Id).Count());

            }
        }

        [TestMethod]
        public async Task PatientRepositroyUpdatePatientAsyncNullTest()
        {

            var testModel = new PatientModel()
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Hu",
                Gender = Gender.male,
                DateOfBirth = new DateTime(1992, 2, 14),
                Email = "test@test.com",
                Phone = "0425789632",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            using (var context = new PatientDbContext(dbOptionsBuilder.Options))
            {
                context.Patients.Add(testModel);
                context.SaveChanges();
                var repo = new PatientRepository(context);
                var result = await repo.UpdatePatientAsync(new PatientModel() { Id = new Guid() });

                Assert.IsNull(result);
            }
        }

        [TestMethod]
        public async Task PatientRepositroyUpdatePatientAsyncSuccessTest()
        {
            var testModel = new PatientModel()
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Hu",
                Gender = Gender.male,
                DateOfBirth = new DateTime(1992, 2, 14),
                Email = "test@test.com",
                Phone = "0425789632",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            var expected = new PatientModel()
            {
                Id = testModel.Id,
                FirstName = "Tee",
                LastName = "Hu",
                Gender = Gender.male,
                DateOfBirth = new DateTime(1992, 2, 14),
                Email = "test@test.com",
                Phone = "0425789632",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };
            using (var context = new PatientDbContext(dbOptionsBuilder.Options))
            {
                context.Patients.Add(testModel);
                context.SaveChanges();
                var entity = context.Patients.Single(p => p.Id == testModel.Id); //To Avoid tracking error
                context.Entry(entity).State = EntityState.Detached;
                var repo = new PatientRepository(context);
                var result = await repo.UpdatePatientAsync(
                new PatientModel() { 
                    Id = testModel.Id,
                    FirstName = "Tee",
                    LastName = "Hu",
                    Gender = Gender.male,
                    DateOfBirth = new DateTime(1992, 2, 14),
                    Email = "test@test.com",
                    Phone = "0425789632",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                });
                context.SaveChanges();
                Assert.AreEqual("Tee", result.FirstName);
            }
        }


    }
}
