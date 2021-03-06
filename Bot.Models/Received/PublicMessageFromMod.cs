﻿using System;
using Bot.Models.Interfaces;
using Bot.Tools.Interfaces;

namespace Bot.Models.Received {
  public class PublicMessageFromMod : ReceivedPublicMessage<Moderator> {

    public PublicMessageFromMod(string text, ITimeService timeService)
      : base(new Moderator("SampleMod"), text, timeService) { }

    public PublicMessageFromMod(string nick, string text, ITimeService timeService)
      : base(new Moderator(nick), text, timeService) { }

    public PublicMessageFromMod(string text, DateTime timestamp)
      : base(new Moderator(timestamp.ToShortTimeString()), text, timestamp) { }

    public PublicMessageFromMod(string nick, string text, DateTime timestamp)
      : base(new Moderator(nick), text, timestamp) { }

    public override TResult Accept<TResult>(IReceivedVisitor<TResult> visitor) => visitor.Visit(this);
    public override string ToString() => $"{Sender}: {Text}";
  }
}
