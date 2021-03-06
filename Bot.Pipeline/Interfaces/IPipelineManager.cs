﻿using System;
using System.Collections.Generic;
using Bot.Models.Interfaces;

namespace Bot.Pipeline.Interfaces {
  public interface IPipelineManager {
    void Enqueue(IReceived<IUser, ITransmittable> received);
    void Enqueue(ISendable<ITransmittable> received);
    void Enqueue(IReadOnlyList<ISendable<ITransmittable>> received);
    void Enqueue(string received);
    void SetSender(Action<string> sender);
  }
}
