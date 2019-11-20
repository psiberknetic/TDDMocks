using ChewCrew.Helpers;
using ChewCrew.Models.Identity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace ChewCrewTests
{
    [TestClass]
    public class UserAccessTests
    {
        [TestMethod]
        public void CanAddRestaurant_UserIsSuperAdmin_ReturnsTrue()
        {
            var principal = CreateMockPrinciple();
            AddRole(principal, ChewCrewRoles.SuperAdmin);
            var userAccess = new UserAccess(principal.Object);

            userAccess.CanAddRestaurant().Should().BeTrue();
        }

        [TestMethod]
        public void CanAddRestaurant_UserIsGroupAdmin_ReturnsTrue()
        {
            var principal = CreateMockPrinciple();
            AddRole(principal, ChewCrewRoles.GroupAdmin);
            var userAccess = new UserAccess(principal.Object);

            userAccess.CanAddRestaurant().Should().BeTrue();
        }

        [TestMethod]
        public void CanAddRestaurant_UserIsGroupAndSuperAdmin_ReturnsTrue()
        {
            var principal = CreateMockPrinciple();
            AddRole(principal, ChewCrewRoles.GroupAdmin);
            AddRole(principal, ChewCrewRoles.SuperAdmin);
            var userAccess = new UserAccess(principal.Object);

            userAccess.CanAddRestaurant().Should().BeTrue();
        }

        [TestMethod]
        public void CanAddRestaurant_DefaultUser_ReturnsFalse()
        {
            var principal = CreateMockPrinciple();
            var userAccess = new UserAccess(principal.Object);

            userAccess.CanAddRestaurant().Should().BeFalse();
        }

        // Testing private testing Mock methods

        [TestMethod]
        public void AddRole_RoleHasBeenAdded_ReturnsTrue()
        {
            var principal = CreateMockPrinciple();
            principal = AddRole(principal, ChewCrewRoles.SuperAdmin);
            principal.Object.IsInRole(ChewCrewRoles.SuperAdmin).Should().BeTrue();
        }

        [TestMethod]
        public void AddRole_RoleHasNotBeenAdded_ReturnsFalse()
        {
            var principal = CreateMockPrinciple();
            principal.Object.IsInRole(ChewCrewRoles.SuperAdmin).Should().BeFalse();
        }

        [TestMethod]
        public void RemoveRole_RoleHasBeenAddedThenRemoved_ReturnsFalse()
        {
            var principal = CreateMockPrinciple();
            principal = AddRole(principal, ChewCrewRoles.SuperAdmin);
            principal = RemoveRole(principal, ChewCrewRoles.SuperAdmin);
            principal.Object.IsInRole(ChewCrewRoles.SuperAdmin).Should().BeFalse();
        }

        private Mock<IPrincipal> CreateMockPrinciple()
        {
            var principal = new Mock<IPrincipal>();
            principal.Setup(p => p.IsInRole(It.IsAny<string>())).Returns(false);

            return principal;
        }

        private Mock<IPrincipal> AddRole(Mock<IPrincipal> principal, string role)
        {
            principal.Setup(p => p.IsInRole(role)).Returns(true);

            return principal;
        }

        private Mock<IPrincipal> RemoveRole(Mock<IPrincipal> principal, string role)
        {
            principal.Setup(p => p.IsInRole(role)).Returns(false);

            return principal;
        }
    }
}
