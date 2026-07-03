using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Scripts.Shared.Events;
using UnityEngine;

namespace Game.Scripts.Shared.ServiceLocator
{
    public class GeneralInstaller : MonoSingleton<GeneralInstaller>
    {
        [SerializeField] private List<Installer> installers;

        public void InstallDependencies()
        {
            foreach (Installer i in installers)
                i.Install(ServiceLocator.Instance);
        }

        protected override void OnAwake()
        {
            InstallDependencies();
        }
    }
}
