﻿using Bot.Database.Entities;
using Bot.Database.Interfaces;
using Bot.Database.Tests.Helper;
using Bot.Tests;
using Bot.Tools.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bot.Database.Tests {
  [TestClass]
  public class DatabaseServiceTests {

    [TestMethod]
    public void DatabaseServiceAddingIncompleteEntity_Always_ThrowsForeignKeyException() {
      var contextService = DatabaseHelper.GetContainerWithRecreatedAndIsolatedDatabase().GetInstance<IQueryCommandService<IBotDbContext>>();

      var punishedUser = new PunishedUser {
        Count = 1,
        AutoPunishmentId = 1,
        UserId = 1,
      };

      var exception = TestHelper.AssertCatch<DbUpdateException>(() => contextService.Command(db => db.PunishedUsers.Add(punishedUser)));

      Assert.AreEqual("SQLite Error 19: 'FOREIGN KEY constraint failed'.", exception.InnerException.Message);
    }

  }
}
