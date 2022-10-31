using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ShootingJoystick shootingJoystick;
    public float timeToSpawn = 1f;
    public float FirstSpawn = 3f;

    public GameObject bulletPrefab;
    public float bulletSpeed = 2.0f;
    public bool canShoot = false;

    public GameObject crosshair;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot == true)
        {
            FirstSpawn -= Time.deltaTime;
            Shoot();
        }
    }
    void Shoot()
    {
        if (FirstSpawn <= 0f)
        {
            GameObject arrow = Instantiate(bulletPrefab, crosshair.transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(shootingJoystick.joystickVec.x * bulletSpeed, shootingJoystick.joystickVec.y * bulletSpeed);
            //arrow.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            //Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
            //rb.AddForce(crosshair.transform.forward * bulletSpeed);
            Destroy(arrow, 2.0f);
            FirstSpawn = timeToSpawn;
        }
    }
}
