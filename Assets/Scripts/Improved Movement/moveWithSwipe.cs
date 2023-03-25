using UnityEngine;
using System.Collections;

public class moveWithSwipe : MonoBehaviour
{
    public float moveSpeed = 10f;
    public int laneCount = 3;
    public float laneDistance = 3f;
    public int currentLane = 1;
    public bool isMoving = false;
    private bool isSwitchingLanes = false;

    public float jumpForce = 10f;
    public float raycastDistance = 1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
{
    // Move forward at a consistent speed
    if (!isMoving)
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
    }

    // Move right
    if ((Input.GetKeyDown(KeyCode.D) || swipeDetector.swipeRight) && currentLane < laneCount && !isSwitchingLanes)
    {
        isMoving = true;
        isSwitchingLanes = true;
        currentLane--;
        Vector3 targetPosition = transform.position + Vector3.right * laneDistance;
        StartCoroutine(MoveToPosition(targetPosition, 0.5f));
    }

    // Move left
    if ((Input.GetKeyDown(KeyCode.A) || swipeDetector.swipeLeft) && currentLane > 1 && !isSwitchingLanes)
    {
        isMoving = true;
        isSwitchingLanes = true;
        currentLane++;
        Vector3 targetPosition = transform.position + Vector3.left * Mathf.Abs(laneDistance);
        StartCoroutine(MoveToPosition(targetPosition, 0.5f));
    }

    // Jump
    if (Input.GetKeyDown(KeyCode.Space) || swipeDetector.swipeUp)
    {
        Jump();
    }

    // Check for touch input on the screen
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            Jump();
        }
    }
}

void Jump()
{
    RaycastHit hit;
    if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
    {
        if (hit.collider.CompareTag("Ground"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}



    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
        isSwitchingLanes = false;
    }
}
