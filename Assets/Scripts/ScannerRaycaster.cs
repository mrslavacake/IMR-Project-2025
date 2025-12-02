using UnityEngine;
using TMPro;

public class ScannerRaycaster : MonoBehaviour
{
    [Header("Setup")]
    public Transform raycastOrigin;
    public TextMeshProUGUI scannerText;

    [Header("Settings")]
    public float maxDistance = 5f;
    private const string DEFAULT_TEXT = "SCANNER READY";

    void Start()
    {
        if (scannerText != null)
        {
            scannerText.text = DEFAULT_TEXT;
        }
    }

    void Update()
    {
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider != null)
            {
                string objectName = hit.collider.gameObject.name;

                scannerText.text = objectName;
            }
        }
        else
        {
            scannerText.text = DEFAULT_TEXT;
        }

        // Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * maxDistance, Color.red);
    }
}