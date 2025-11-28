using UnityEngine;

public class BezierCutter : MonoBehaviour
{
    [Header("Setari Taiere")]
    [Tooltip("Tag-ul pe care il au legaturile Bezier (curbele)")]
    public string targetTag = "BezierLink";

    // Particule optionale pentru un efect vizual (ex. "sange" sau "taiere")
    public GameObject cutEffectPrefab;

    // Functia este apelata cand un Collider (Is Trigger) intra in contact cu un alt Collider
    private void OnTriggerEnter(Collider other)
    {
        // 1. Verifica daca obiectul cu care am intrat in contact este o legatura
        if (other.CompareTag(targetTag))
        {
            // 2. (Optional) Instantiaza un efect vizual la locul taieturii
            if (cutEffectPrefab != null)
            {
                // Instantiaza particulele la punctul de contact
                Instantiate(cutEffectPrefab, other.ClosestPoint(transform.position), Quaternion.identity);
            }

            // 3. Distruge obiectul legaturii (curba Bezier)
            Destroy(other.gameObject);

            // 4. (Optional) Adauga logica pentru a modifica starea organului
            // Ex: Daca organul are un "IsAttached" flag, seteaza-l pe false aici.
        }
    }
}