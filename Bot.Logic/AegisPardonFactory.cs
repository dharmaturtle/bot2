using System.Collections.Generic;
using System.Linq;
using Bot.Logic.Interfaces;
using Bot.Models;
using Bot.Models.Interfaces;
using Bot.Models.Received;
using Bot.Models.Sendable;
using Bot.Tools;
using Bot.Tools.Interfaces;

namespace Bot.Logic {
  public class AegisPardonFactory : NukeAegisBase, IErrorableFactory<IReadOnlyList<IReceived<IUser, ITransmittable>>, IReadOnlyList<ISendable<ITransmittable>>> {
    private readonly IModCommandRegex _modCommandRegex;
    private readonly IReceivedFactory _receivedFactory;

    public AegisPardonFactory(IModCommandRegex modCommandRegex, IReceivedFactory receivedFactory, ISettings settings, ITimeService timeService) : base(settings, timeService) {
      _modCommandRegex = modCommandRegex;
      _receivedFactory = receivedFactory;
    }

    public IReadOnlyList<ISendable<ITransmittable>> Create(IReadOnlyList<IReceived<IUser, ITransmittable>> context) {
      var modMessages = context.OfType<IReceived<Moderator, IMessage>>().ToList();
      var nukes = _GetStringNukes(modMessages).Concat(_GetRegexNukes(modMessages));
      var victims = nukes.SelectMany(n => GetCurrentVictims(n, context));

      //TODO consider checking if these are actual victims?
      var alreadyPardoned = context.OfType<ReceivedPardon>().Select(umb => umb.Target);
      return victims.Except(alreadyPardoned).Select(v => new SendablePardon(v)).ToList();
    }

    public IReadOnlyList<ISendable<ITransmittable>> OnErrorCreate => new SendableError($"An error occured in {nameof(AegisPardonFactory)}.").Wrap().ToList();

    private IEnumerable<ParsedNuke> _GetStringNukes(IEnumerable<IReceived<Moderator, IMessage>> modMessages) => modMessages
      .Where(m => m.IsMatch(_modCommandRegex.Nuke))
      .Select(rm => _receivedFactory.ParsedNuke(rm));

    private IEnumerable<ParsedNuke> _GetRegexNukes(IEnumerable<IReceived<Moderator, IMessage>> modMessages) => modMessages
      .Where(m => m.IsMatch(_modCommandRegex.RegexNuke))
      .Select(rm => _receivedFactory.ParsedNuke(rm));
  }
}
