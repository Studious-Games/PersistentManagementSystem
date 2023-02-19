using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Studious.PersistentManagement
{
    public static class PersistentInitialization 
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialization()
        {
            InstantiatePersistentObjects();
        }

        private static void InstantiatePersistentObjects()
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes().Where(type => type.GetCustomAttributes(typeof(PersistentAttribute), true).Length > 0));

            GameObject manager = new GameObject("PersistanceManager");
            //go.hideFlags = HideFlags.HideInHierarchy;
            MonoBehaviour.DontDestroyOnLoad(manager);

            foreach (var type in types)
            {
                PersistentAttribute attr = (PersistentAttribute)Attribute.GetCustomAttribute(type, typeof(PersistentAttribute));

                var group = GameObject.Find(attr.GroupName);

                if (attr.IsValidGroupName() && group == null) {
                    GameObject go = new GameObject(attr.GroupName);
                    //go.hideFlags|= HideFlags.HideInHierarchy;
                    MonoBehaviour.DontDestroyOnLoad(go);
                    group = go;
                } else if(group == null)
                { 
                    group = manager;
                }

                var comp = group.AddComponent(type);

                PersistentInstance pi = new PersistentInstance(attr, type, comp);
                PersistentLocator.Register(pi);

                //List<Component> results = new();
                //pi.PersistentComponent.gameObject.GetComponents(results);

                //var rr = 100;
                //MonoBehaviour.Destroy(pi.PersistentComponent);

            }
        }
    }
}
