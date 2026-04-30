using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Transform camTransform;

    void Start()
    {
        // Cache the main camera's transform so we aren't searching for it every frame
        if (Camera.main != null)
        {
            camTransform = Camera.main.transform;
        }
    }

    // We use LateUpdate so the UI rotates AFTER the camera has finished moving for the frame
    void LateUpdate()
    {
        if (camTransform == null) return;

        // Makes the UI exactly parallel to the camera screen. 
        // This is usually best for UI so it doesn't look skewed at the edges of the screen.
        transform.forward = camTransform.forward;

        // --- ALTERNATIVE ---
        // If you want the UI to pivot to look directly at the camera's center position point:
        // transform.LookAt(transform.position + camTransform.rotation * Vector3.forward, camTransform.rotation * Vector3.up);
    }
}