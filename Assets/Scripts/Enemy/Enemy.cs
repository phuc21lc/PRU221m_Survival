using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    GameObject targetGameObj;
    Character targetCharacter;
    [SerializeField] float speed;

    Rigidbody2D rb;

    [SerializeField]
    GameObject healOrbPrefab;

    [SerializeField]
    int hp = 10;
    [SerializeField]
    int dmg = 1;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetGameObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = (targetGameObj.transform.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObj)
        {
            Attack();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Bullet"))
        {
            takeDamage(FindObjectOfType<Bullet>().dmg);
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
    private void takeDamage(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
        {
            int a = Random.Range(1, 5);
            if (a == 1)
            {
                GameObject arrow = Instantiate(healOrbPrefab, this.transform.position, Quaternion.identity);
            }
            //GameObject arrow = Instantiate(healOrbPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
