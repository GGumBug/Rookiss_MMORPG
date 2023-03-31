using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    void Start()
    {
        GameObject go = Managers.Resource.Instantiate("Tank");
        Managers.Resource.Destroy(go, 3);
    }
}
