﻿using System;
using Bot.Models.Contracts;

namespace Bot.Models {
  public interface IReceivedFactory {
    PublicMessageFromMod ModPublicReceivedMessage(string text);
    PublicMessageFromMod ModPublicReceivedMessage(string text, DateTime timestamp);
    PublicReceivedMessage PublicReceivedMessage(string text);
    PublicReceivedMessage PublicReceivedMessage(string text, DateTime timestamp);
    ReceivedPardon ReceivedPardon(IUser sender, IUser target);
    ReceivedRegexNuke ReceivedRegexNuke(ReceivedMessage message);
    ReceivedStringNuke ReceivedStringNuke(ReceivedMessage message);
    ReceivedRegexNuke ReceivedRegexNuke(string command);
    ReceivedStringNuke ReceivedStringNuke(string command);
  }
}