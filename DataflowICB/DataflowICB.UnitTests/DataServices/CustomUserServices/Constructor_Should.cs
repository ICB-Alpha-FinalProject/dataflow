using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Dataflow.DataServices;
using Dataflow.DataServices.Contracts;
using DataflowICB.Database;

namespace DataflowICB.UnitTests.DataServices.UserServices
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void CreateObjectOfTypeApplicationDbContext_WhenParamsAreValid()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            //Act
            var userServices = new CustomUserServicesICB(dbContextMock.Object);

            //Assert
            Assert.IsInstanceOfType(userServices, typeof(IUserServices));
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenApplicationDbContextIsNull()
        {
            //Arrange && Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CustomUserServicesICB(null));
        }
    }
}
