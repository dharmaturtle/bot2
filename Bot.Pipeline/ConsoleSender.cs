﻿using System;
using Bot.Models.Contracts;
using Bot.Pipeline.Contracts;

namespace Bot.Pipeline {
  public class ConsoleSender : ISender {

    //Console.WriteLine($"Sending a mute targeting {mute.Target} for {mute.Duration}");

    //Console.WriteLine($"Sending an unmuteban targeting {unMuteBan.Target}");

    //Console.WriteLine($"Sending a subonly that is {subonly.IsEnabled}");

    //Console.WriteLine($"Sending a ban targeting {ban.Target} for {ban.Duration}; perm is {ban.IsPermanent}");

    //Console.WriteLine($"Sending a broadcast saying: {broadcast.Text}");

    public void Send(ISendable sendable) {
      Console.WriteLine(sendable.ConsolePrint);
    }
  }
}
