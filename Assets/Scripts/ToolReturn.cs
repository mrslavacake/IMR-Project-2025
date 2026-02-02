using UnityEngine;
using System.Collections;

public class ToolReturn : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    [Header("Return Settings")]
    public float returnDelay = 0.1f; //pause before return
    public bool smoothReturn = true;
    public float smoothSpeed = 5f;

    void Awake()
    {
        //save initial position
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    public void OnRelease()
    {
        StopAllCoroutines();
        StartCoroutine(ReturnToOrigin());
    }

    private IEnumerator ReturnToOrigin()
    {
        yield return new WaitForSeconds(returnDelay);

        if (smoothReturn)
        {
            while (Vector3.Distance(transform.position, initialPosition) > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * smoothSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * smoothSpeed);

                //no residual speed if kinematic
                if (rb != null && !rb.isKinematic)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }

                yield return null;
            }
        }

        //final snap for precision
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}