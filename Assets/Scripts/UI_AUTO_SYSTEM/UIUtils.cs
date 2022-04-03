using UnityEngine;

namespace XReal.XTown.UI
{
    public class UI_Utils
    {
        //�־��� �͵� �� �̸��� ��Ī��

        public static T FindUIChild<T>(GameObject go, string UIname = null, bool searchGrandChildren = false) where T : UnityEngine.Object
        {
            //Ư�� ������Ʈ�� �ִ� ������Ʈ�� ��Ī��
            // go������Ʈ�� ��� �ڽĵ� ��?<T> ������Ʈ�� ������,?<UIname�� �̸��� ��ġ> �ϴ� ������Ʈ�� ã�� �����մϴ�. 
            if (go == null) return null;

            if (!searchGrandChildren) //searchGrandChildren�� false��(default) <go�� "���� �ڽ�"�� �߿����� T ������Ʈ�� ���� �ڽ��� ã���ϴ�>.
            {
                for (int i = 0; i < go.transform.childCount; ++i)
                {
                    //�ڽ��� �������� ����������~
                    Transform tr = go.transform.GetChild(i); //tr�� �����͹���~
                    if (string.IsNullOrEmpty(UIname) || tr.name == UIname)
                    {
                        //����ְų� null�̸�
                        //UI�̸��� ���ų� ������ tr���� �̸��� ������?
                        T component = tr.GetComponent<T>(); //������Ʈ�� ������
                        if (component != null) return component; //������Ʈ�� �ִٸ� return
                    }
                }
            }
            else //searchGrandChildren�� true�� go�� "�����ڵ�"���� rescursive�ϰ� ��� ������ T ������Ʈ�� ���� �ڽ��� ã���ϴ�.
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
            //������Ʈ�� ���� ������Ʈ ��Ī
            //�� ������Ʈ ������ ��� ������Ʈ�� Transform ������Ʈ�� �����Ƿ� �� Transform ������Ʈ�� ���� ������Ʈ�� ã�ƿ�����?FindChild<Transform> �� ȣ���մϴ�.
            Transform transform = FindUIChild<Transform>(go, name, searchGrandChildren);
            if (transform == null) return null;
            return transform.gameObject;
        }

        public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            //������Ʈ�� ������ T������Ʈ �����ϰ� ������
            T component = go.GetComponent<T>();
            if (component == null)
                component = go.AddComponent<T>();
            return component;
        }
    }
}