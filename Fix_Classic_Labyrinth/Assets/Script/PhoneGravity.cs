using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGravity : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float gravityMagnitude;
    bool UseGyro;
    Vector3 gravityDir;

    // memeriksa apakah pada perangkat target terdapat gyroscope
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            UseGyro = true;
            Input.gyro.enabled = true;
        }
    }

    // Input akan mengambil dari sensor yang tersedia (accelerometer/gyroscope)
    void Update()
    {
        var inputDir = UseGyro ? Input.gyro.gravity : Input.acceleration;
        gravityDir = new Vector3(inputDir.x, inputDir.z, inputDir.y);
    }

    // memberikan force pada gameobject agar dapat bergerak sesuai dengan arah smartphone
    private void FixedUpdate()
    {
        rb.AddForce(gravityDir * ((byte)gravityMagnitude), ForceMode.Acceleration);
    }

    public void SetGravityMagnitude(float gravity)
    {
        gravityMagnitude = gravity;
    }
}
