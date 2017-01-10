﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bot.Logic.Contracts;
using Bot.Models;
using Bot.Models.Contracts;
using Bot.Tools;

namespace Bot.Logic {
  public class ModCommandGenerator : IModCommandGenerator {
    private readonly IModCommandLogic _modCommandLogic;
    private readonly IModCommandParser _modCommandParser;
    private readonly IModCommandRegex _modCommandRegex;

    public ModCommandGenerator(IModCommandLogic modCommandLogic, IModCommandParser modCommandParser, IModCommandRegex modCommandRegex) {
      _modCommandLogic = modCommandLogic;
      _modCommandParser = modCommandParser;
      _modCommandRegex = modCommandRegex;
    }

    public IReadOnlyList<ISendable> Generate(IContextualized contextualized) {
      var context = contextualized.Context;
      var message = contextualized.First as ReceivedMessage;
      if (message != null) {
        if (message.IsMatch(_modCommandRegex.Sing))
          return _modCommandLogic.Sing().Wrap().ToList();
        if (message.StartsWith("!long"))
          return _modCommandLogic.Long(context).Wrap().ToList();
        if (message.IsMatch(_modCommandRegex.Nuke)) {
          var phrase = _modCommandParser.Nuke(message.Text).Item1;
          var duration = _modCommandParser.Nuke(message.Text).Item2;
          return _modCommandLogic.Nuke(context, phrase, duration);
        }
      }
      return new List<ISendable>();
    }

  }
}
