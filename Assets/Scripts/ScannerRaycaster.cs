using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScannerRaycaster : MonoBehaviour
{
    [Header("Setup")]
    public Transform raycastOrigin;
    public TextMeshProUGUI scannerText;

    [Header("Interaction State")]
    public bool isGrabbed = false;

    [Header("Visuals")]
    public LineRenderer laserLine;

    [Header("Settings")]
    public float maxDistance = 5f;
    private const string DEFAULT_TEXT = "SCANNER READY";

    [Header("Highlight Settings")]
    public Material highlightMaterial;

    private GameObject lastHitObject = null;

    private Dictionary<Renderer, Material> originalMaterials = new Dictionary<Renderer, Material>();

    [Header("Filtering")]
    public string[] scanOnlyTags = { "Organ", "Tool", "BezierLink", "Clutter" }; //reintroduced Clutter

    public ProjectorManager projectorManager;
    private bool firstTimeGrabbed = false;

    void Start()
    {
        if (scannerText != null)
        {
            scannerText.text = DEFAULT_TEXT;
        }
    }

    public void SetGrabState(bool grabbed)
    {
        isGrabbed = grabbed;
        Debug.Log("Scanner Grab State: " + grabbed);
        if (grabbed && !firstTimeGrabbed)
        {
            firstTimeGrabbed = true;
            if (projectorManager != null) projectorManager.ActivateProjectorSystem();
        }
    }

    private GameObject GetTargetObject(GameObject hitObject)
    {
        Transform currentParent = hitObject.transform;
        while (currentParent != null)
        {
            if (System.Array.IndexOf(scanOnlyTags, currentParent.gameObject.tag) != -1)
            {
                return currentParent.gameObject; 
            }
            currentParent = currentParent.parent;
        }

        return null;
    }

    private void ApplyHighlight(GameObject target)
    {
        Renderer[] renderers = target.GetComponentsInChildren<Renderer>();

        originalMaterials.Clear();

        foreach (Renderer renderer in renderers)
        {
            if (renderer.enabled && renderer.gameObject != gameObject)
            {
                originalMaterials.Add(renderer, renderer.material);
                renderer.material = highlightMaterial;
            }
        }
        lastHitObject = target;
    }

    private void ResetHighlight()
    {
        if (lastHitObject == null) return;

        foreach (KeyValuePair<Renderer, Material> entry in originalMaterials)
        {
            Renderer renderer = entry.Key;
            Material originalMaterial = entry.Value;

            if (renderer != null)
            {
                renderer.material = originalMaterial;
            }
        }

        originalMaterials.Clear();
        lastHitObject = null;
    }

    void Update()
    {
        if (!isGrabbed)
        {
            laserLine.SetPosition(0, raycastOrigin.position);
            laserLine.SetPosition(1, raycastOrigin.position);

            if (lastHitObject != null)
            {
                ResetHighlight();
            }
            scannerText.text = DEFAULT_TEXT;
            return;
        }

        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject rawHitObject = hit.collider.gameObject;
            GameObject targetObject = GetTargetObject(rawHitObject);

            if (targetObject != null)
            {
              
                if (lastHitObject != null && lastHitObject != targetObject)
                {
                    ResetHighlight();
                }

                if (lastHitObject == null) 
                {
                    ApplyHighlight(targetObject);
                }

                scannerText.text = targetObject.name;

                ScannableObject scannable = targetObject.GetComponent<ScannableObject>();
                if (scannable != null && projectorManager != null)
                {
                    projectorManager.DisplayOrganDetails(scannable.info);
                }

                laserLine.SetPosition(0, raycastOrigin.position);
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                HandleNoHitOrIrrelevantHit(ray, maxDistance);
            }
        }
        else
        {
            HandleNoHitOrIrrelevantHit(ray, maxDistance);
        }
    }
    private void HandleNoHitOrIrrelevantHit(Ray ray, float distance)
    {
        if (lastHitObject != null)
        {
            ResetHighlight();
        }

        laserLine.SetPosition(0, raycastOrigin.position);
        laserLine.SetPosition(1, raycastOrigin.position + raycastOrigin.forward * distance);
        scannerText.text = DEFAULT_TEXT;
    }

}