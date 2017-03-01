﻿using Bot.Models;
using Bot.Models.Interfaces;
using Bot.Models.Snapshot;

namespace Bot.Logic.ReceivedVisitor {
  public class ReceivedVisitor : IReceivedVisitor<DelegatedSnapshotFactory> {
    public DelegatedSnapshotFactory Visit(IReceived<Civilian, PublicMessage> message) =>
      new DelegatedSnapshotFactory(context => new PublicMessageFromCivilianSnapshot(message, context));

    public DelegatedSnapshotFactory Visit(IReceived<Moderator, PublicMessage> message) =>
      new DelegatedSnapshotFactory(context => new PublicMessageFromModSnapshot(message, context));

    public DelegatedSnapshotFactory Visit(IReceived<Moderator, ErrorMessage> message) =>
      new DelegatedSnapshotFactory(context => new ErrorSnapshot(message, context));

    public DelegatedSnapshotFactory Visit(IReceived<Moderator, Pardon> pardon) =>
      new DelegatedSnapshotFactory(context => new PardonSnapshot(pardon, context));

  }
}