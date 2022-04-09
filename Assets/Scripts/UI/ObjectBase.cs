using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XReal.XTown.UI
{
    public abstract class ObjectBase : MonoBehaviour
    {
        protected Dictionary<Type, UnityEngine.Object[]> _objects = new();
        public abstract void Init();

        /// <summary>
        /// If targetName is provided, searches through go's children for
        /// component with name that matches targetName.
        /// If not, searches through go's children assuming uniqueness.
        /// If desirable component is found, returns it.
        /// If not, add T component to go and returns it.
        /// </summary>
        public static T GetComponent<T>(
            GameObject go,
            string targetName = null
            ) where T : Component
        {
            if (go is null) return null;

            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (targetName is null || component.name == targetName)
                {
                    return component;
                }
            }

            return go.AddComponent<T>();
        }

        protected void BindGameObjects(Type type)
        {
            string[] names = Enum.GetNames(type);
            GameObject[] gameObjects = new GameObject[names.Length];

            for (int i = 0; i < names.Length; ++i)
            {
                gameObjects[i] = GetComponent<Transform>(this.gameObject, names[i]).gameObject;
            }

            _objects.Add(typeof(GameObject), gameObjects);
        }

        protected void BindComponents<T>(Type type) where T: Component
        {
            string[] names = Enum.GetNames(type);
            Component[] components = new Component[names.Length];

            for (int i = 0; i < names.Length; ++i)
            {
                components[i] = GetComponent<T>(gameObject, names[i]);
            }

            _objects.Add(typeof(T), components);
        }

        public static void BindEvent(
            GameObject go,
            Action<PointerEventData> action,
            UIEvents.UIEvent type = UIEvents.UIEvent.PointerClick
            )
        {
            UIEventHandler evt = go.GetComponent<UIEventHandler>();

            if (evt is null)
            {
                Debug.Log("<color = red> Deprecation warning: XReal UI convention requires you to attach UIEventHandler script to " +
                    "interactable UI elements. But we will attach it for you for now. </color>");
                evt = go.AddComponent<UIEventHandler>();
            }

            switch (type)
            {
                case UIEvents.UIEvent.PointerEnter:
                    evt.OnPointerEnterHandler -= action;
                    evt.OnPointerEnterHandler += action;
                    break;
                case UIEvents.UIEvent.PointerClick:
                    evt.OnPointerClickHandler -= action;
                    evt.OnPointerClickHandler += action;
                    break;
                case UIEvents.UIEvent.PointerExit:
                    evt.OnPointerExitHandler -= action;
                    evt.OnPointerExitHandler += action;
                    break;
                case UIEvents.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    evt.OnDragHandler += action;
                    break;
            }
        }

        protected GameObject GetGameObject(int index)
        {
            if (_objects.TryGetValue(typeof(GameObject), out UnityEngine.Object[] objects))
            {
                return objects[index] as GameObject;
            }

            Debug.LogError($"UIBase/ Cannot find component of type {typeof(GameObject)}");
            return null;
        }

        protected T GetComponent<T>(int index) where T : UnityEngine.Component
        {
            if (_objects.TryGetValue(typeof(T), out UnityEngine.Object[] objects))
            {
                return objects[index] as T;
            }

            Debug.LogError($"UIBase/ Cannot find component of type {typeof(T)}");
            return null;
        }
    }
}