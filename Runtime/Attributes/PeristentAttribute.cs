using System;
using UnityEngine;

namespace Studious.PersistentManagement
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class PersistentAttribute : Attribute
    {
        public HideFlags HideFlags { get; set; }
        public string GroupName { get; set; }
        public string Scene { get; set; }
        public string SceneUnload { get; set; }

        public PersistentAttribute()
        {
            HideFlags = HideFlags.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsValidGroupName()
        {
            if(GroupName != null && GroupName != string.Empty)
                return true;
            else
                return false;
        }

    }
}