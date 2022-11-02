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
    public GameObject player;

    public GameObject bulletPrefab;
    public float bulletSpeed = 2.0f;

    public float timeToSpawn = 1f;
    public float FirstSpawn = 3f;

    public GameObject gun;
    public GameObject gun2;
    public float crosshairDistance = 1.0f;

    public bool facingRight = true;
    Vector2 GameObjectRotation;
    private float GameObjectRotation2;
    private float GameObjectRotation3;
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
            GameObjectRotation = new Vector2(joystickVec.x, joystickVec.y);
            GameObjectRotation3 = GameObjectRotation.x;

            if (facingRight)
            {
                //Rotates the object if the player is facing right
                GameObjectRotation2 = GameObjectRotation.x + GameObjectRotation.y * 90;
                gun.transform.rotation = Quaternion.Euler(0f, 0f, GameObjectRotation2);
                gun2.transform.rotation = Quaternion.Euler(0f, 0f, GameObjectRotation2);
            }
            else
            {
                //Rotates the object if the player is facing left
                GameObjectRotation2 = GameObjectRotation.x + GameObjectRotation.y * -90;
                gun.transform.rotation = Quaternion.Euler(0f, 180f, -GameObjectRotation2);
                gun2.transform.rotation = Quaternion.Euler(0f, 180f, -GameObjectRotation2);
            }
            if (GameObjectRotation3 < 0 && facingRight)
            {
                // Executes the void: Flip()
                Flip();
            }
            else if (GameObjectRotation3 > 0 && !facingRight)
            {
                // Executes the void: Flip()
                Flip();
            }
        }
    }
    private void Flip()
    {
        // Flips the player.
        facingRight = !facingRight;

        //player.transform.Rotate(0, 180, 0);
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
        FindObjectOfType<Gun>().canShoot = true;
        //FindObjectOfType<Gun1>().canShoot = true;
        //Debug.Log("Drag");
    }
    public void PointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOgPos;
        joystickBG.transform.position = joystickOgPos;
        FindObjectOfType<Gun>().canShoot = false;
        
        //FindObjectOfType<Gun1>().canShoot = false;
        //Debug.Log("Up");
    }
}
