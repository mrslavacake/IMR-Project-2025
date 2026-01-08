using UnityEngine;

public class IncisionTrigger : MonoBehaviour
{
    private FrogDissectionManager manager;
    private bool hasBeenCut = false;

    void Start()
    {
        // Find the manager in the scene
        manager = Object.FindAnyObjectByType<FrogDissectionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "Tool"
        // and if we haven't already processed this cut
        if (other.CompareTag("Tool") && !hasBeenCut)
        {
            hasBeenCut = true;
            manager.OnCutPerformed();

            // Optional: Add a haptic pulse for VR controllers here
            // or play a cutting sound
        }
    }
}