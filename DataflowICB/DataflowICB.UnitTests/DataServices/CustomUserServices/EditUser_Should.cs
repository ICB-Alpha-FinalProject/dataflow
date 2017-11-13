﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataflowICB.Database;
using Moq;
using Dataflow.DataServices.Contracts;
using Dataflow.DataServices;
using Dataflow.DataServices.Models;
using System.Linq;
using DataflowICB.Database.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace DataflowICB.UnitTests.DataServices.CustomUserServices
{
    [TestClass]
    public class EditUser_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenParameterIsNull()
        {
            //Arrange
            var dbContextMock = new Mock<ApplicationDbContext>();
            var userServices = new CustomUserServicesICB(dbContextMock.Object);

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => userServices.EditUser(null));
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
            userServices.EditUser(userMock.Object);

            //Assert
            dbContextMock.Verify(d => d.Users, Times.Once());
        }
    }
}