﻿using System;
using Bot.Models.Interfaces;

namespace Bot.Models.Sendable {
  public class SendableMute : ISendable<Mute> {

    public SendableMute(Civilian target, TimeSpan duration) {
      Transmission = new Mute(target, duration);
    }

    public SendableMute(Civilian target, TimeSpan duration, string reason) {
      Transmission = new Mute(target, duration, reason);
    }

    public Mute Transmission { get; }
    public IUser Target => Transmission.Target;
    public TimeSpan Duration => Transmission.Duration;
    public string Reason => Transmission.Reason;
    public TResult Accept<TResult>(ISendableVisitor<TResult> visitor) => visitor.Visit(this);
    public object Json => new Websockets.SendableMute(Target.Nick, Duration);
    public override string ToString() => Transmission.ToString();
  }
}
