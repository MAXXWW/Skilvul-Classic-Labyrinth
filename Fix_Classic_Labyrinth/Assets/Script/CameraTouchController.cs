using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTouchController : MonoBehaviour
{
    [SerializeField, Range(0, 20)] float filterFactor = 10;
    [SerializeField, Range(0, 1)] float zoomFactor = 0.2f;

    [Tooltip("equal camera y position")]
    [SerializeField] float minCameraPos = 70;
    [SerializeField] float maxCameraPos = 170;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] float maxZ;
    float distance;
    Vector3 touchBeganWorldPos;
    Vector3 cameraBeganWorldPos;

    void Start()
    {
        distance = this.transform.position.y;
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        var touch0 = Input.GetTouch(0);

        // simpan posisi awal tapi posisi realworld
        if (touch0.phase == TouchPhase.Began)
        {
            touchBeganWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch0.position.x, touch0.position.y, distance));
            Debug.Log(touchBeganWorldPos);
            cameraBeganWorldPos = this.transform.position;
        }

        // atur posisi sekarang sesuai perubahan posisi Began
        if (Input.touchCount == 1 && touch0.phase == TouchPhase.Moved)
        {
            // posisi touch (world space) saat ini
            var touchMoveWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch0.position.x, touch0.position.y, distance));
            // perbedaan posisi world space
            var delta = touchMoveWorldPos - touchBeganWorldPos;
            var targetPos = cameraBeganWorldPos - delta;
            // TODO clamp targetPos
            targetPos = new Vector3(Mathf.Clamp(targetPos.x, minX, maxX), targetPos.y, Mathf.Clamp(targetPos.z, minZ, maxZ));
            // menggunakan Lerp (Linear interpolation) sebagai filter agar movement smooth
            this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * filterFactor);
        }

        if (Input.touchCount < 2)
        {
            return;
        }

        var touch1 = Input.GetTouch(1);

        if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
        {
            var touch0PrevPos = touch0.position - touch0.deltaPosition;
            var touch1PrevPos = touch1.position - touch1.deltaPosition;
            var prevDistance = Vector3.Distance(touch0PrevPos, touch1PrevPos);
            var currentDistance = Vector3.Distance(touch0.position, touch1.position);
            var delta = currentDistance - prevDistance;

            this.transform.position -= new Vector3(0, delta * zoomFactor, 0);

            // batasi zoom
            this.transform.position = new Vector3(this.transform.position.x, Mathf.Clamp(this.transform.position.y, minCameraPos, maxCameraPos), this.transform.position.z);
            distance = this.transform.position.y;
        }
    }
}
