using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    public GameObject container;
    public GameObject pin;
    public GameObject upperLock;
    public GameObject lowerLock;
    public GameObject unlockPoint;

    private bool isUnlocked;
    private bool justUnlocked;

    private BoxCollider pinBoxCollider;
    private Vector3 lowerLockLocalScale;

    private Vector3 boxColliderOriginalScale;
    private Vector3 boxColliderOriginalCenter;

    private Coroutine unlockCoroutine;

    private void Awake()
    {
        pinBoxCollider = pin.GetComponent<BoxCollider>();
        lowerLockLocalScale = lowerLock.transform.localScale;

        boxColliderOriginalScale = pinBoxCollider.size;
        boxColliderOriginalCenter = pinBoxCollider.center;
    }

    private void Update()
    {
        var lerpedColor = Color.Lerp(Color.white, Color.yellow, Vector3.Distance(transform.position, unlockPoint.transform.position));

        //upperLock.GetComponent<Renderer>().material.color = lerpedColor;
        //lowerLock.GetComponent<Renderer>().material.color = lerpedColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Unlock" && !isUnlocked)
        {
            unlockCoroutine = StartCoroutine(TriggerUnlockTime());
        }

        if (other.gameObject.tag == "PinUpper" && isUnlocked && !justUnlocked)
            TriggerLock();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Unlock")
        {
            StopCoroutine(unlockCoroutine);

            if (isUnlocked)
                justUnlocked = false;
        }
    }

    IEnumerator TriggerUnlockTime()
    {
        yield return new WaitForSeconds(.2f);

        TriggerUnlock();
        isUnlocked = true;
        justUnlocked = true;
    }

    private void TriggerUnlock()
    {
        var bottomLockMultiplier = lowerLock.transform.localScale.y >= 1 ? 1 : -1;

        upperLock.transform.SetParent(container.transform);

        pinBoxCollider.size = new Vector3(pinBoxCollider.size.x, lowerLockLocalScale.y, pinBoxCollider.size.z);
        pinBoxCollider.center = new Vector3(pinBoxCollider.center.x, (lowerLockLocalScale.y/20) * bottomLockMultiplier, 0f);

        var pinDrag = pin.GetComponent<Drag>();
        pinDrag.isUnlocked = true;
        pinDrag.isCollidingTop = true;
        pinDrag.GetPosY(pin.transform);

        upperLock.GetComponent<BoxCollider>().enabled = true;
    }

    private void TriggerLock()
    {
        upperLock.transform.SetParent(pin.transform);

        pinBoxCollider.size = boxColliderOriginalScale;
        pinBoxCollider.center = boxColliderOriginalCenter;

        pin.GetComponent<Drag>().isUnlocked = false;

        upperLock.GetComponent<BoxCollider>().enabled = false;

        isUnlocked = false;
    }
}
