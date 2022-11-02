using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick joystick;

    [SerializeField]
    public float playerSpeed;
    private Rigidbody2D rb;
    Vector3 movementVector;
    Animate animate;
    public Animator animator;
    //public GameObject crosshair;

    //public float timeToSpawn = 1f;
    //public float FirstSpawn = 3f;

    //public GameObject bulletPrefab;
    //public float bulletSpeed = 2.0f;
    //public bool canShoot = false;

    [SerializeField] private LayerMask dashLayer;
    public bool isDashButtonDown;
    [SerializeField] float dashAmount = 50f;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animate = GetComponent<Animate>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (joystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(joystick.joystickVec.x * playerSpeed, joystick.joystickVec.y * playerSpeed);
            if (joystick.joystickVec.x > 0)
            {
                animator.SetBool("IsRight", true);
                animator.SetBool("IsLeft", false);
            }
            else if (joystick.joystickVec.x < 0)
            {
                animator.SetBool("IsLeft", true);
                animator.SetBool("IsRight", false);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("IsRight", false);
            animator.SetBool("IsLeft", false);
        }

        //if (canShoot == true)
        //{
        //    FirstSpawn -= Time.deltaTime;
        //    Shoot();
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
        Dash();
    }

    void Dash()
    {
        if (isDashButtonDown)
        {
            rb.velocity = new Vector2(joystick.joystickVec.x * dashAmount, joystick.joystickVec.y * dashAmount);
            Debug.Log("Dashed");
            isDashButtonDown = false;
        }
    }

    //void Shoot()
    //{

    //    if (FirstSpawn <= 0f)
    //    {
    //        GameObject arrow = Instantiate(bulletPrefab, crosshair.transform.position, Quaternion.identity);
    //        arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingJoystick.joystickVec.x * bulletSpeed, shootingJoystick.joystickVec.y * bulletSpeed);
    //        //arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
    //        //Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
    //        //rb.AddForce(crosshair.transform.forward * bulletSpeed);
    //        Destroy(arrow, 2.0f);
    //        FirstSpawn = timeToSpawn;
    //    }


    //}
}
