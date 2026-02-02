using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PageTurnTrigger : MonoBehaviour
{
    public PageManager pageManager;

    private void Start()
    {
        if (pageManager == null)
        {
            pageManager = FindFirstObjectByType<PageManager>();
        }
    }

    public void ExecuteAdvancePage()
    {
        if (pageManager != null)
        {
            Debug.Log("[VR] Attempting to turn page...");
            pageManager.AdvancePage();
        }
    }
}