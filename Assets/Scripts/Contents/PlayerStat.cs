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
        Hp = 100;
        MaxHp = 100;
        Attack = 10;
        Defense = 5;
        MoveSpeed = 5.0f;
        Exp = 0;
        Gold = 0;
    }
}
