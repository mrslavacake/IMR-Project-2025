using UnityEngine;

public class InteractablePart : MonoBehaviour
{
    public enum PartType { IncisionTrigger, SkinFlap }
    public PartType type;

    private FrogDissectionManager manager;

    void Start()
    {
        manager = Object.FindAnyObjectByType<FrogDissectionManager>();
    }

    void OnMouseDown() // Detects click (or use OnMouseEnter for scalpel hover)
    {
        if (type == PartType.IncisionTrigger)
        {
            manager.OnCutPerformed();
        }
        else if (type == PartType.SkinFlap)
        {
            manager.FlipFlap(this.gameObject);
        }
    }
}