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
        private static string _SuperAdmin = "SuperAdmin";
        private static string _HasAllergy = "HasAllergy";
        private static string _GroupAdmin = "GroupAdmin";

        [TestMethod]
        public void CanAddRestaurant_UserIsGroupAdmin_ReturnsTrue()
        {

        }

        // Testing private testing Mock methods

        [TestMethod]
        public void AddRole_RoleHasBeenAdded_ReturnsTrue()
        {
            var principal = CreateMockPrinciple();
            principal = AddRole(principal, _SuperAdmin);
            principal.Object.IsInRole(_SuperAdmin).Should().BeTrue();
        }

        [TestMethod]
        public void AddRole_RoleHasNotBeenAdded_ReturnsFalse()
        {
            var principal = CreateMockPrinciple();
            principal.Object.IsInRole(_SuperAdmin).Should().BeFalse();
        }

        [TestMethod]
        public void RemoveRole_RoleHasBeenAddedThenRemoved_ReturnsFalse()
        {
            var principal = CreateMockPrinciple();
            principal = AddRole(principal, _SuperAdmin);
            principal = RemoveRole(principal, _SuperAdmin);
            principal.Object.IsInRole(_SuperAdmin).Should().BeFalse();
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
