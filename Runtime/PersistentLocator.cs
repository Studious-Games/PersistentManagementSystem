using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Studious.PersistentManagement
{
    public static class PersistentLocator 
    {
        private static readonly List<PersistentInstance> _persitentObjects = new List<PersistentInstance>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsRegistered(Type t)
        {
            return _persitentObjects.Where(x => x.Type == t).Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void UnRegister<T>()
        {
            if(IsRegistered(typeof(T)))
            {
                var persistentComponent = Get(typeof(T));
                _persitentObjects.Remove(persistentComponent);

                //Todo : Unregister component, if no more then remove object as well.

                //if (persistentComponent.PersistentAttribute.KeepSeperate)
                //{
                //    MonoBehaviour.Destroy(persistentComponent.PersistentComponent.gameObject);
                //}
                //else
                //{
                //    MonoBehaviour.Destroy(persistentComponent.PersistentComponent);
                //}
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <exception cref="PersistentLocatorException"></exception>
        public static void Register(PersistentInstance instance)
        {
            if (IsRegistered(instance.Type))
                throw new PersistentLocatorException($"{instance.Type.Name} has been already registered.");

            _persitentObjects.Add(instance);
        }

        private static PersistentInstance Get(Type type )
        {
            return _persitentObjects.Where(item => item.Type == type).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            if (!IsRegistered(typeof(T)))
                return default;

            return _persitentObjects.Select(item => item.PersistentComponent).OfType<T>().FirstOrDefault(item => item.GetType() == typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static IEnumerable<PersistentInstance> GetAllBySceneUnload(Scene scene)
        {
            return _persitentObjects.Where(x => x.PersistentAttribute.SceneUnload == scene.name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static IEnumerable<PersistentInstance> GetAllBySceneLoad(Scene scene)
        {
            return _persitentObjects.Where(x => x.PersistentAttribute.Scene == scene.name);
        }
    }

    public class PersistentLocatorException : Exception
    {
        public PersistentLocatorException(string message) : base(message) { }
    }
}
