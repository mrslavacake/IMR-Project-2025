using UnityEngine;

public class PageTurnTrigger : MonoBehaviour
{
    public PageManager pageManager;

    private void Start()
    {
        if (pageManager == null)
        {
            pageManager = FindFirstObjectByType<PageManager>();
            //pageManager = FindAnyObjectByType<PageManager>();
        }
    }

    void OnMouseDown() 
    {
        if (pageManager != null)
        {
            pageManager.AdvancePage();
        }
    }
}