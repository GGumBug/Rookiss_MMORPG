using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // Object를 안붙이면 재귀함수로 본인을 호출하려해서 붙여줘야된다.
        GameObject go = Object.Instantiate(prefab, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index); // 0 번부터 index 까지의 문자열을 잘라서 반환
        // go.name = prefab.name으로 하면 한줄로 가능하다.
        // Load는 메모리에 원본을 할당 시키는거고, Instantiate는 할당한 원본의 복사본을 객체화 시키는 것이라 clone이 붙는 거다.

        return go;
    }

    public void Destroy(GameObject go, float time = 0f)
    {
        if (go == null)
            return;

        Object.Destroy(go, time);
    }
}
