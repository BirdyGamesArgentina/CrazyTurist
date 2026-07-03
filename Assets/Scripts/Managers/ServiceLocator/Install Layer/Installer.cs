using System.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Shared.ServiceLocator
{
    public abstract class Installer : MonoBehaviour
    {
        public virtual void Install(IServiceLocator serviceLocator)
        {

        }
        public abstract void Uninstall(IServiceLocator serviceLocator);
    }
}
