using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace XReal.XTown.UI
{
    public class UIManager : MonoBehaviour
    {
        static UIManager _UIManager;

        public static UIManager UI
        {
            get => _UIManager;
        }

        void Awake()
        {
            if (_UIManager != null && _UIManager != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _UIManager = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        int _order = 1;

        SceneView _scene = null;
        Stack<PopupView> _popupStack = new();

        public GameObject Root
        {
            get
            {
                GameObject root = GameObject.Find("@UI_Root");
                if (root is null) root = new GameObject { name = "@UI_Root" };

                return root;
            }
        }

        public void PrepareCanvas(GameObject go, bool sort = true)
        {
            Canvas canvas = go.GetComponent<Canvas>();
            if (canvas is null)
            {
                Debug.LogError("ShowCanvas: canvas is missing");
                return;
            }

            canvas.overrideSorting = true;
            if (sort)
            {
                canvas.sortingOrder = _order++;
            }
            else
            {
                canvas.sortingOrder = 0;
            }
        }

        public T PrepareView<T>(string name = null) where T: ObjectBase
        {
            if (string.IsNullOrEmpty(name)) name = typeof(T).Name;
            GameObject go = Instantiate(Resources.Load<GameObject>($"UI/{typeof(T)}/{name}"));
            T view = go.GetComponent<T>();

            if (view is null)
            {
                Debug.LogError($"UIManager/ failed to show view {typeof(T)}: check if script is attached");
                return null;
            }
            else if (view is SceneView)
            {
                _scene = view as SceneView;
            }
            else if (view is PopupView)
            {
                _popupStack.Push(view as PopupView);
            }
            else
            {
                Debug.LogError($"UIManager/ Currently inaccessible view class {typeof(T)}");
                return null;
            }

            go.transform.SetParent(Root.transform);

            return view;
        }

        public T PrepareSubItem<T>(Transform parent = null, string name = null) where T : ObjectBase
        {
            if (string.IsNullOrEmpty(name)) name = typeof(T).Name;
            GameObject go = Instantiate(Resources.Load<GameObject>($"UI/SubItem/{name}"));

            if (parent is null)
            {
                Debug.LogError("UIManager/ failed to open subitem: check if script is attached.");
                return null;
            }

            go.transform.SetParent(parent);

            return go.GetComponent<T>();
        }

        public void ClosePopup()
        {
            PopupView popup = _popupStack.Pop();
            Destroy(popup.gameObject);
            _order--;
        }

        public void ClosePopup(PopupView popup)
        {
            if (_popupStack.Count == 0) return;
            if (_popupStack.Peek() != popup)
            {
                Debug.LogError("UIManager/ Close popup failed!");
                return;
            }
            ClosePopup();
        }

        public void CloseAllPopup()
        {
            while (_popupStack.Count > 0) ClosePopup();
        }
    }
}