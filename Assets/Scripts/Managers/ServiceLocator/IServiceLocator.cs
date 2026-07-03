namespace Game.Scripts.Shared.ServiceLocator
{
    public interface IServiceLocator
    {
        bool Contains<T>();
        T GetService<T>();
        void RegisterService<T>(T service);
        void UnregisterService<T>();
    }
}
