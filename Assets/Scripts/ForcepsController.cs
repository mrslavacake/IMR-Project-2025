using UnityEngine;
using UnityEngine.InputSystem; // Necesita noul sistem de Input System (Action-based)

public class ForcepsController : MonoBehaviour
{
    [Header("Componente Forceps")]
    public Transform leftArm; // Jumatatea stanga a forcepsului
    public Transform rightArm; // Jumatatea dreapta a forcepsului

    [Header("Setari Rotatie")]
    [Tooltip("Axa locala de rotatie (ex: Vector3.right sau Vector3.up)")]
    public Vector3 rotationAxis = Vector3.right;
    public float maxOpenAngle = 30f; // Unghiul maxim de deschidere

    [Header("Input Control")]
    [Tooltip("Action-ul care controleaza deschiderea/inchiderea (ex: XRI RightHand/Grip)")]
    public InputActionProperty gripAction;

    [Header("Logica Prindere")]
    [Tooltip("Tag-ul pe care il au organele (ex: Organ)")]
    public string targetTag = "Organ";
    public float grabThreshold = 0.1f; // Valoarea sub care se considera ca forta este suficienta (ex. 10% deschis)

    private GameObject grabbedObject = null;
    private float currentGripValue = 0f;

    void Awake()
    {
        // Activeaza input action-ul la pornire
        gripAction.action.Enable();
    }

    void Update()
    {
        // 1. Obtine valoarea input-ului (de la 0 la 1)
        currentGripValue = gripAction.action.ReadValue<float>();

        // 2. Calculeaza unghiul de rotatie interpolat
        // Valoarea 1.0f -> forcepsul complet DESCHIS (maxOpenAngle)
        // Valoarea 0.0f -> forcepsul complet iNCHIS (0 grade)
        float currentAngle = currentGripValue * maxOpenAngle;

        // 3. Aplica rotatia pe cele doua brate
        // Nota: Presupunem ca rotatiile sunt pe axa locala a fiecarui brat.
        leftArm.localRotation = Quaternion.AngleAxis(currentAngle, rotationAxis);
        rightArm.localRotation = Quaternion.AngleAxis(-currentAngle, rotationAxis); // Rotatie in sens opus

        // 4. Gestioneaza prinderea/eliberarea
        HandleGrabbing();
    }

    // Aceasta functie detecteaza organele si le prinde/elibereaza
    private void HandleGrabbing()
    {
        // Verifica daca ar trebui sa eliberezi obiectul
        if (grabbedObject != null && currentGripValue >= grabThreshold)
        {
            ReleaseObject();
        }

        // Daca nu tine nimic si se inchide, incearca sa prinda
        else if (grabbedObject == null && currentGripValue < grabThreshold)
        {
            TryGrabObject();
        }
    }

    // Foloseste un Collider care este IsTrigger pe unul din varfuri pentru a detecta un organ
    private void TryGrabObject()
    {
        // Foloseste SphereCast sau BoxCast de la varfuri pentru a detecta obiecte,
        // sau foloseste un Collider mare pe varf care este IsTrigger

        // in acest exemplu, vom folosi o detectie simpla bazata pe o zona definita de un Collider
        // Trebuie sa ai un Collider IsTrigger pe unul din brate (LeftArm sau RightArm)

        // Pentru simplitate, presupunem ca un trigger a marcat deja un obiect ca fiind 'potentialTarget'
        // Cea mai simpla metoda este sa folosesti OnCollisionEnter sau OnTriggerEnter pe un Collider
        // Atasat de bratul stang (leftArm) pentru a seta un 'potentialGrabTarget'.

        // *** SOLUtIE SIMPLIFICATa (NEVOIE DE COLIDER PE UN BRAt) ***
        Collider[] hitColliders = Physics.OverlapSphere(leftArm.position, 0.02f); // 0.02f = raza mica de detectie

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(targetTag) && hitCollider.gameObject != grabbedObject)
            {
                GrabObject(hitCollider.gameObject);
                return;
            }
        }
    }

    private void GrabObject(GameObject target)
    {
        grabbedObject = target;

        // 1. Seteaza ca obiect copil al forcepsului
        target.transform.SetParent(transform);

        // 2. Opreste fizica pe organ (daca are Rigidbody)
        Rigidbody rb = target.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void ReleaseObject()
    {
        // 1. Anuleaza relatia de obiect copil
        grabbedObject.transform.SetParent(null);

        // 2. Reactiveaza fizica (pentru a-l lasa sa cada in tava)
        Rigidbody rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        grabbedObject = null;
    }
}