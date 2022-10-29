using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick joystick;

    public ShootingJoystick shootingJoystick;
    [SerializeField]
    public float playerSpeed;
    private Rigidbody2D rb;
    Vector3 movementVector;
    Animate animate;

    public GameObject crosshair;

    public float timeToSpawn = 1f;
    public float FirstSpawn = 3f;

    public GameObject bulletPrefab;
    public float bulletSpeed = 2.0f;
    public bool canShoot = false;

    private bool isDashButtonDown;
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
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
        FirstSpawn -= Time.deltaTime;
        if (canShoot == true)
        {
            Shoot();
        }
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
            isDashButtonDown = false;
        }
    }
    
    void Shoot()
    {

        Vector2 shootingDirection = crosshair.transform.localPosition;
        shootingDirection.Normalize();

        if (FirstSpawn <= 0f)
        {
            GameObject arrow = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = shootingDirection * bulletSpeed;
            arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
            FirstSpawn = timeToSpawn;
        }


    }
}
