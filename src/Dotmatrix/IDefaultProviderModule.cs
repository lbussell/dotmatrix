﻿namespace Dotmatrix;

using Jab;

[ServiceProviderModule]
[Singleton(typeof(IGameConsole), typeof(GameConsole))]
[Singleton(typeof(ICpu), typeof(SM83))]
[Singleton(typeof(IBus), typeof(Bus))]
[Singleton(typeof(IMemory), typeof(Memory))]
public interface IDefaultProviderModule
{
}