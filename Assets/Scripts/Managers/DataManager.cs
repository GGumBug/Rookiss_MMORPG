using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<int, Stat> StatDict {get; private set;} = new Dictionary<int, Stat>();

    public void Init()
    {
       StatData stats = LoaderJson<StatData, int, Stat>("StatData");
       StatDict = stats.MakeDict();
    }

    Loader LoaderJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
