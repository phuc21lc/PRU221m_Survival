using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    [SerializeField]
    private float speed;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

    }
}
