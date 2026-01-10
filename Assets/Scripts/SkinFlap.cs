using UnityEngine;
using UnityEngine.InputSystem;

public class SkinFlap : MonoBehaviour
{
    private FrogDissectionManager manager;
    private bool isToolInside = false;

    [Header("Input Settings")]
    public InputActionProperty activateAction;

    void Start()
    {
        manager = Object.FindAnyObjectByType<FrogDissectionManager>();
    }

    void Update()
    {
        if (isToolInside)
        {
            float triggerValue = activateAction.action.ReadValue<float>();

            if (triggerValue > 0.5f)
            {
                Debug.Log($"[DISSECTION] Trigger pressed while tool is in {gameObject.name}!");
                manager.FlipFlap(this.gameObject);
                isToolInside = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tool"))
        {
            Debug.Log($"[DISSECTION] Tool entered {gameObject.name}");
            isToolInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tool"))
        {
            Debug.Log($"[DISSECTION] Tool exited {gameObject.name}");
            isToolInside = false;
        }
    }
}