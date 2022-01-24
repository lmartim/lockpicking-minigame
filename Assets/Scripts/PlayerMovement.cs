using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public List<GameObject> pinsPositions;

    private int totalPositions;
    private int currentPosition = 0;

    private void Awake()
    {
        totalPositions = pinsPositions.Count;
        transform.position = pinsPositions[0].transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) MovePick(1);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MovePick(-1);
    }

    private void MovePick(int newPosition)
    {
        currentPosition += newPosition;

        if (currentPosition < 0) currentPosition = totalPositions - 1;
        if (currentPosition > totalPositions - 1) currentPosition = 0;

        transform.position = new Vector3(pinsPositions[currentPosition].transform.position.x, transform.position.y, transform.position.z);
    }
}
