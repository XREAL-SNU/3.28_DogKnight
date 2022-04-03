using UnityEngine;

namespace XReal.XTown.UI
{
    public class UI_Utils
    {
        //주어진 것들 중 이름을 서칭함

        public static T FindUIChild<T>(GameObject go, string UIname = null, bool searchGrandChildren = false) where T : UnityEngine.Object
        {
            //특정 컴포넌트가 있는 오브젝트를 서칭함
            // go오브젝트의 모든 자식들 중?<T> 컴포넌트를 가지며,?<UIname과 이름이 일치> 하는 오브젝트를 찾아 리턴합니다. 
            if (go == null) return null;

            if (!searchGrandChildren) //searchGrandChildren이 false면(default) <go의 "직속 자식"들 중에서만 T 컴포넌트를 가진 자식을 찾습니다>.
            {
                for (int i = 0; i < go.transform.childCount; ++i)
                {
                    //자식의 순서까지 내려가버령~
                    Transform tr = go.transform.GetChild(i); //tr을 가져와버령~
                    if (string.IsNullOrEmpty(UIname) || tr.name == UIname)
                    {
                        //비어있거나 null이면
                        //UI이름이 없거나 가져온 tr과의 이름과 같으면?
                        T component = tr.GetComponent<T>(); //컴포넌트를 가져옴
                        if (component != null) return component; //컴포넌트가 있다면 return
                    }
                }
            }
            else //searchGrandChildren이 true면 go의 "증손자들"까지 rescursive하게 모두 뒤져서 T 컴포넌트를 가진 자식을 찾습니다.
            {
                foreach (T component in go.GetComponentsInChildren<T>())
                {
                    if (string.IsNullOrEmpty(UIname) || component.name == UIname)
                    {
                        return component;
                    }
                }
            }
            Debug.Log($"UIUtils/ FindUIChild failed : {UIname}");
            return null;
        }

        public static GameObject FindUIChild(GameObject go, string name = null, bool searchGrandChildren = false)
        {
            //컴포넌트가 없는 오브젝트 서칭
            //빈 오브젝트 포함한 모든 오브젝트는 Transform 컴포넌트를 가지므로 이 Transform 컴포넌트를 가진 오브젝트를 찾아오도록?FindChild<Transform> 를 호출합니다.
            Transform transform = FindUIChild<Transform>(go, name, searchGrandChildren);
            if (transform == null) return null;
            return transform.gameObject;
        }

        public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            //오브젝트에 임의의 T컴포넌트 생성하고 가져옴
            T component = go.GetComponent<T>();
            if (component == null)
                component = go.AddComponent<T>();
            return component;
        }
    }
}