using Game.Scripts.Shared.ServiceLocator;
using System.Threading.Tasks;


namespace Game.Scripts.Shared.Events.Installer
{
    public class EventBusInstaller : ServiceLocator.Installer
    {
        public override void Install(IServiceLocator serviceLocator)
        {
            serviceLocator.RegisterService<IEventBus>(new GameEventBus());
        }

        public override void Uninstall(IServiceLocator serviceLocator)
        {
            serviceLocator.UnregisterService<IEventBus>();
        }
    }
}
