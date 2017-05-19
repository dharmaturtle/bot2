﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Bot.Tools.Interfaces {
  public static class IEnumerableExtensions {
    public static TData ArgMax<TData, TColumn>(this IEnumerable<TData> set, Func<TData, TColumn> compareBy) =>
      set.OrderByDescending(compareBy).FirstOrDefault();

    public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int n) {
      var enumerated = source.ToList();
      return enumerated.Skip(Math.Max(0, enumerated.Count - n));
    }

  }
}
