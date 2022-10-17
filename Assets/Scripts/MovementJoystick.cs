using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOgPos;
    private float joystickRadius;
    // Start is called before the first frame update
    void Start()
    {
        joystickOgPos = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;

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
