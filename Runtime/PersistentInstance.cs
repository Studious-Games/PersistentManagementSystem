using System;
using UnityEngine;

namespace Studious.PersistentManagement
{
    public class PersistentInstance
    {
        public PersistentAttribute PersistentAttribute;
        public Type Type;
        public Component PersistentComponent;

        public PersistentInstance(PersistentAttribute persistentAttribute, Type type)
        {
            PersistentAttribute = persistentAttribute;
            Type = type;
        }

        public void AddToGroup()
        {
            GameObject group = GameObject.Find(PersistentAttribute.GroupName);

            if (PersistentAttribute.IsValidGroupName() && group == null)
            {
                GameObject go = new GameObject(PersistentAttribute.GroupName);
                go.hideFlags|= HideFlags.HideInHierarchy;
                MonoBehaviour.DontDestroyOnLoad(go);
                group = go;
            }

            Component comp = group.AddComponent(Type);
            PersistentComponent = comp;
        }

        public void RemoveFromGroup()
        {
            MonoBehaviour.DestroyImmediate(PersistentComponent);
            GameObject group = GameObject.Find(PersistentAttribute.GroupName);
            Component[] components = group.GetComponents(typeof(Component));

            if (group.transform.childCount == 1)
                MonoBehaviour.Destroy(group);
        }
    }
}