using UnityEngine;

public class TrayZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Organ"))
        {
            Debug.Log("Organ placed in tray: " + other.name);

            //deactivate rigidbody
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) rb.isKinematic = true;

            //parent organ to tray
            other.transform.SetParent(transform);
        }
    }
}
