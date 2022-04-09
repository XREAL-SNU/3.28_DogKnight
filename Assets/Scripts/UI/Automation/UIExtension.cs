using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XReal.XTown.UI
{
    public static class Extension
    {
        /// <summary>
        /// If targetName is provided, searches through go's children for
        /// component with name that matches targetName.
        /// If not, searches through go's children assuming uniqueness.
        /// If desirable component is found, returns it.
        /// If not, add T component to go and returns it.
        /// </summary>
        public static T GetComponent<T>(
            this GameObject go,
            string targetName = null
            ) where T: Component
        {
            return ObjectBase.GetComponent<T>(go, targetName);
        }

        public static void BindEvent(
            this GameObject go,
            Action<PointerEventData> action,
            UIEvents.UIEvent type = UIEvents.UIEvent.PointerClick
            )
        {
            ObjectBase.BindEvent(go, action, type);
        }
    }
}