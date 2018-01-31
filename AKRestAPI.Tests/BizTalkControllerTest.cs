using System;
using System.Xml;
using Xunit;
using AKRestAPI;
using AKRestAPI.Models;
using AKRestAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AKWebAPI.Tests
{
    public class BizTalkControllerTests
    {
        [Fact]
        public void ConnectionTest()
        {
            // Arrange
            var bizTalkController = new BizTalkController();

            // Act
            var result = bizTalkController.MCI();

            //Assert
            var expected = typeof(String);
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.IsType(expected, contentResult.Content);          
        }
    }
}