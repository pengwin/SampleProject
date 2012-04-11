using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleProject.Authentication.Models;
using SampleProject.Models.UserModels;

namespace SampleProjectTest
{
    [TestClass]
    public class UserInfoTest
    {
        [TestMethod]
        public void ToString_WithoutRoles_Test()
        {
            var target = new UserInfo{UserId = 1,Username = "TestUser", FullName = "Test User", Email = "test@test.test"};

            var expected = "1|TestUser|test@test.test|Test User|";
            
            var actual = target.ToString();

            Assert.AreEqual(expected,actual);
        }

        [TestMethod]
        public void ToString_WithRoles_Test()
        {
            var target = new UserInfo{UserId = 1,Username = "TestUser", FullName = "Test User", Email = "test@test.test"};
            target.Roles = new []{"Admin", "Guest"};

            var expected = "1|TestUser|test@test.test|Test User|Admin;Guest;";

            var actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FromString_WithoutRoles_Test()
        {
            var user = "1|TestUser|test@test.test|Test User|";

            var expected = new UserInfo { UserId = 1, Username = "TestUser", FullName = "Test User", Email = "test@test.test" };

            var actual = UserInfo.FromString(user);

            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(0,actual.Roles.Length);
        }

        [TestMethod]
        public void FromString_WithRoles_Test()
        {

            var user = "1|TestUser|test@test.test|Test User|Admin;Guest;";

            var expected = new UserInfo { UserId = 1, Username = "TestUser", FullName = "Test User", Email = "test@test.test" };
            expected.Roles = new[] { "Admin", "Guest" };

            var actual = UserInfo.FromString(user);

            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(2, actual.Roles.Length);
            Assert.AreEqual(expected.Roles[0], actual.Roles[0]);
            Assert.AreEqual(expected.Roles[1], actual.Roles[1]);
        }

        [TestMethod]
        public void FromUser_WithoutRoles_Test()
        {
            var user = new User{ UserId = 1, Username = "TestUser", FullName = "Test User", Email = "test@test.test" };
            //user.Roles = { "Admin", "Guest" };

            var expected = new UserInfo { UserId = 1, Username = "TestUser", FullName = "Test User", Email = "test@test.test" };

            var actual = UserInfo.FromUser(user);

            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(0, actual.Roles.Length);
        }

        [TestMethod]
        public void FromUser_WithRoles_Test()
        {
            var user = new User { UserId = 1, Username = "TestUser", FullName = "Test User", Email = "test@test.test" };
            user.Roles = new List<Role> { new Role { RoleName = "Admin" }, new Role { RoleName = "Guest" } };

            var expected = new UserInfo { UserId = 1, Username = "TestUser", FullName = "Test User", Email = "test@test.test" };
            expected.Roles = new[] { "Admin", "Guest" };

            var actual = UserInfo.FromUser(user);

            Assert.AreEqual(expected.UserId, actual.UserId);
            Assert.AreEqual(expected.Username, actual.Username);
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.FullName, actual.FullName);
            Assert.AreEqual(2, actual.Roles.Length);
            Assert.AreEqual(expected.Roles[0], actual.Roles[0]);
            Assert.AreEqual(expected.Roles[1], actual.Roles[1]);
        }

    }
}
