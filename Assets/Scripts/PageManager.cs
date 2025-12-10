using UnityEngine;
using TMPro;
using System.Text;
using System.Linq;

public class PageManager : MonoBehaviour
{
    [Header("Setup")]
    public TextMeshProUGUI pageTextDisplay;
    public TextMeshProUGUI taskListDisplay;
    public Animator notebookAnimator;
    public Collider pageTurnCollider;

    [Header("Behavior Settings")]
    public bool allowFreePageTurn = false;

    [Header("Instructions")]
    public Page[] pages;

    [Header("State")]
    private int currentPageIndex = 0;
    private Page currentPage;

    void Start()
    {
        if (pages.Length > 0)
        {
            SetPage(0);
        }
        if (notebookAnimator != null)
        {
            notebookAnimator.SetTrigger("Start");
        }
    }

    public void AdvancePage()
    {
        if (allowFreePageTurn || IsCurrentPageCompleted())
        {
            if (currentPageIndex < pages.Length - 1)
            {
                if (notebookAnimator != null)
                {
                    notebookAnimator.SetTrigger("TurnPage");
                }

                SetPage(currentPageIndex + 1);
            }
            else
            {
                pageTextDisplay.text = "Dissection finalised. Well done!";
                taskListDisplay.text = "";
                if (pageTurnCollider != null) pageTurnCollider.enabled = false;
            }
        }
    }

    private void SetPage(int index)
    {
        ToggleHints(false);

        currentPageIndex = index;
        currentPage = pages[index];

        pageTextDisplay.text = currentPage.pageTextHeader;

        foreach (var task in currentPage.tasks)
        {
            task.isCompleted = false;
        }

        UpdateHints();

        UpdateTaskListDisplay();

        if (pageTurnCollider != null)
        {
            pageTurnCollider.enabled = allowFreePageTurn || IsCurrentPageCompleted();
        }

        Debug.Log("Waiting for task: " + GetNextRequiredTask()?.requiredTask.ToString() + " on object: " + GetNextRequiredTask()?.targetObjectName);
    }

    private bool IsCurrentPageCompleted()
    {
        foreach (var task in currentPage.tasks)
        {
            if (!task.isCompleted)
            {
                return false;
            }
        }
        return true;
    }

    private TaskInstruction GetNextRequiredTask()
    {
        foreach (var task in currentPage.tasks)
        {
            if (!task.isCompleted)
            {
                return task;
            }
        }
        return null;
    }

    private void UpdateTaskListDisplay()
    {
        if (taskListDisplay == null) return;

        StringBuilder sb = new StringBuilder();

        foreach (var task in currentPage.tasks)
        {
            string checkmark = task.isCompleted ? "<color=#00FF00>✓</color>" : "<color=#FF0000>☐</color>";
            sb.AppendLine($"{checkmark} {task.taskDescription}");
        }

        taskListDisplay.text = sb.ToString();

        if (IsCurrentPageCompleted())
        {
            Debug.Log("Page done. Click to turn page.");
            if (pageTurnCollider != null) pageTurnCollider.enabled = true;
        }
    }

    private void UpdateHints()
    {
        ToggleHints(false);

        TaskInstruction nextTask = GetNextRequiredTask();
        if (nextTask != null && nextTask.hintObject != null)
        {
            nextTask.hintObject.SetActive(true);
        }
    }

    public void MarkTaskCompleted(TaskType completedTask, string objectName)
    {
        TaskInstruction nextRequiredTask = GetNextRequiredTask();

        
        if (nextRequiredTask != null &&
            nextRequiredTask.requiredTask == completedTask &&
            (string.IsNullOrEmpty(nextRequiredTask.targetObjectName) || nextRequiredTask.targetObjectName.Equals(objectName, System.StringComparison.OrdinalIgnoreCase)))
        {
            nextRequiredTask.isCompleted = true; 
            Debug.Log("Task " + completedTask + " on " + objectName + " COMPLETED.");

          
            UpdateTaskListDisplay();
            UpdateHints();

            TaskInstruction newlyNextTask = GetNextRequiredTask();
            if (newlyNextTask != null)
            {
                Debug.Log("Waiting for next task: " + newlyNextTask.requiredTask.ToString() + " on object: " + newlyNextTask.targetObjectName);
            }
            else
            {
                Debug.Log("All tasks on this page completed.");
            }
        }
        else
        {
            Debug.Log("Task (" + completedTask + " on " + objectName + ") was not completed in the required order or is incorrect.");
        }
    }

    private void ToggleHints(bool state)
    {
        if (currentPage != null)
        {
            foreach (var task in currentPage.tasks)
            {
                if (task.hintObject != null)
                {
                    task.hintObject.SetActive(state);
                }
            }
        }
    }
}