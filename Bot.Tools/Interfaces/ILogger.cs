﻿namespace Bot.Tools.Interfaces {
  public interface ILogger {
    void LogWarning(string warning);
    void LogError(string error);
    void LogInformation(string information);
    void LogVerbose(string verbose);
  }
}