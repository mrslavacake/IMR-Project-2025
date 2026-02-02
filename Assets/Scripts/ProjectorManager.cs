using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ProjectorManager : MonoBehaviour
{
    [Header("Hardware References")]
    public SkinnedMeshRenderer screenMesh; // Obiectul care are Shape Keys
    public int blendShapeIndex = 0;        // Indexul shape-ului de roll down
    public Transform projectorTransform;
    public float projectorLowerY = 2.5f;
    public float moveSpeed = 0.2f;

    [Header("UI References")]
    public Canvas projectionCanvas;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public Image organImage;
    public Sprite idleLogo;

    private bool isSystemActive = false;

    void Start()
    {
        projectionCanvas.gameObject.SetActive(false);
        if (screenMesh != null) screenMesh.SetBlendShapeWeight(blendShapeIndex, 100);
    }

    public void ActivateProjectorSystem()
    {
        if (isSystemActive) return;
        isSystemActive = true;

        StartCoroutine(DeployHardware());
    }

    private IEnumerator DeployHardware()
    {
        float progress = 0;
        Vector3 startPos = projectorTransform.position;
        Vector3 targetPos = new Vector3(startPos.x, projectorLowerY, startPos.z);

        while (progress < 1.0f)
        {
            progress += Time.deltaTime * moveSpeed;

            if (screenMesh != null)
            {
                // Lerp from 100 to 0 
                float currentWeight = Mathf.Lerp(100f, 0f, progress);
                screenMesh.SetBlendShapeWeight(blendShapeIndex, currentWeight);
            }

            projectorTransform.position = Vector3.Lerp(startPos, targetPos, progress);

            yield return null;
        }

        if (screenMesh != null) screenMesh.SetBlendShapeWeight(blendShapeIndex, 0f);
        projectorTransform.position = targetPos;

        projectionCanvas.gameObject.SetActive(true);
        titleText.text = "SCANNING SYSTEM ACTIVE";
        descriptionText.text = "Waiting for Data...";
        if (idleLogo != null)
        {
            organImage.sprite = idleLogo;
            organImage.enabled = true;
            //adjust color for transparency
            //organImage.color = Color.white;
        }
    }

    public void DisplayOrganDetails(OrganInfo info)
    {
        if (info == null || !isSystemActive) return;

        titleText.text = info.organName;
        descriptionText.text = info.detailedDescription;
        organImage.sprite = info.organIllustration;
        organImage.enabled = (info.organIllustration != null);
    }
}