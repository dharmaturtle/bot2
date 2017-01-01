﻿using System.Collections.Generic;
using Bot.Logic.Contracts;
using Bot.Models;
using Bot.Models.Contracts;

namespace Bot.Logic {
  public class ScanForBans : IScanForBans {

    public IReadOnlyList<ISendable> Scan(IContextualized contextualized) {
      var outbox = new List<ISendable>();
      var message = contextualized.First as IPublicMessageReceived;
      if (message != null) {
        if (message.Text.Contains("banplox")) {
          outbox.Add(new PublicMessage($"{message.Sender.Nick} banned for saying {message.Text}"));
        }
      }
      return outbox;
    }
  }
}
