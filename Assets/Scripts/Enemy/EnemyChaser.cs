using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform target;
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField]
    public float within_range = 10f;
    [SerializeField]
    public float speed = 1f;
    private Vector2 movement;
    void Start()
    {
        // Vì preFab ko cho g?n game obj ? bên ngoài nên ta s? khai báo private ?? x? lý trong code
        player = GameObject.FindWithTag("Player"); //tim game obj can duoi theo
        target = player.transform; //ep lai kieu
        rb = this.GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        float dist = Mathf.Abs(Vector2.Distance(target.position, transform.position));
        if (dist >= within_range)
        {
            transform.Translate(Vector2.down * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        }

    }
}
