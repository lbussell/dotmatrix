namespace DotMatrix;

using Jab;

[ServiceProviderModule]
[Singleton(typeof(IGameConsole), typeof(GameConsole))]
[Singleton(typeof(ICpu), typeof(Cpu))]
[Singleton(typeof(IBus), typeof(Bus))]
[Singleton(typeof(IMemory), typeof(Memory))]
public interface IDefaultProviderModule
{
}
