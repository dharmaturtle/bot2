﻿using System;
using System.Diagnostics;

namespace Bot.Models {
  [DebuggerDisplay("From:{Sender.Nick} Saying:{Text}")]
  public class ReceivedPublicMessage : ReceivedMessage {
    public ReceivedPublicMessage(string text) : base(text) {
      Sender = new User("SampleUser");
      Timestamp = DateTime.UtcNow;
    }

  }
}
