﻿using System.Collections.Generic;
using System.Linq;
using Bot.Database;
using Bot.Database.Entities;
using Bot.Models;
using Bot.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bot.Repository.Tests {
  [TestClass]
  public class AutoPunishmentRepositoryTests {

    [TestMethod]
    public void ReadWriteAutoPunishment() {
      var container = RepositoryHelper.GetContainerWithInitializedAndIsolatedRepository();

      var id = TestHelper.RandomInt();
      var term = TestHelper.RandomString();
      var type = TestHelper.RandomAutoPunishmentType();
      var duration = TestHelper.RandomInt();
      var nick = TestHelper.RandomString();
      var count = TestHelper.RandomInt();
      var autoPunishmentEntity = new AutoPunishmentEntity {
        Id = id,
        Term = term,
        Type = type,
        Duration = duration,
      };
      var punishedUsersEntity = new List<PunishedUserEntity> {
        new PunishedUserEntity { Nick = nick, Count = count, AutoPunishmentEntity = autoPunishmentEntity }
      };
      autoPunishmentEntity.PunishedUsers = punishedUsersEntity;

      using (var context = container.GetInstance<BotDbContext>()) {
        var autoPunishmentRepository = new AutoPunishmentRepository(context.AutoPunishments);
        autoPunishmentRepository.Add(new AutoPunishment(autoPunishmentEntity));
        context.SaveChanges();
      }

      var testRead = new List<AutoPunishment>();
      using (var context = container.GetInstance<BotDbContext>()) {
        var userRepository = new AutoPunishmentRepository(context.AutoPunishments);
        testRead.AddRange(userRepository.GetAllMutedString());
        testRead.AddRange(userRepository.GetAllBannedString());
        testRead.AddRange(userRepository.GetAllMutedRegex());
        testRead.AddRange(userRepository.GetAllBannedRegex());
      }
      var dbAutoPunishment = testRead.Single();

      Assert.AreEqual(dbAutoPunishment.Id, id);
      Assert.AreEqual(dbAutoPunishment.Term, term);
      Assert.AreEqual(dbAutoPunishment.Type, type);
      Assert.AreEqual(dbAutoPunishment.Duration.TotalSeconds, duration);
      Assert.AreEqual(dbAutoPunishment.PunishedUsers.Single().Count, count);
      Assert.AreEqual(dbAutoPunishment.PunishedUsers.Single().Nick, nick);
    }

    [TestMethod]
    public void ReadWriteUpdateAutoPunishment() {
      var container = RepositoryHelper.GetContainerWithInitializedAndIsolatedRepository();

      var id = TestHelper.RandomInt();
      var term = TestHelper.RandomString();
      var type = TestHelper.RandomAutoPunishmentType();
      var duration = TestHelper.RandomInt();
      var nick = TestHelper.RandomString();
      var count = TestHelper.RandomInt();
      var autoPunishmentEntity = new AutoPunishmentEntity {
        Id = id,
        Term = term,
        Type = type,
        Duration = duration,
      };
      var punishedUsersEntity = new List<PunishedUserEntity> {
        new PunishedUserEntity { Nick = nick, Count = count, AutoPunishmentEntity = autoPunishmentEntity }
      };
      autoPunishmentEntity.PunishedUsers = punishedUsersEntity;
      var autoPunishment = new AutoPunishment(autoPunishmentEntity);

      using (var context = container.GetInstance<BotDbContext>()) {
        var autoPunishmentRepository = new AutoPunishmentRepository(context.AutoPunishments);
        autoPunishmentRepository.Add(autoPunishment);
        context.SaveChanges();
      }

      autoPunishmentEntity.Id = TestHelper.RandomInt();
      autoPunishmentEntity.Term = TestHelper.RandomString();
      autoPunishmentEntity.Type = TestHelper.RandomAutoPunishmentType();
      autoPunishmentEntity.Duration = TestHelper.RandomInt();
      nick = TestHelper.RandomString();
      count = TestHelper.RandomInt();
      autoPunishmentEntity = new AutoPunishmentEntity {
        Id = id,
        Term = term,
        Type = type,
        Duration = duration,
      };
      punishedUsersEntity = new List<PunishedUserEntity> {
        new PunishedUserEntity { Nick = nick, Count = count, AutoPunishmentEntity = autoPunishmentEntity }
      };
      autoPunishmentEntity.PunishedUsers = punishedUsersEntity;

      using (var context = container.GetInstance<BotDbContext>()) {
        var autoPunishmentRepository = new AutoPunishmentRepository(context.AutoPunishments);
        autoPunishmentRepository.Update(new AutoPunishment(autoPunishmentEntity));
        context.SaveChanges();
      }

      var testRead = new List<AutoPunishment>();
      using (var context = container.GetInstance<BotDbContext>()) {
        var userRepository = new AutoPunishmentRepository(context.AutoPunishments);
        testRead.AddRange(userRepository.GetAllMutedString());
        testRead.AddRange(userRepository.GetAllBannedString());
        testRead.AddRange(userRepository.GetAllMutedRegex());
        testRead.AddRange(userRepository.GetAllBannedRegex());
      }
      var dbAutoPunishment = testRead.Single();

      Assert.AreEqual(dbAutoPunishment.Id, id);
      Assert.AreEqual(dbAutoPunishment.Term, term);
      Assert.AreEqual(dbAutoPunishment.Type, type);
      Assert.AreEqual(dbAutoPunishment.Duration.TotalSeconds, duration);
      Assert.AreEqual(dbAutoPunishment.PunishedUsers.Single().Count, count);
      Assert.AreEqual(dbAutoPunishment.PunishedUsers.Single().Nick, nick);
    }

  }
}
