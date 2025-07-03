using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;  // Import the Vuforia namespace

public class WalkAround : MonoBehaviour
{
    public float speed = 150.0f;  // Movement speed
    private Rigidbody rb;  // Rigidbody for physics-based movement
    private bool isTracking = false;  // Track if the image target is currently tracked

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Register the tracking event callback
        ObserverBehaviour observer = GetComponentInParent<ObserverBehaviour>();
        if (observer != null)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void Update()
    {
        // Eğer hedef izleniyorsa, hareket et
        if (isTracking)
        {
            // Yumuşak ve sürekli hareket için AddForce kullanıyoruz
            Vector3 movement = transform.forward * speed;
            rb.AddForce(movement, ForceMode.Force);
        }
    }

    // Callback method for target status changes
    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            // Image target is detected or tracked
            isTracking = true;
        }
        else
        {
            // Image target is lost
            isTracking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            transform.Rotate(0, 180, 0);  // Rotate 180 degrees to turn around
        }
    }
}
