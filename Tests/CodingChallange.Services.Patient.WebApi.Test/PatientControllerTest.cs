using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Threading.Tasks;
using CodingChallange.Repositories.Patient.EFCore;
using CodingChallange.Shared.Models.Patient;
using CodingChallange.Shared.Patient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Moq;
using CodingChallange.Repositories.Patient;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using CodingChallange.Services.Patient.WebApi.Controllers;
using CodingChallange.Services.Patient.WebApi.Mappers;
using Sieve.Models;
using CodingChallange.Repositories.Patient.Tests;
using CodingChallange.Shared.Models.Pagination;
using CodingChallange.Shared.ViewModels.Patient;

namespace CodingChallange.Services.Patient.WebApi.Test
{
    [TestClass]
    public class PatientControllerTest
    {
        private Mock<IPatientManager> _mockPatientManager;
        private Mock<ILogger<PatientsController>> _mockLogger;
        private IMapper _mapper;

       [TestInitialize]
        public void Initialisze()
        {
            _mockPatientManager = new Mock<IPatientManager>();
            _mockLogger = new Mock<ILogger<PatientsController>>();

            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<PatientModelAndViewModelMappingProfile>();
            });

            _mapper = configuration.CreateMapper();

        }

        [TestMethod]
        public async Task PatientControllerGetPatientNullPageSettingTest()
        {
            var testData = TestDataGenerateHelper.createPatientModelData();
            _mockPatientManager.Setup(r => r.GetAllPatients()).ReturnsAsync(testData);

            var patientController = new PatientsController(_mockLogger.Object, _mockPatientManager.Object, _mapper);

            var sieveModel = new SieveModel();
            sieveModel.Page = null;
            sieveModel.PageSize = null;

            dynamic result = await patientController.GetPatient(sieveModel);

            Assert.AreEqual(200, result.StatusCode);
        }


        [TestMethod]
        public async Task PatientControllerGetPatientSuccessTest()
        {
            var testData = new PagedResult<PatientModel>() {
                PageNumber = 1,
                PageSize = 10,
                TotalNumberOfPages = 11,
                TotalNumberOfRecords =12,
                Results = TestDataGenerateHelper.createPatientModelData()
            };
            var sieveModel = new SieveModel();
            sieveModel.Page = 1;
            sieveModel.PageSize = 10;

            _mockPatientManager.Setup(r => r.GetPagedPatientAsync(sieveModel)).ReturnsAsync(testData);
            var patientController = new PatientsController(_mockLogger.Object, _mockPatientManager.Object, _mapper);
            dynamic result = await patientController.GetPatient(sieveModel);

            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod] 
        public async Task PatientControllerGetPatientByIdSuccessTest()
        {
            var testModel = new PatientModel()
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Hu",
                Gender = Gender.male,
                DateOfBirth = new DateTime(1992, 2, 14),
                Email = "test@test.com",
                Phone = "0425789632",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _mockPatientManager.Setup(r => r.GetPatientByIdAsync(testModel.Id)).ReturnsAsync(testModel);
            
            var patientController = new PatientsController(_mockLogger.Object, _mockPatientManager.Object, _mapper);
            dynamic result = await patientController.GetPatientById(testModel.Id);
            
            Assert.AreEqual(200, result.Result.StatusCode);
            Assert.IsNotNull(result.Result.Value);

        }


        [TestMethod]
        public async Task PatientControllerPostSuccessTest()
        {
            var testRequestModel = new PatientRequestViewModel()
            {
                FirstName = "test",
                LastNanme = "test",
                Gender = Gender.male,
                DateOfBirth = new DateTime(2019, 1, 1),
                Email = "test@test.com",
                Phone = "04240521321",
                IsActive = false
            };
            var model = _mapper.Map<PatientModel>(testRequestModel);
            _mockPatientManager.Setup(r => r.AddNewPatientAsync(model)).ReturnsAsync(model);

            var patientController = new PatientsController(_mockLogger.Object, _mockPatientManager.Object, _mapper);
            dynamic result = await patientController.Post(testRequestModel);

            Assert.AreEqual(201, result.Result.StatusCode);

        }

    }
}
