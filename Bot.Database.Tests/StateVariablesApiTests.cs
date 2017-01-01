﻿using System;
using Bot.Pipeline;
using Bot.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bot.Database.Tests {

  [TestClass]
  public class StateVariablesApiTests {

    [TestMethod]
    public void GetAndSetOnTime() {
      // Arrange
      var consoleLogger = new ConsoleLogger();
      var factory = new ApiFactory(consoleLogger);
      var api = factory.GetStateVariablesApi;
      var expected = DateTime.UtcNow;

      // Act
      api.OnTime = expected;
      var actual = api.OnTime;

      // Assert
      Assert.AreEqual(expected.ToUnixTime(), actual.ToUnixTime()); // Need Unix time to round; 

    }
  }
}