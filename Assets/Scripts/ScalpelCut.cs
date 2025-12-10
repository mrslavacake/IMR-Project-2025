using UnityEngine;

public class ScalpelCut : MonoBehaviour
{
    private PageManager pageManager;
    void Start()
    {
        pageManager = FindObjectOfType<PageManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cuttable"))
        {
            if (pageManager != null)
            {
                pageManager.MarkTaskCompleted(TaskType.Cut, other.name);
            }

            other.gameObject.SetActive(false);

            Debug.Log("Cut: " + other.name);
        }
    }
}