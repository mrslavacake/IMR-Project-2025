using UnityEngine;
using System.Collections;

public class FrogDissectionManager : MonoBehaviour
{
    [Header("Incision Stages")]
    public GameObject[] incisionStages; 
    public GameObject[] cutTriggers;    

    [Header("Skin Flaps")]
    public GameObject[] closedFlaps;   
    public GameObject[] openedFlaps;    

    private int currentStage = 0;

    void Start()
    {
        InitializeDissection();
    }

    void InitializeDissection()
    {
        for (int i = 0; i < incisionStages.Length; i++)
            incisionStages[i].SetActive(i == 0);

        foreach (GameObject flap in closedFlaps) flap.SetActive(false);
        foreach (GameObject flap in openedFlaps) flap.SetActive(false);

        UpdateTriggers();
    }

    public void OnCutPerformed()
    {
        if (currentStage < cutTriggers.Length)
        {
            incisionStages[currentStage].SetActive(false);

            currentStage++;

            if (currentStage < incisionStages.Length)
            {
                incisionStages[currentStage].SetActive(true);
            }
            else if (currentStage == cutTriggers.Length)
            {
                foreach (GameObject flap in closedFlaps) flap.SetActive(true);
            }

            UpdateTriggers();
        }
    }

    void UpdateTriggers()
    {
        for (int i = 0; i < cutTriggers.Length; i++)
        {
            bool isActiveTrigger = (i == currentStage);
            cutTriggers[i].SetActive(isActiveTrigger);

            if (isActiveTrigger)
            {
                StopAllCoroutines(); 
                StartCoroutine(BlinkEffect(cutTriggers[i]));
            }
        }
    }

    public void FlipFlap(GameObject closedFlap)
    {
        int index = System.Array.IndexOf(closedFlaps, closedFlap);
        if (index != -1)
        {
            Debug.Log($"[MANAGER] Replace {closedFlap.name} with {openedFlaps[index].name}");
            closedFlap.SetActive(false);
            openedFlaps[index].SetActive(true);
            // SOUND!!!!!!!!!!!
        }
        else
        {
            Debug.LogError($"[MANAGER] Object {closedFlap.name} not found in closedFlaps!");
        }
    }

    IEnumerator BlinkEffect(GameObject target)
    {
        Renderer ren = target.GetComponent<Renderer>();
        if (ren == null) yield break;
        Material mat = ren.material;

        mat.EnableKeyword("_EMISSION");

        while (target.activeSelf)
        {
            float lerp = Mathf.PingPong(Time.time * 2.0f, 1.0f);
            // Color.cyan * intensity
            mat.SetColor("_EmissionColor", Color.cyan * lerp * 2.0f);
            yield return null;
        }
    }
}