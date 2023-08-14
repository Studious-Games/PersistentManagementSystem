using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Studious.PersistentManagement
{
    public static class PersistentLocator 
    {
        private static readonly Dictionary<Type, PersistentInstance> _persistentObjs = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsRegistered(Type t)
        {
            return _persistentObjs.ContainsKey(t);
        }

        public static Dictionary<Type, PersistentInstance> GetAll()
        {
            return _persistentObjs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void UnRegister<T>()
        {
            if(IsRegistered(typeof(T)))
            {
                Type key = typeof(T);
                var value = _persistentObjs[key];

                value.RemoveFromGroup();
                _persistentObjs.Remove(key);
            }
        }

        public static void UnRegister(Type type)
        {
            _persistentObjs.Remove(type);
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

            _persistentObjs.Add(instance.Type, instance);
        }

        private static PersistentInstance Get(Type type )
        {
            return _persistentObjs[type];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            if (_persistentObjs.TryGetValue(typeof(T), out PersistentInstance instance))
            {
                if (instance.PersistentComponent is T component)
                    return component;
            }

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static IEnumerable<PersistentInstance> GetAllBySceneUnload(Scene scene)
        {
            return _persistentObjs
                .Where(kv => kv.Value.PersistentAttribute.SceneUnload == scene.name).Select(kv => kv.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public static IEnumerable<PersistentInstance> GetAllBySceneLoad(Scene scene)
        {
            return _persistentObjs
                .Where(kv => kv.Value.PersistentAttribute.Scene == scene.name || kv.Value.PersistentAttribute.Scene == null).Select(kv => kv.Value);
        }
    }

    public class PersistentLocatorException : Exception
    {
        public PersistentLocatorException(string message) : base(message) { }
    }
}
