
using UnityEngine;
using System.Collections.Generic;

namespace Eco
{
    public static class Services
    {
        private static readonly Dictionary<System.Type, MonoBehaviour> m_RegisteredServices = new Dictionary<System.Type, MonoBehaviour>();

        public static void RegisterService(MonoBehaviour service)
        {
            System.Type serviceType = service.GetType();

            if (m_RegisteredServices.ContainsKey(serviceType))
            {
                string warningMessage = $"Registered service of type {serviceType.Name} is already registered";

                if (m_RegisteredServices[serviceType] != service)
                {
                    GameObject serviceGameObject = service.gameObject;

                    warningMessage += $". The current service is not the same as the previously registered. Deleting service's GameObject \"{serviceGameObject.name}\"";

                    UnityEngine.GameObject.DestroyImmediate(serviceGameObject);
                }

                Debug.LogWarning(warningMessage);
            }
            else
            {
                m_RegisteredServices.Add(serviceType, service);
            }
        }

        public static T GetService<T>() where T : MonoBehaviour
        {
            System.Type serviceType = typeof(T);

            if (m_RegisteredServices.ContainsKey(serviceType))
            {
                return m_RegisteredServices[serviceType] as T;
            }

            Debug.LogError($"No service of type {serviceType.Name} registered");
            return null;
        }
    }
}
