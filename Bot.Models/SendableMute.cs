﻿using System;
using System.Diagnostics;
using Bot.Models.Contracts;

namespace Bot.Models {
  [DebuggerDisplay("Muted {Target} for {Duration.TotalMinutes}m for: {Reason}")]
  public class SendableMute : Mute, ISendable {
    public SendableMute(IUser target, TimeSpan duration) : base(target, duration) { }
    public SendableMute(IUser target, TimeSpan duration, string reason) : base(target, duration, reason) { }
  }
}
