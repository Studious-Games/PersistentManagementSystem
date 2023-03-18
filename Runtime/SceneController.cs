using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

using Object = UnityEngine.Object;

namespace Studious.PersistentManagement
{

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class SceneController
    {
        static SceneController()
        {
            Application.quitting += OnApplicationQuit;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            LoadPersistentComponents(scene);
            UnLoadPersistentComponents(scene);
        }

        private static void UnLoadPersistentComponents(Scene scene)
        {
            //var scripts = PersistentLocator.GetAllBySceneUnload(scene).ToList();

            //foreach (var script in scripts)
            //{
            //    var test = GameObject.Find(script.SingletonAttribute.GroupName);
            //    Object.Destroy(test);
            //}
        }

        private static void LoadPersistentComponents(Scene scene)
        {
            List<PersistentInstance> testPI = PersistentLocator.GetAll();

            var scripts = PersistentLocator.GetAllBySceneLoad(scene).ToList();

            foreach (var script in scripts)
            {
                if(script.PersistentComponent == null)
                    Debug.Log($"Component {script.Type}");
            //    script.ScriptType.Invoke(null, null);
            }
        }

        private static void OnApplicationQuit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Application.quitting -= OnApplicationQuit;
        }
    }
}