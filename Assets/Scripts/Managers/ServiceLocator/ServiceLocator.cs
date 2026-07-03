using System;
using System.Collections.Generic;

namespace Game.Scripts.Shared.ServiceLocator
{
	public class ServiceLocator : IServiceLocator
	{
		public static ServiceLocator Instance;

		private readonly Dictionary<Type, object> _services;
    
		public ServiceLocator()
		{
			_services = new Dictionary<Type, object>();

			if (Instance == null)
			{
                Instance = this;
			}
		}
    
		public bool Contains<Type>()
		{
			var serviceType = typeof(Type);
			return _services.ContainsKey(serviceType);
		}
    
		public Type GetService<Type>()
		{
			var serviceType = typeof(Type);
			if (!_services.TryGetValue(serviceType, out var service))
			{
				throw new Exception($"Service of type {serviceType} not found");
			}
			return (Type)service;
		}
    
		public void RegisterService<Type>(Type service)
		{
			var serviceType = typeof(Type);
			if (_services.ContainsKey(serviceType))
				_services.Remove(serviceType);
			_services.Add(serviceType, service);
		}
    
		public void UnregisterService<Type>()
		{
			var serviceType = typeof(Type);
			if (_services.ContainsKey(serviceType))
				_services.Remove(serviceType);
		}
	}
}
