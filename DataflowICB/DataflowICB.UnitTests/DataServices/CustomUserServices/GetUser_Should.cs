using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dataflow.DataServices;
using Moq;
using DataflowICB.Database;
using Dataflow.DataServices.Contracts;
using System.Collections.Generic;
using DataflowICB.Database.Models;
using System.Data.Entity;

namespace DataflowICB.UnitTests.DataServices.CustomUserServices
{
    [TestClass]
    public class GetUser_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userServices = new CustomUserServicesICB(dbContextMock.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => userServices.GetUser(null));
        }

        [TestMethod]
        public void CallUsersOnce_WhenUserIsExistent()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userMock = new Mock<IUserDataModel>();
            string Id = "test";
            userMock.Setup(x => x.Id).Returns(Id);

            string userId = Id;
            string username = "username";
            string email = "username@username.com";
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = username, Id = userId, Email = email }
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            var userServices = new CustomUserServicesICB(dbContextMock.Object);

            //Act
            userServices.GetUser(username);

            //Assert
            dbContextMock.Verify(d => d.Users, Times.Once());
        }

        [TestMethod]
        public void ReturnTheUserWithCorrectId()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userMock = new Mock<IUserDataModel>();
            string Id = "test";
            userMock.Setup(x => x.Id).Returns(Id);

            string userId = Id;
            string username = "username";
            string email = "username@username.com";

            string secondUserId = "something else";
            string secondUsername = "username5";
            string secondEmail = "second@email.com";
            List<ApplicationUser> users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = secondUsername, Id = secondUserId, Email = secondEmail },
                new ApplicationUser() { UserName = username, Id = userId, Email = email }             
            };

            var usersSetMock = new Mock<DbSet<ApplicationUser>>().SetupData(users);

            dbContextMock.SetupGet(m => m.Users).Returns(usersSetMock.Object);

            var userServices = new CustomUserServicesICB(dbContextMock.Object);

            //Act
            IUserDataModel returnedUser = userServices.GetUser(username);

            //Assert
            Assert.AreEqual(username, returnedUser.Username);
            Assert.AreEqual(email, returnedUser.Email);
            Assert.AreEqual(Id, returnedUser.Id);
        }
    }
}
