﻿using System;
using Bot.Tools.Interfaces;

namespace Bot.Repository.Interfaces {
  public interface IUnitOfWork : IDisposable, ISavable {
    IStateIntegerRepository StateIntegers { get; }
    IAutoPunishmentRepository AutoPunishments { get; }
    IPunishedUserRepository PunishedUsers { get; }
    ICustomCommandRepository CustomCommand { get; }
    IPeriodicMessageRepository PeriodicMessages { get; }
    IInMemoryRepository InMemory { get; }
  }
}
