using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepMelees : MonoBehaviour, IMelee
{
    private Action<CreepMelees> _action;
    public void Init(Action<CreepMelees> action)
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
        Debug.Log("Creep Melee Attack");
    }

    public void Move()
    {
        Debug.Log("Creep Melee Move");
    }
}
