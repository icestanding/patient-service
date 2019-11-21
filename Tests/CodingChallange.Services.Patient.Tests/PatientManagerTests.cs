using CodingChallange.Repositories.Patient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CodingChallange.Services.Patient;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sieve.Services;
using System;
using CodingChallange.Shared.Models.Patient;
using CodingChallange.Shared.Patient;

namespace CodingChallange.Services.Patient.Tests
{
    [TestClass]
    public class PatientManagerTests
    {
        private  Mock<IPatientRepository> _mockRepo;
        private  Mock<ILogger<PatientManager>> _mockLogger;
        private  Mock<ISieveProcessor> _mockSieveProcessor;

        [TestInitialize]
        public void Initialise()
        {
            _mockRepo = new Mock<IPatientRepository>();
            _mockLogger = new Mock<ILogger <PatientManager>>();
            _mockSieveProcessor = new Mock<ISieveProcessor>();
        }

        private static PatientModel createSinglePatientModel()
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
            return testModel;
        }


        [TestMethod]
        public async Task PatientManagerGetPatientByIdAsyncNullIdTest()
        {
            var testModel = new PatientModel();
            _mockRepo.Setup(r => r.GetPatientByIdAsync(new Guid())).ReturnsAsync(testModel);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            var result = await patientManager.GetPatientByIdAsync(Guid.Empty);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task PatientManagerGetPatientByIdAsyncSuccessTest()
        {
            var testModel = createSinglePatientModel();

            _mockRepo.Setup(r => r.GetPatientByIdAsync(testModel.Id)).ReturnsAsync(testModel);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            var result = await patientManager.GetPatientByIdAsync(testModel.Id);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public async Task PatientManagerAddNewPatientAsyncSuccessTest()
        {
            var testModel = createSinglePatientModel();

            _mockRepo.Setup(r => r.AddPatientAsync(testModel)).ReturnsAsync(testModel);
            _mockRepo.Setup(r => r.SaveChanges()).Returns(true);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            var result = await patientManager.AddNewPatientAsync(testModel);

            Assert.AreEqual(testModel.Id, result.Id);
        }

        [TestMethod]
        public async Task PatientManagerAddNewPatientAsyncFailedCreationTest()
        {
            var testModel = createSinglePatientModel();

            _mockRepo.Setup(r => r.AddPatientAsync(testModel)).ReturnsAsync((PatientModel)null);
            _mockRepo.Setup(r => r.SaveChanges()).Returns(true);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            try
            {
                var result = await patientManager.AddNewPatientAsync(testModel);
            }
            catch (Exception e) {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }


        public async Task PatientManagerAddNewPatientAsyncFailedSaveToDbTest()
        {
            var testModel = createSinglePatientModel();

            _mockRepo.Setup(r => r.AddPatientAsync(testModel)).ReturnsAsync(testModel);
            _mockRepo.Setup(r => r.SaveChanges()).Returns(false);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            try
            {
                var result = await patientManager.AddNewPatientAsync(testModel);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        public async Task PatientManagerUpdatePatientAsyncSuccessTest()
        {
            var testModel = createSinglePatientModel();

            _mockRepo.Setup(r => r.UpdatePatientAsync(testModel)).ReturnsAsync(testModel);
            _mockRepo.Setup(r => r.SaveChanges()).Returns(true);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            try
            {
                var result = await patientManager.UpdatePatientAsync(testModel);
                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Assert.Fail();
                return;
            }
        }


        public async Task PatientManagerUpdatePatientAsyncFailTest()
        {
            var testModel = createSinglePatientModel();

            _mockRepo.Setup(r => r.UpdatePatientAsync(testModel)).ReturnsAsync(testModel);
            _mockRepo.Setup(r => r.SaveChanges()).Returns(false);

            var patientManager = new PatientManager(_mockLogger.Object, _mockRepo.Object, _mockSieveProcessor.Object);
            try
            {
                var result = await patientManager.UpdatePatientAsync(testModel);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }



    }
}
