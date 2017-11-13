using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.DataServices.Contracts;
using System.Collections.Generic;
using DataflowICB.Database.Models;
using System.Data.Entity;
using Dataflow.DataServices;

namespace DataflowICB.UnitTests.DataServices.CustomUserServices
{
    [TestClass]
    public class GetAllUsers_Should
    {
        [TestMethod]
        public void CallUsersOnce()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();

            string userId = "test";
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
            userServices.GetAllUsers();

            //Assert
            dbContextMock.Verify(d => d.Users, Times.Once());
        }
    }
}
