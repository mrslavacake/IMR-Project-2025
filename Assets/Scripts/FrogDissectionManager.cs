using UnityEngine;
using System.Collections;

public class FrogDissectionManager : MonoBehaviour
{
    [Header("Incision Stages")]
    public GameObject[] incisionStages; // Stages A, B, C, D
    public GameObject[] cutTriggers;    // The 3 guide parallelepipeds

    [Header("Skin Flaps")]
    public GameObject[] closedFlaps;    // Meshes E
    public GameObject[] openedFlaps;    // Meshes F

    private int currentStage = 0;

    void Start()
    {
        InitializeDissection();
    }

    void InitializeDissection()
    {
        // Mesh A is visible, B-D are hidden
        for (int i = 0; i < incisionStages.Length; i++)
            incisionStages[i].SetActive(i == 0);

        // Hide all flaps initially
        foreach (GameObject flap in closedFlaps) flap.SetActive(false);
        foreach (GameObject flap in openedFlaps) flap.SetActive(false);

        UpdateTriggers();
    }

    public void OnCutPerformed()
    {
        if (currentStage < cutTriggers.Length)
        {
            // Disable the current stage mesh and enable the next one
            incisionStages[currentStage].SetActive(false);
            currentStage++;
            incisionStages[currentStage].SetActive(true);

            UpdateTriggers();

            // If we reached the final mesh (D), show the flaps (E)
            if (currentStage == 3)
            {
                foreach (GameObject flap in closedFlaps) flap.SetActive(true);
            }
        }
    }

    void UpdateTriggers()
    {
        for (int i = 0; i < cutTriggers.Length; i++)
        {
            // Only the trigger for the current step should be active
            bool isActiveTrigger = (i == currentStage);
            cutTriggers[i].SetActive(isActiveTrigger);

            if (isActiveTrigger)
                StartCoroutine(BlinkEffect(cutTriggers[i]));
        }
    }

    public void FlipFlap(GameObject closedFlap)
    {
        int index = System.Array.IndexOf(closedFlaps, closedFlap);
        if (index != -1)
        {
            closedFlaps[index].SetActive(false);
            openedFlaps[index].SetActive(true);
        }
    }

    IEnumerator BlinkEffect(GameObject target)
    {
        Renderer ren = target.GetComponent<Renderer>();
        if (ren == null) yield break;

        // Using material.color for simplicity, or _EmissionColor for glow
        Material mat = ren.material;
        mat.EnableKeyword("_EMISSION");

        while (target.activeSelf)
        {
            float lerp = Mathf.PingPong(Time.time * 2.0f, 1.0f);
            mat.SetColor("_EmissionColor", Color.cyan * lerp);
            yield return null;
        }
    }
}