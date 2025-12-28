namespace BetelgueseLib.Core;

public interface IComponentData
{
    string Name => GetType().Name;

    string ComponentName { get; }
}