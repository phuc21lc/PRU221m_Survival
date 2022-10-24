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
    public float crosshairDistance = 1.0f;

    public float timeToSpawn = 1f;
    public float FirstSpawn = 3f;

    public GameObject bulletPrefab;
    public float bulletSpeed = 2.0f;
    public bool canShoot = false;
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
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        if (joystick.joystickVec.y != 0)
        {
            rb.velocity = new Vector2(joystick.joystickVec.x * playerSpeed, joystick.joystickVec.y * playerSpeed);
        } else
        {
            rb.velocity = Vector2.zero;
        }
        Shoot();
        
    }

    void Aim()
    {
        if(shootingJoystick.joystickVec != Vector2.zero)
        {
            crosshair.transform.localPosition = shootingJoystick.joystickVec * crosshairDistance;
        }
    }
    void Shoot()
    {
        if(canShoot == true)
        {
            Vector2 shootingDirection = crosshair.transform.localPosition;
            shootingDirection.Normalize();
            FirstSpawn -= Time.deltaTime;
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
}
