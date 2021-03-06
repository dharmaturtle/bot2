﻿using Bot.Models.Interfaces;
using Bot.Models.Websockets;

namespace Bot.Models.Sendable {
  public class SendablePardon : ISendable<Pardon> {

    public SendablePardon(IUser target) {
      Transmission = new Pardon(target);
    }

    public Pardon Transmission { get; }
    public IUser Target => Transmission.Target;
    public TResult Accept<TResult>(ISendableVisitor<TResult> visitor) => visitor.Visit(this);
    public IDggJson Json => new Websockets.SendablePardon(Target.Nick);
    public string Twitch => $".unban {Target}";
    public override string ToString() => $"Pardoned {Target})";
  }
}
