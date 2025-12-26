using UnityEngine;
using TMPro;

public class ObjectivesItem : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text objectiveText;

    public void SetText(string text)
    {
        if (objectiveText != null)
            objectiveText.text = text;
    }

    public TMP_Text GetTextComponent()
    {
        return objectiveText;
    }
}
