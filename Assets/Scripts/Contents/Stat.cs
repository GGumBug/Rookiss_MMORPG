using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [field:SerializeField]
    public int Level {get; protected set;}
    [field:SerializeField]
    public int Hp {get; protected set;}
    [field:SerializeField]
    public int MaxHp {get; protected set;}
    [field:SerializeField]
    public int Attack {get; protected set;}
    [field:SerializeField]
    public int Defense {get; protected set;}
    [field:SerializeField]
    public float MoveSpeed {get; protected set;}

    private void Start() 
    {
        Level = 1;
        Hp = 100;
        MaxHp = 100;
        Attack = 10;
        Defense = 5;
        MoveSpeed = 5.0f;
    }

    public virtual void OnAttacked(Stat attaker)
    {
        int damage = Mathf.Max(0, attaker.Attack - Defense);
        Hp -= damage;
        Hp = Mathf.Clamp(Hp, 0, MaxHp);

        if (Hp <= 0)
        {
            OnDead(attaker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.SetEXP(15);
        }

        Managers.Game.DeSpawn(gameObject);
    }
}
