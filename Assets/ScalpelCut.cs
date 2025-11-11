using UnityEngine;

public class ScalpelCut : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cuttable"))
        {
            //deactivate curb
            other.gameObject.SetActive(false);

            Debug.Log("Cut: " + other.name);
        }
    }
}
