using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    [SerializeField]
    private float speed;
    public Animator animator;
    GameObject targetGameObj;
    Character targetCharacter;
    [SerializeField]
    int hp = 10;
    [SerializeField]
    int dmg = 1;
    Rigidbody2D rb;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetGameObj = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 des = (targetGameObj.transform.position - transform.position).normalized;
        rb.velocity = des * speed;
        Vector3 direction = player.position - this.transform.position;
        if (direction.x < 0)
        {
            animator.SetBool("IsRight", false);
            animator.SetBool("IsLeft", true);
        }
        else
        {
            animator.SetBool("IsRight", true);
            animator.SetBool("IsLeft", false);
        }
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
        if (targetCharacter == null)
        {
            targetCharacter = targetGameObj.GetComponent<Character>();
        }
        targetCharacter.takeDamage(dmg);
    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        Debug.Log(hp);
        if (hp <= 0)
        {
            //int a = Random.Range(1, 5);
            //if (a == 1)
            //{
            //    GameObject arrow = Instantiate(healOrbPrefab, this.transform.position, Quaternion.identity);
            //}
            //GameObject arrow = Instantiate(healOrbPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
