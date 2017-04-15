﻿using System.Collections.Generic;
using System.Linq;
using Bot.Database.Entities;
using Bot.Models;
using Bot.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bot.Repository {
  public class CustomCommandRepository : ICustomCommandRepository {

    private readonly DbSet<CustomCommandEntity> _entities;

    public CustomCommandRepository(DbSet<CustomCommandEntity> entities) {
      _entities = entities;
    }

    public IList<CustomCommand> GetAll => _entities.Select(c => new CustomCommand(c.Command, c.Response)).ToList();

    public void Add(string command, string response) => _entities.Add(new CustomCommandEntity(command, response));
  }
}