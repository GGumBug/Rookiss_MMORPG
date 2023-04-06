using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager
{
    // 서버랑 연결시 id (int 혹은 long) <-> 짝지은 GameObject Dictionary 형태로 들고있을것
    // 서버 Dictionary 사용 예시 : Dictionary<int, Gameobject> _player = new Dictionary<int, Gameobject>();

    GameObject          _player;

    //HashSet은 key값이 없는 Dictionary라고 생각하면 됨
    HashSet<GameObject> _monster = new HashSet<GameObject>();

    public Action<int> OnSpawnEvent;

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch (type)
        {
            case Define.WorldObject.Player:
                _player = go;
                break;
            case Define.WorldObject.Monster:
                _monster.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.UnKnown;

        return bc.WorldObjectType;
    }

    public void DeSpawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
            case Define.WorldObject.Monster:
                {
                    if (_monster.Contains(go))
                        _monster.Remove(go);
                    if (OnSpawnEvent != null)
                        OnSpawnEvent.Invoke(-1);
                }
                break;
        }

        Managers.Resource.Destroy(go);
    }
}
