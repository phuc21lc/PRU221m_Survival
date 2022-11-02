using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Monster:")]
    [SerializeField]
    private int speed;
    private Rigidbody2D rb;
    private Vector3 mousePosition;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
