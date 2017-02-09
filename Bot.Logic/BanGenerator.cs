﻿using System.Collections.Generic;
using Bot.Models;
using Bot.Models.Interfaces;

namespace Bot.Logic {
  public class BanGenerator : BaseSendablesFactory<Civilian, PublicMessage> {

    public override IReadOnlyList<ISendable<ITransmittable>> Create(ISnapshot<Civilian, PublicMessage> snapshot) {
      var outbox = new List<ISendable<ITransmittable>>();
      var message = snapshot.Latest;
      if (message.Transmission.Text.Contains("banplox")) {
        outbox.Add(new SendablePublicMessage($"{message.Sender.Nick} banned for saying {message.Transmission.Text}"));
      }
      return outbox;
    }

    //IReadOnlyCollection?
    //public IReadOnlyList<ISendable> Generate(ISnapshot snapshot) =>
    //  Herpderp(snapshot).ToList();

    //private IEnumerable<ISendable> Herpderp(ISnapshot snapshot) {
    //  var message = snapshot.First as ReceivedMessage;
    //  if (message != null && message.Text.Contains("banplox")) {
    //    yield return new SendablePublicMessage($"{message.Sender.Nick} banned for saying {message.Text}");
    //  }
    //}

  }
}
