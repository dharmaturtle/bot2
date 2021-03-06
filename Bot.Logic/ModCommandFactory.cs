﻿using System.Collections.Generic;
using System.Linq;
using Bot.Logic.Interfaces;
using Bot.Models;
using Bot.Models.Interfaces;
using Bot.Models.Sendable;
using Bot.Tools;
using Bot.Tools.Interfaces;

namespace Bot.Logic {
  public class ModCommandFactory : BaseSendableFactory<Moderator, IMessage> {
    private readonly IModCommandLogic _modCommandLogic;
    private readonly IModCommandRepositoryLogic _modCommandRepositoryLogic;
    private readonly IModCommandRegex _modCommandRegex;
    private readonly IModCommandParser _modCommandParser;
    private readonly IFactory<IReceived<Moderator, IMessage>, Nuke> _nukeFactory;

    public ModCommandFactory(
      IModCommandLogic modCommandLogic, 
      IModCommandRepositoryLogic modCommandRepositoryLogic, 
      IModCommandRegex modCommandRegex, 
      IModCommandParser modCommandParser, 
      IFactory<IReceived<Moderator, IMessage>, Nuke> nukeFactory) {
      _modCommandLogic = modCommandLogic;
      _modCommandRepositoryLogic = modCommandRepositoryLogic;
      _modCommandRegex = modCommandRegex;
      _modCommandParser = modCommandParser;
      _nukeFactory = nukeFactory;
    }

    public override IReadOnlyList<ISendable<ITransmittable>> Create(ISnapshot<Moderator, IMessage> snapshot) {
      var context = snapshot.Context;
      var message = snapshot.Latest;
      if (message.IsMatch(_modCommandRegex.Sing))
        return _modCommandLogic.Sing().Wrap().ToList();
      if (message.StartsWith("!long"))
        return _modCommandLogic.Long(context).Wrap().ToList();
      if (message.IsMatch(_modCommandRegex.Nuke))
        return _modCommandLogic.Nuke(context, _nukeFactory.Create(message));
      if (message.IsMatch(_modCommandRegex.RegexNuke))
        return _modCommandLogic.Nuke(context, _nukeFactory.Create(message));
      if (message.IsMatch(_modCommandRegex.Aegis))
        return _modCommandLogic.Aegis(context);
      if (message.IsMatch(_modCommandRegex.AddCommand)) {
        var tuple = _modCommandParser.AddCommand(message.Transmission.Text);
        return _modCommandRepositoryLogic.AddCommand(tuple.Item1, tuple.Item2);
      }
      if (message.IsMatch(_modCommandRegex.DelCommand)) {
        var commandToDelete = _modCommandParser.DelCommand(message.Transmission.Text);
        return _modCommandRepositoryLogic.DelCommand(commandToDelete);
      }
      if (message.IsMatch(_modCommandRegex.Stalk)) {
        var user = _modCommandParser.Stalk(message.Transmission.Text);
        return _modCommandLogic.Stalk(user);
      }
      if (message.IsMatch(_modCommandRegex.Ipban)) {
        var ipbanTuple = _modCommandParser.Ipban(message.Transmission.Text);
        return _modCommandLogic.Ipban(ipbanTuple.Item1, ipbanTuple.Item2);
      }
      if (message.IsMatch(_modCommandRegex.Ban)) {
        var banTuple = _modCommandParser.Ban(message.Transmission.Text);
        return _modCommandLogic.Ban(banTuple.Item1, banTuple.Item2);
      }
      if (message.IsMatch(_modCommandRegex.Mute)) {
        var muteTuple = _modCommandParser.Mute(message.Transmission.Text);
        return _modCommandLogic.Mute(muteTuple.Item1, muteTuple.Item2);
      }
      if (message.IsMatch(_modCommandRegex.Pardon)) {
        var nick = _modCommandParser.Pardon(message.Transmission.Text);
        return _modCommandLogic.Pardon(nick);
      }
      if (message.IsMatch(_modCommandRegex.AddMute)) {
        var muteDuration = _modCommandParser.AddMute(message.Transmission.Text);
        return _modCommandRepositoryLogic.AddMute(muteDuration.Item1, muteDuration.Item2);
      }
      if (message.IsMatch(_modCommandRegex.AddMuteRegex)) {
        var muteDuration = _modCommandParser.AddMuteRegex(message.Transmission.Text);
        return _modCommandRepositoryLogic.AddMuteRegex(muteDuration.Item1, muteDuration.Item2);
      }
      if (message.IsMatch(_modCommandRegex.AddBan)) {
        var banDuration = _modCommandParser.AddBan(message.Transmission.Text);
        return _modCommandRepositoryLogic.AddBan(banDuration.Item1, banDuration.Item2);
      }
      if (message.IsMatch(_modCommandRegex.AddBanRegex)) {
        var banDuration = _modCommandParser.AddBanRegex(message.Transmission.Text);
        return _modCommandRepositoryLogic.AddBanRegex(banDuration.Item1, banDuration.Item2);
      }
      if (message.IsMatch(_modCommandRegex.DelMute)) {
        var mutedPhrase = _modCommandParser.DelMute(message.Transmission.Text);
        return _modCommandRepositoryLogic.DelMute(mutedPhrase);
      }
      if (message.IsMatch(_modCommandRegex.DelMuteRegex)) {
        var mutedPhrase = _modCommandParser.DelMuteRegex(message.Transmission.Text);
        return _modCommandRepositoryLogic.DelMuteRegex(mutedPhrase);
      }
      if (message.IsMatch(_modCommandRegex.DelBan)) {
        var bannedPhrase = _modCommandParser.DelBan(message.Transmission.Text);
        return _modCommandRepositoryLogic.DelBan(bannedPhrase);
      }
      if (message.IsMatch(_modCommandRegex.DelBanRegex)) {
        var bannedPhrase = _modCommandParser.DelBanRegex(message.Transmission.Text);
        return _modCommandRepositoryLogic.DelBanRegex(bannedPhrase);
      }

      return new List<ISendable<ITransmittable>>();
    }

    public override IReadOnlyList<ISendable<ITransmittable>> OnErrorCreate => new SendableError($"An error occured in the {nameof(ModCommandFactory)}.").Wrap().ToList();
  }
}
