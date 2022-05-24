using System;

public interface ITypeEventSystem : IDisposable
{
    IDisposable RegisterEvent<T>(Action<object> onReceive);
    void UnRegisterEvent<T>(Action<object> onReceive);
    void SendEvent<T>();
    void SendEvent<T>(object e);
    void Clear();
}

public interface ITypeEventSystem<Tvalue> : IDisposable
{
    IDisposable RegisterEvent<T>(Action<Tvalue> onReceive);
    void UnRegisterEvent<T>(Action<Tvalue> onReceive);
    void SendEvent<T>(Tvalue e);
    void Clear();
}