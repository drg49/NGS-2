using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenuObjectivesController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject objectivesPanel;       // Panel parent
    [SerializeField] private Transform objectiveListParent;    // Where objectives are instantiated
    [SerializeField] private GameObject objectivePrefab;       // ObjectiveItem prefab
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = new Color(1, 1, 1, 0.4f);

    private List<ObjectivesItem> objectiveItems = new List<ObjectivesItem>();
    private int activeIndex = 0;

    /// <summary>
    /// Sets the objectives and instantiates them under the parent.
    /// </summary>
    public void SetObjectives(List<string> objectives, int startIndex = 0)
    {
        ClearObjectives();
        activeIndex = startIndex;

        foreach (string obj in objectives)
        {
            if (objectivePrefab == null)
            {
                Debug.LogError("Objective prefab is not assigned!");
                return;
            }

            GameObject go = Instantiate(objectivePrefab, objectiveListParent);

            ObjectivesItem item = go.GetComponent<ObjectivesItem>();
            if (item != null)
            {
                item.SetText(obj);
                objectiveItems.Add(item);
            }
            else
            {
                Debug.LogError("ObjectivesItem script missing on prefab!");
            }
        }

        UpdateObjectiveDisplay();
    }


    /// <summary>
    /// Clears all current objectives.
    /// </summary>
    private void ClearObjectives()
    {
        foreach (Transform child in objectiveListParent)
            Destroy(child.gameObject);

        objectiveItems.Clear();
    }

    /// <summary>
    /// Updates the colors to show which objective is active.
    /// </summary>
    private void UpdateObjectiveDisplay()
    {
        for (int i = 0; i < objectiveItems.Count; i++)
        {
            TMP_Text tmp = objectiveItems[i].GetTextComponent();
            if (tmp != null)
                tmp.color = (i == activeIndex) ? activeColor : inactiveColor;
        }
    }

    /// <summary>
    /// Sets which objective is active.
    /// </summary>
    public void SetActiveObjective(int index)
    {
        if (index < 0 || index >= objectiveItems.Count) return;

        activeIndex = index;
        UpdateObjectiveDisplay();
    }

    /// <summary>
    /// Shows or hides the objectives panel.
    /// </summary>
    public void ShowPanel(bool show)
    {
        objectivesPanel.SetActive(show);
    }
}
