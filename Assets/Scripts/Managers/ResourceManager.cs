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
        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go, float time = 0f)
    {
        if (go == null)
            return;

        Object.Destroy(go, time);
    }
}
