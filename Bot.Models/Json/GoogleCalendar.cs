﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using NodaTime;
using NodaTime.Text;

namespace Bot.Models.Json {
  public class GoogleCalendar {
    public class Creator {
      public string email { get; set; }
      public string displayName { get; set; }
    }

    public class Organizer {
      public string email { get; set; }
      public string displayName { get; set; }
      public bool self { get; set; }
    }

    public class Start {
      public string date { get; set; }
      public string dateTime { get; set; }
      public string timeZone { get; set; }
    }

    public class End {
      public string date { get; set; }
      public string dateTime { get; set; }
      public string timeZone { get; set; }
    }

    public class OriginalStartTime {
      public string dateTime { get; set; }
      public string timeZone { get; set; }
      public string date { get; set; }
    }

    public class ExtendedItem {
      private readonly string _timeZone;
      public ExtendedItem(Item item, string timeZone) {
        Item = item;
        _timeZone = timeZone;
      }
      public Item Item { get; }
      public DateTime ParsedStart => Item.start.dateTime != null ? ParsedStart_dateTime : ParsedStart_date;
      public DateTime ParsedStart_dateTime => DateTime.ParseExact(Item.start.dateTime, "yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture).ToUniversalTime();
      public DateTime ParsedStart_date {
        get {
          var pattern = LocalDateTimePattern.CreateWithInvariantCulture("yyyy-MM-dd");
          var localTime = pattern.Parse(Item.start.date).Value;
          var timeZone = DateTimeZoneProviders.Tzdb[_timeZone];
          return localTime.InZoneLeniently(timeZone).ToDateTimeUtc();
        }
      }
    }

    public class Item {
      public string kind { get; set; }
      public string etag { get; set; }
      public string id { get; set; }
      public string status { get; set; }
      public string htmlLink { get; set; }
      public string created { get; set; }
      public string updated { get; set; }
      public string summary { get; set; }
      public Creator creator { get; set; }
      public Organizer organizer { get; set; }
      public Start start { get; set; }
      public End end { get; set; }
      public string transparency { get; set; }
      public string iCalUID { get; set; }
      public int sequence { get; set; }
      public string recurringEventId { get; set; }
      public OriginalStartTime originalStartTime { get; set; }
    }

    public class RootObject {
      public string kind { get; set; }
      public string etag { get; set; }
      public string summary { get; set; }
      public string description { get; set; }
      public string updated { get; set; }
      public string timeZone { get; set; }
      public string accessRole { get; set; }
      public List<object> defaultReminders { get; set; }
      public string nextPageToken { get; set; }
      public List<Item> items { get; set; }
      public List<ExtendedItem> ExtendedItem => items.Select(i => new ExtendedItem(i, timeZone)).ToList();
    }

  }
}
