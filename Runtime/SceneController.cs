using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            var scripts = PersistentLocator.GetAllBySceneUnload(scene).ToList();

            foreach (var script in scripts)
            {
                if (script.PersistentComponent == null)
                    break;
                script.RemoveFromGroup();
            }
        }

        private static void LoadPersistentComponents(Scene scene)
        {
            var scripts = PersistentLocator.GetAllBySceneLoad(scene).ToList();

            foreach (var script in scripts)
            {
                if (script.PersistentComponent == null)
                {
                    script.AddToGroup();
                }
            }
        }

        private static void OnApplicationQuit()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Application.quitting -= OnApplicationQuit;
        }
    }
}