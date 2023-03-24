using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 5f;              // The speed at which the player moves
    public float swipeThreshold = 50f;   // The minimum distance required for a swipe

    private bool isMoving = false;       // Tracks if the player is currently moving
    private Vector2 startPosition;       // The position where a swipe started

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // If the screen is being touched, track the starting position of the touch
                startPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                // If the screen is being swiped, calculate the swipe distance and direction
                Vector2 swipeDelta = touch.position - startPosition;
                float swipeDistance = swipeDelta.magnitude;

                if (swipeDistance >= swipeThreshold)
                {
                    // If the swipe distance is greater than the threshold, move the player on the Z-axis
                    float swipeAngle = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;
                    float zMovement = swipeAngle > 0 ? 1 : -1;
                    transform.position += new Vector3(zMovement, 0, 0) * speed * Time.deltaTime;
                }
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                // If the screen is being held down, move the player up on the X-axis
                isMoving = true;
            }
            else
            {
                // If the screen is not being touched or swiped, stop moving the player
                isMoving = false;
            }
        }
        else
        {
            // If there are no touches, stop moving the player
            isMoving = false;
        }

        if (isMoving)
        {
            // Move the player up on the X-axis
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
    }
}
