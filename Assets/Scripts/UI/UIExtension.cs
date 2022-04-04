using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XReal.XTown.UI
{
    public static class Extension
    {
        // Ȯ�� �޼���
        public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
        {
            return UIUtils.GetOrAddComponent<T>(go);
        }

        // Ȯ��޼���
        public static void BindEvent(this GameObject go, Action<PointerEventData> action, UIEvents.UIEvent type = UIEvents.UIEvent.Click)
        {
            UIBase.BindEvent(go, action, type);
        }
    }
}