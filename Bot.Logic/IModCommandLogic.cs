﻿using System.Collections.Generic;
using Bot.Models;
using Bot.Models.Contracts;

namespace Bot.Logic {
  public interface IModCommandLogic {
    ISendable Long(IReadOnlyList<IReceived<IUser, ITransmittable>> context);
    ISendable Sing();
    IReadOnlyList<ISendable> Nuke(IReadOnlyList<IReceived<IUser, ITransmittable>> context, IReceivedNuke nuke);
    IReadOnlyList<ISendable> Aegis(IReadOnlyList<IReceived<IUser, ITransmittable>> context);
  }
}
