﻿using System;
using System.Collections.Generic;
using Bot.Models;
using Bot.Models.Contracts;
using Bot.Pipeline.Contracts;

namespace Bot.Pipeline {
  public class ConsoleSender : ISender {
    public void Send(IEnumerable<ISendable> sendables) {
      foreach (var sendable in sendables) {
        if (sendable is SendablePublicMessage) {
          var pm = (SendablePublicMessage) sendable;
          Console.WriteLine($"Sending: {pm.Text}");
        } else {
          Console.WriteLine(sendable);
        }
      }
    }

  }
}
