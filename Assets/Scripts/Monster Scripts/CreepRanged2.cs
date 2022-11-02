using Assets.Scripts.Monster_Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepRanged2 : MonoBehaviour, IRanged
{
    private Action<CreepRanged2> _action;

<<<<<<< HEAD
    public CreepRanged2(float hp, float attackDamage, float attackRange)
    {
        Hp = hp;
        AttackDamage = attackDamage;
        AttackRange = attackRange;
    }

  

=======
>>>>>>> d636285e532018bdd3eca031c3caf24737199102
    public void Init(Action<CreepRanged2> action)
    {
        _action = action;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            _action(this);
        }
    }
    private void OnBecameInvisible()
    {
        _action(this);
    }
    public void Attack()
    {
        Debug.Log("Creep Range Attack");
    }

    public void Move()
    {
        Debug.Log("Creep Range Move");
    }
}