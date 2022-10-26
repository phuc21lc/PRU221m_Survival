using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform target;
    GameObject targetGameObj;
    Character targetCharacter;
    [SerializeField] float speed;

    Rigidbody2D rb;

    [SerializeField]
    int hp = 999;
    [SerializeField]
    int dmg = 1;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetGameObj = target.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject == targetGameObj)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(targetCharacter == null)
        {
            targetCharacter = targetGameObj.GetComponent<Character>();
        }
        targetCharacter.takeDamage(dmg);
    }
}
