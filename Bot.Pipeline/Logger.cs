﻿using System;
using Bot.Tools.Logging;
using log4net;

namespace Bot.Pipeline {
  public class Logger : ILogger {
    private readonly ILog _logger;

    public Logger() {
      _logger = LogManager.GetLogger(nameof(Logger));
    }

    public void LogDebug(string debug) => _logger.Debug(debug);
    public void LogInformation(string information) => _logger.Info(information);
    public void LogWarning(string warning) => _logger.Warn(warning);
    public void LogError(string error) => _logger.Error(error);
    public void LogError(string error, Exception exception) => _logger.Error(error, exception);
    public void LogFatal(string fatal) => _logger.Fatal(fatal);
    public void LogFatal(string fatal, Exception exception) => _logger.Fatal(fatal, exception);
  }
}
