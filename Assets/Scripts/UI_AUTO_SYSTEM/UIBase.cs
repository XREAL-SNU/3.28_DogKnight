using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
// all UI canvases should derive from this.

namespace XReal.XTown.UI
{
    public abstract class UIBase : MonoBehaviour
    {
        // 오브젝트 저장 및 관리
        protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>(); //유니티 씬 상에 존재하는 모든 UI 오브젝트들을 로드해서 이 곳에 바인딩하여 보관합니다.
        // 모든 프리팹은 UIBase.cs 을 상속 받습니다. 
        // 캔버스 안의 구성 오브젝트들을 Enum으로 분류하고 각 Enum에 오브젝트 이름을 넣습니다. 이 Enum Type 을 Key로 하여 그 Enum 에 담긴 이름들에 해당하는 실제 오브젝트들을 배열로 담아 Value 로 할 것입니다. 예컨데, Button, Text, Image, GameObject 등등으로 분류
        public abstract void Init();

        // T must contain an enum definition for UI element names.
        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            string[] names = Enum.GetNames(type); //Enum전체를 배열로 받는 함수임
            UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
            _objects.Add(typeof(T), objects);

            for (int i = 0; i < names.Length; ++i)
            {
                if (typeof(T) == typeof(GameObject))
                { // if T is a gameObject
                    objects[i] = UI_Utils.FindUIChild(gameObject, names[i], true); //names[i] 이름과 일치하는 오브젝트를 씬에서 찾아 하나하나 objects[i]에 로드
                }
                else
                { // if T is a component (Button, Image etc)
                    objects[i] = UI_Utils.FindUIChild<T>(gameObject, names[i], true);
                }
            }
        }

        // idx is usually an ENUM casted to int. don't worry about memorizing numbers..
        // T is UIStuff,, like Button,Image,, etc.
        // similar to GetComponent, except we give extra search ENUM idx.
        protected T GetUIComponent<T>(int idx) where T : UnityEngine.Object
        {
            UnityEngine.Object[] objects = null;
            if (!_objects.TryGetValue(typeof(T), out objects))
            {
                Debug.LogError($"UIBase/ Cannot find component of type {typeof(T)}");
                return null;
            }

            return objects[idx] as T;
        }

        protected GameObject GetObject(int idx) { return GetUIComponent<GameObject>(idx); } // 오브젝트로서 가져오기
        protected Text GetText(int idx) { return GetUIComponent<Text>(idx); } // Text로서 가져오기
        protected Button GetButton(int idx) { return GetUIComponent<Button>(idx); } // Button로서 가져오기
        protected Image GetImage(int idx) { return GetUIComponent<Image>(idx); } // Image로서 가져오기

        // Binding event to gameObject. note this is static!!!
        public static void BindEvent(GameObject go, Action<PointerEventData> action, UIEvents.UIEvent type = UIEvents.UIEvent.Click)
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
                case UIEvents.UIEvent.Enter:
                    evt.OnEnterHandler -= action;
                    evt.OnEnterHandler += action;
                    break;
                case UIEvents.UIEvent.Click:
                    evt.OnClickHandler -= action;
                    evt.OnClickHandler += action;
                    break;
                case UIEvents.UIEvent.Exit:
                    evt.OnExitHandler -= action;
                    evt.OnExitHandler += action;
                    break;
                case UIEvents.UIEvent.Drag:
                    evt.OnDragHandler -= action;
                    evt.OnDragHandler += action;
                    break;
            }
        }
    }
}