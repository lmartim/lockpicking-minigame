using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    public GameObject pivot;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) && transform.rotation.z <= 0.14)
        {
            transform.RotateAround(pivot.transform.position, Vector3.forward, 15 * Time.deltaTime);
        } 
        else if (transform.rotation.z > 0)
        {
            transform.RotateAround(pivot.transform.position, Vector3.back, 15 * Time.deltaTime);

            if (transform.rotation.z < 0) transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
