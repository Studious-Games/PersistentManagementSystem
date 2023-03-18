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
    }
}