using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMelee : MonoBehaviour, IMelee
{
    private int hp;

    private Action<EMelee> _action;
    public void Init(Action<EMelee> action)
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
        Debug.Log("Elite Melee Attack");
    }

    public void Move()
    {
        Debug.Log("Elite Melee Move");
    }
}
