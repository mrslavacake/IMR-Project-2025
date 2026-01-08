using UnityEngine;

public class SkinFlap : MonoBehaviour
{
    private FrogDissectionManager manager;

    void Start()
    {
        manager = Object.FindAnyObjectByType<FrogDissectionManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the forceps (Tool) touches the closed skin flap (E)
        if (other.CompareTag("Tool"))
        {
            manager.FlipFlap(this.gameObject);
        }
    }
}