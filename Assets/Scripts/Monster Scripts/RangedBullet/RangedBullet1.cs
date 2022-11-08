using Assets.Scripts.Monster_Scripts.Ranged;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBullet1 : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D bulletRb;
    private Action<RangedBullet1> _action;

    private GameObject target;

    public void Init(Action<RangedBullet1> action)
    {
        _action = action;
    }
    
    void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRb.velocity = new Vector2(moveDir.x, moveDir.y);
        //Destroy(gameObject,2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            _action(this);
        }
    }

    private void OnBecameInvisible()
    {
        _action(this);
    }
}
