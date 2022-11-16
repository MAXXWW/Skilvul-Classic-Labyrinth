using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        var touch = Input.GetTouch(0);

        // percobaan swipe
        if (touch.deltaPosition.x > 10)
        {
            Debug.Log("Right");
        }
        else if (touch.deltaPosition.x < -10)
        {
            Debug.Log("Left");
        }

        // Tap
        if (touch.tapCount > 0)
        {
            Debug.Log(touch.tapCount);
        }

        // switch (touch.phase)
        // {
        //     case TouchPhase.Began:
        //         Debug.Log("Began");
        //         break;
        //     case TouchPhase.Stationary:
        //         Debug.Log("Stationary");
        //         break;
        //     case TouchPhase.Moved:
        //         Debug.Log("Moved");
        //         break;
        //     case TouchPhase.Ended:
        //         Debug.Log("Ended");
        //         break;
        //     case TouchPhase.Canceled:
        //         Debug.Log("Canceled");
        //         break;
        //     default:
        //         break;
        // }
    }

    private void OnDrawGizmos()
    {
        foreach (var touch in Input.touches)
        {
            var touchWorldPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchWorldPos.z = 0;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Began");
                    Gizmos.color = Color.green;
                    break;
                case TouchPhase.Stationary:
                    Debug.Log("Stationary");
                    Gizmos.color = Color.red;
                    break;
                case TouchPhase.Moved:
                    Debug.Log("Moved");
                    Gizmos.color = Color.black;
                    break;
                case TouchPhase.Ended:
                    Debug.Log("Ended");
                    Gizmos.color = Color.blue;
                    break;
                case TouchPhase.Canceled:
                    Debug.Log("Canceled");
                    Gizmos.color = Color.yellow;
                    break;
                default:
                    break;
            }
            Gizmos.DrawSphere(touchWorldPos, 0.5f);
        }
    }
}
