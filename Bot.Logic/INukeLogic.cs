﻿using System.Collections.Generic;
using Bot.Models;
using Bot.Models.Contracts;

namespace Bot.Logic {
  public interface INukeLogic {
    IReadOnlyList<ISendable> Aegis(IReadOnlyList<IReceived<IUser, ITransmittable>> context);
    IReadOnlyList<ISendable> Nuke(IReceivedNuke nuke, IReadOnlyList<IReceived<IUser, ITransmittable>> context);
  }
}
