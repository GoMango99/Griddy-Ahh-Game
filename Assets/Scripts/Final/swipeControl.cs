using UnityEngine;

public class swipeControl : MonoBehaviour
{
    [Header("Lane Settings")]
    [Tooltip("The number of lanes.")] [Range(1, 10)] public int numLanes = 3; //
    [Tooltip("The width of each lane.")] public float laneWidth = 2.0f; 

    [Header("Swipe Settings")]
    [Tooltip("The minimum distance the player needs to swipe to change lanes.")] public float minSwipeDist = 50.0f; //

    [Header("Jump Settings")]
    [Tooltip("The force applied to the player when jumping.")] public float jumpForce = 10.0f; //
    [Tooltip("The distance from the player's center to the ground.")] public float groundDistance = 0.5f; //
    [Tooltip("The layer mask used for ground detection.")] public LayerMask groundMask; //
    [Tooltip("The object which detets whether the player is touching the ground or not")] [SerializeField] Transform groundCheck;

    [Header("Player Movement Settings")]

    [Tooltip("The players constant forewards movement.")] [Range(1f, 100f)] float speed = 12f;


    [Header("Keycodes")]

    [Tooltip("The left movement key")] [SerializeField] KeyCode leftKey = KeyCode.A;
    [Tooltip("The right movement key")] [SerializeField] KeyCode rightKey = KeyCode.D;
    [Tooltip("The jump button")] [SerializeField] KeyCode jumpKey = KeyCode.Space;


    // The current lane index
    private int currentLane = 0;

    // The target lane index (set by swipes or button presses)
    private int targetLane = 0;

    // The position of the player at the start of the swipe
    private Vector2 startSwipePos;

    // Whether the player is currently grounded
    [SerializeField]private bool isGrounded = false;


    Rigidbody rb;


    void Start(){
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Check for swipe input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startSwipePos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDist = touch.position - startSwipePos;

                if (swipeDist.magnitude > minSwipeDist)
                {
                    // Determine the direction of the swipe
                    int swipeDir = (int)Mathf.Sign(swipeDist.x);

                    // Move to the next lane in the swipe direction
                    targetLane = currentLane + swipeDir;

                    // Clamp the target lane index to the valid range
                    targetLane = Mathf.Clamp(targetLane, 0, numLanes - 1);
                }
                else
                {
                    // Jump if the player taps the screen
                    if (isGrounded)
                    {
                        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        isGrounded = false;
                    }
                }
            }
        }


        if(Input.GetKeyDown(jumpKey)){
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        // Check for keyboard input
        if (Input.GetKeyDown(leftKey))
        {
            // Move to the previous lane
            targetLane = currentLane - 1;

            // Clamp the target lane index to the valid range
            targetLane = Mathf.Clamp(targetLane, 0, numLanes - 1);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            // Move to the next lane
            targetLane = currentLane + 1;

            // Clamp the target lane index to the valid range
            targetLane = Mathf.Clamp(targetLane, 0, numLanes - 1);
        }
        
        // Move the player forward
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Move towards the target lane
        float targetX = (targetLane - (numLanes - 1) * 0.5f) * laneWidth;
        float newX = Mathf.MoveTowards(transform.position.x, targetX, Time.deltaTime * 10.0f);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Update the current lane index
        if (Mathf.Abs(transform.position.x - targetX) < 0.1f)
        {
            currentLane = targetLane;
        }
    }



    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }




}

