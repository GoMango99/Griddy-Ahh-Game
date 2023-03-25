using UnityEngine;

public class swipeDetector : MonoBehaviour
{
    private Vector2 fingerDown;
    private Vector2 fingerUp;
    private bool detectSwipeOnlyAfterRelease = false;

    public static bool swipeLeft;
    public static bool swipeRight;
    public static bool swipeUp;

    [SerializeField]
    private float SWIPE_THRESHOLD = 20f;

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDown = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {
        if (SwipeDistanceCheck())
        {
            if (fingerDown.x - fingerUp.x > 0)
            {
                swipeRight = false; // Change swipe direction
                swipeLeft = true;
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                swipeRight = true; // Change swipe direction
                swipeLeft = false;
            }
            else if (fingerDown.y - fingerUp.y > 0)
            {
                swipeUp = true;
            }
            fingerUp = fingerDown;
        }
        else
        {
            swipeRight = false;
            swipeLeft = false;
            swipeUp = false;
        }
    }

    bool SwipeDistanceCheck()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x) > SWIPE_THRESHOLD || Mathf.Abs(fingerDown.y - fingerUp.y) > SWIPE_THRESHOLD;
    }
}
