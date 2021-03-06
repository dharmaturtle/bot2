﻿using System;
using System.Diagnostics;
using System.Linq;
using Bot.Database.Entities;
using Bot.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bot.Tests {
  public static class TestHelper {

    public static int RandomInt() => Random().Next();
    public static DateTime RandomDateTime() => TimeService.UnixEpoch.AddSeconds(Random().Next());

    public static AutoPunishmentType RandomAutoPunishmentType() {
      var values = Enum.GetValues(typeof(AutoPunishmentType));
      return (AutoPunishmentType) values.GetValue(Random().Next(values.Length));
    }

    public static Random Random(bool printSeed = true) {
      var seed = Guid.NewGuid().GetHashCode();
      if (printSeed) Trace.WriteLine($"Seed is: {seed}");
      return new Random(seed);
    }

    public static string RandomString(int length = 10, bool printSeed = false) {
      const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789 ";
      return new string(Enumerable.Repeat(chars, length)
        .Select(s => s[Random(printSeed).Next(s.Length)]).ToArray());
    }

    public static TException AssertCatch<TException>(Action action)
      where TException : Exception {
      try {
        action();
        throw new AssertFailedException($"Expected exception of type {typeof(TException)} was not thrown");
      } catch (TException exception) {
        return exception;
      }
    }

    public static TException AssertCatch<TException>(Func<object> func)
     where TException : Exception {
      try {
        func();
        throw new AssertFailedException($"Expected exception of type {typeof(TException)} was not thrown");
      } catch (TException exception) {
        return exception;
      }
    }

    public static DateTime Parse(string timestamp) => DateTime.MinValue + TimeSpan.Parse(timestamp);

  }
}
