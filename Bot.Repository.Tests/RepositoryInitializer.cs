﻿using Bot.Database.Entities;
using Bot.Database.Interfaces;
using Bot.Database.Tests.Helper;
using Bot.Repository.Interfaces;
using Bot.Tools.Interfaces;

namespace Bot.Repository.Tests {
  public class RepositoryInitializer {
    private readonly IQueryCommandService<IBotDbContext> _queryCommandService;
    private readonly DatabaseInitializer _databaseInitializer;

    public RepositoryInitializer(IQueryCommandService<IBotDbContext> queryCommandService) {
      _queryCommandService = queryCommandService;
      _databaseInitializer = new DatabaseInitializer(_queryCommandService);
    }

    private void AddMasterData() {
      _queryCommandService.Command(context => {
        context.StateIntegers.Add(new StateInteger(nameof(IStateIntegerRepository.LatestStreamOnTime), 0));
        context.StateIntegers.Add(new StateInteger(nameof(IStateIntegerRepository.LatestStreamOffTime), 0));
        context.StateIntegers.Add(new StateInteger(nameof(IStateIntegerRepository.DeathCount), 0));
      });
    }

    public void RecreateWithMasterData() {
      _databaseInitializer.Recreate();
      AddMasterData();
    }

    public void EnsureDeleted() =>
      _databaseInitializer.EnsureDeleted();
  }
}
