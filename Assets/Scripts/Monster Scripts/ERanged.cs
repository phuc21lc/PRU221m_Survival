using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERanged : MonoBehaviour, IRanged
{
    private Action<ERanged> _action;
    public void Init(Action<ERanged> action)
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
        Debug.Log("Elite Range Attack");
    }

    public void Move()
    {
        Debug.Log("Elite Range Move");
    }
}
