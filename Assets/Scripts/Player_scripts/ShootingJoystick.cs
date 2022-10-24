using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootingJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOgPos;
    private float joystickRadius;

    bool aiming = false;

    public GameObject bulletPrefab;
    public float bulletSpeed = 2.0f;

    public float timeToSpawn = 1f;
    public float FirstSpawn = 3f;

    public GameObject crosshair;
    public float crosshairDistance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        joystickOgPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;

    }
    void Aim()
    {
        if (joystickVec != Vector2.zero)
        {
            crosshair.transform.localPosition = joystickVec * crosshairDistance;
        }
    }
    void Shoot()
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
    public void PointerDown()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = -Camera.main.transform.position.z; // check position of camera property
        pos = Camera.main.ScreenToWorldPoint(pos);

        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
        
        //Debug.Log("Down");
    }
    
    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
        Aim();
        FindObjectOfType<Player>().canShoot = true;
        //Debug.Log("Drag");
    }
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOgPos;
        joystickBG.transform.position = joystickOgPos;
        //Debug.Log("Up");
    }
}
