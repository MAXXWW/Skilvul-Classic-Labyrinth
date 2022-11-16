using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject checkPoint;
    [SerializeField] GameObject key;
    [SerializeField] GameObject padLock;
    [SerializeField] AudioSource sfxCheckPoint;
    [SerializeField] AudioSource sfxGetKey;
    public Vector3 Position => rb.position;
    public bool IsMoving => rb.velocity != Vector3.zero;
    [SerializeField] Vector3 lastPosition;
    public bool IsTeleporting => isTeleporting;
    bool isTeleporting;

    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        lastPosition = this.transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Out")
        {
            // teleport
            StopAllCoroutines();
            StartCoroutine(DeleyedTeleport());
        }
    }

    IEnumerator DeleyedTeleport()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(1);
        // rb.isKinematic = true;
        this.transform.position = lastPosition;
        isTeleporting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            sfxCheckPoint.Play();
            lastPosition = checkPoint.transform.position;
            Destroy(checkPoint);
        }

        if (other.gameObject.CompareTag("Key"))
        {
            sfxGetKey.Play();
            Destroy(key);
            Destroy(padLock);
        }
    }
}
