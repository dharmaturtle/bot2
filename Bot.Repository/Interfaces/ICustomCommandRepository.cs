﻿using System.Collections.Generic;
using Bot.Models;

namespace Bot.Repository.Interfaces {
  public interface ICustomCommandRepository {
    CustomCommand Get(string command);
    IList<CustomCommand> GetAll { get; }
    void Add(string command, string response);
    void Update(string command, string response);
    void Delete(string command);
  }
}
