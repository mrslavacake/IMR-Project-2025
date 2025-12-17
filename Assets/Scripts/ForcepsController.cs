using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ForcepsController : MonoBehaviour
{
    [Header("Componente Forceps")]
    public Transform leftArm;
    public Transform rightArm;
    public Transform grabPoint; // Obiectul gol (Empty) unde va face snap organul

    [Header("Setari Rotatie")]
    public Vector3 rotationAxis = Vector3.right;
    public float maxOpenAngle = 25f;
    public float rotationSpeed = 100f; 

    [Header("Input Control (Button Actions)")]
    public InputActionProperty openAction;  // Butonul A 
    public InputActionProperty closeAction; // Butonul B

    [Header("Logica Prindere")]
    public string targetTag = "Organ";
    public float grabRadius = 0.02f;

    private GameObject grabbedObject = null;
    private float currentAngle = 0f;

    void Awake()
    {
        if (openAction.action != null) openAction.action.Enable();
        if (closeAction.action != null) closeAction.action.Enable();
    }

    void Update()
    {
        HandleMovement();

        bool isOpening = openAction.action != null && openAction.action.IsPressed();
        bool isClosing = closeAction.action != null && closeAction.action.IsPressed();

        if (isClosing && grabbedObject == null && currentAngle < 5f)
        {
            TryGrab();
        }

        if (isOpening && grabbedObject != null)
        {
            Release();
        }
    }

    private void HandleMovement()
    {
        if (openAction.action == null || closeAction.action == null) return;

        if (openAction.action.IsPressed())
        {
            currentAngle = Mathf.MoveTowards(currentAngle, maxOpenAngle, rotationSpeed * Time.deltaTime);
        }
        else if (closeAction.action.IsPressed())
        {
            currentAngle = Mathf.MoveTowards(currentAngle, 0f, rotationSpeed * Time.deltaTime);
        }

        leftArm.localRotation = Quaternion.AngleAxis(-currentAngle, rotationAxis);
        rightArm.localRotation = Quaternion.AngleAxis(currentAngle, rotationAxis);
    }

    private void TryGrab()
    {
        Collider[] hits = Physics.OverlapSphere(grabPoint.position, grabRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag(targetTag))
            {
                Grab(hit.gameObject);
                break;
            }
        }
    }

    private void Grab(GameObject target)
    {
        grabbedObject = target;

        XRGrabInteractable interactable = target.GetComponent<XRGrabInteractable>();
        if (interactable != null) interactable.enabled = false;

        target.transform.SetParent(grabPoint);
        target.transform.localPosition = Vector3.zero;

        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void Release()
    {
        if (grabbedObject == null) return;

        grabbedObject.transform.SetParent(null);

        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

        XRGrabInteractable interactable = grabbedObject.GetComponent<XRGrabInteractable>();
        if (interactable != null) interactable.enabled = true;

        grabbedObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        if (grabPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(grabPoint.position, grabRadius);
        }
    }
}