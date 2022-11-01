using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject targetGameObj;
    [SerializeField]
    public int dmg = 100;
    Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        targetGameObj = GameObject.FindGameObjectWithTag("Monster");
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("Monster"))
        {
            Attack();
            Destroy(this.gameObject);
        }
    }

    private void Attack()
    {
        //if (enemy == null)
        //{
        //    enemy = targetGameObj.GetComponent<Enemy>();
        //}
        //FindObjectOfType<Enemy>().takeDamage(dmg);
    }
}
