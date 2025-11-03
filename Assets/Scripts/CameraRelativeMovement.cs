using UnityEngine;
using Unity.XR.CoreUtils;

public class CameraRelativeMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 2f;

    void Update()
    {
        if (cameraTransform == null) return;

        // Input
        float h = Input.GetAxis("Horizontal"); // A, D
        float v = Input.GetAxis("Vertical");   // W, S

        
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        Vector3 move = (forward * v + right * h) * moveSpeed * Time.deltaTime;

        transform.position += move;
    }
}
