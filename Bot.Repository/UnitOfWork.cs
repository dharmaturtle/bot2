﻿using Bot.Database.Interfaces;
using Bot.Repository.Interfaces;

namespace Bot.Repository {
  public class UnitOfWork : IUnitOfWork {
    private readonly IBotDbContext _context;

    public UnitOfWork(IBotDbContext context) {
      _context = context;
      StateIntegers = new StateIntegerRepository(_context.StateIntegers);
      AutoPunishments = new AutoPunishmentRepository(_context.AutoPunishments);
      PunishedUsers = new PunishedUserRepository(_context.PunishedUsers);
    }

    public IStateIntegerRepository StateIntegers { get; }
    public IAutoPunishmentRepository AutoPunishments { get; }
    public IPunishedUserRepository PunishedUsers { get; }

    public void Dispose() => _context.Dispose();

    public int SaveChanges() => _context.SaveChanges();
  }
}