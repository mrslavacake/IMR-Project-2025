using UnityEngine;

public class BezierCutter : MonoBehaviour
{
    [Header("Cutting Settings")]
    [Tooltip("Tag for Bezier curves")]
    public string targetTag = "BezierLink";

    public GameObject cutEffectPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            //(Optional)
            if (cutEffectPrefab != null)
            {
                Instantiate(cutEffectPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
            }

            PageManager.Instance.MarkTaskCompleted(TaskType.CutBezier, other.gameObject.name);
            Destroy(other.gameObject);

            //"IsAttached" flag false
        }
    }
}