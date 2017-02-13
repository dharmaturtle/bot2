﻿namespace Bot.Models.Interfaces {
  public interface IReceivedVisitor<out TResult> : IDynamicVisitor<TResult> {
    TResult Visit<TUser, TTransmission>(Received<TUser, TTransmission> received)
      where TUser : IUser
      where TTransmission : ITransmittable;
  }
}
