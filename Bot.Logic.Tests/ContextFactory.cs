﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bot.Models;
using Bot.Models.Contracts;
using Bot.Tools;

namespace Bot.Logic.Tests {
  /// <remarks>
  /// "Context" here differs from IContextualized in that IContextualized's context skips the first message.
  /// This Builder generates the list of IReceived that Contextualized takes in its ctor
  /// </remarks>>
  public class ContextBuilder {
    private readonly DateTime _rootTime = TimeService.UnixEpoch;
    private DateTime _time;

    private readonly IList<IReceived> _nontargets = new List<IReceived>();
    private readonly IList<IReceived> _targets = new List<IReceived>();

    private IReadOnlyList<IUser> _NonTargetUsers => _nontargets.Select(r => r.Sender).ToList();
    private IReadOnlyList<IUser> _TargetUsers => _targets.Select(r => r.Sender).ToList();

    public ContextBuilder() {
      _time = _rootTime;
    }

    public TimeSpan Gap { get; } = TimeSpan.FromMinutes(1);
    public IReadOnlyList<IReceived> GetContext => _targets.Concat(_nontargets).OrderBy(r => r.Timestamp).ToList(); // Is 1 indexed
    public bool IsValid(IReadOnlyList<IUser> targets)
      => targets.OrderBy(u => u.Nick).SequenceEqual(_TargetUsers.OrderBy(u => u.Nick)) && !_NonTargetUsers.Intersect(targets).Any();

    public ContextBuilder TargetedMessage(string message, TimeSpan? timestamp = null)
      => AddReceived(timestamp, t => _targets.Add(new PublicReceivedMessage(message, t)));

    public ContextBuilder PublicMessage(string message, TimeSpan? timestamp = null)
      => AddReceived(timestamp, t => _nontargets.Add(new PublicReceivedMessage(message, t)));

    public ContextBuilder ModMessage(string message, TimeSpan? timestamp = null)
      => AddReceived(timestamp, t => _nontargets.Add(new ModPublicReceivedMessage(message, t)));

    private ContextBuilder AddReceived(TimeSpan? timestamp, Action<DateTime> addReceived) {
      _time = timestamp == null ? _time.Add(Gap) : _rootTime.Add((TimeSpan) timestamp);
      addReceived.Invoke(_time);
      return this;
    }

  }
}