using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Drag : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public bool isCollidingTop = false;
    private bool isCollidingBottom = false;
    private float colPosY;

    public GameObject division;
    public bool isUnlocked = false;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(transform.position.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(transform.position.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

        if (isCollidingBottom || isCollidingTop)
        {
            if (isCollidingBottom && (curPosition.y <= colPosY) ||
                isCollidingTop && (curPosition.y >= colPosY)) curPosition.y = colPosY;
        }

        transform.position = curPosition;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LockUpper" || (isUnlocked && other.gameObject.tag == "PinUpper")) isCollidingTop = true;
        if (other.gameObject.tag == "LockLower") isCollidingBottom = true;

        if (isCollidingTop || isCollidingBottom) GetPosY(transform);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isUnlocked && other.gameObject.tag == "PinUpper") isCollidingTop = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isCollidingTop = false;
        isCollidingBottom = false;
    }

    public void GetPosY(Transform newPosY)
    {
        colPosY = newPosY.position.y;
    }
}
