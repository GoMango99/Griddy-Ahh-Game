using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private bool isMoving = false;
    private Vector2 startingTouchPosition;
    private Vector2 currentTouchPosition;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                isMoving = true;
                startingTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                currentTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMoving = false;
            }
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Vector3 movement = new Vector3((currentTouchPosition.x - startingTouchPosition.x) * 0.02f, 0f, moveSpeed);

            transform.position += movement * Time.deltaTime;
        }
    }
}
