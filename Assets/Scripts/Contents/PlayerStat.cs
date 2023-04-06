using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [field:SerializeField]
    public int Exp {get; protected set;}
    [field:SerializeField]
    public int Gold {get; protected set;}

    private void Start() 
    {
        Level = 1;

        Exp = 0;
        Defense = 5;
        MoveSpeed = 5.0f;
        Gold = 0;

        SetStat(Level);
    }

    public void SetEXP(int exp)
    {
        Exp += exp;

        LevelUpCheck();
    }

    public void LevelUpCheck()
    {
        int level = Level;
        while (true)
        {
            Data.Stat stat;
            if (!Managers.Data.StatDict.TryGetValue(level + 1, out stat))
                break;
            if (Exp < stat.totalExp)
                break;
            level++;
        }

        if (level != Level)
        {
            Debug.Log("Level Up!!!");
            Level = level;
            SetStat(Level);
        }
    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];

        Hp = stat.maxHp;
        MaxHp = stat.maxHp;
        Attack = stat.attack;
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
    }
}
