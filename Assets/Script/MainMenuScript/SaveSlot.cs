using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private TextMeshProUGUI XPText;

    [Header("Delete Data Button")]
    [SerializeField] private Button deleteButton;

    public bool hasData {get; private set; } = false;

    private Button saveSlotButton;

    private void Awake() {
        saveSlotButton = this.GetComponent<Button>();
    }

    public void SetData(GameData data)
    {
        //there's no data for this profileId
        if (data == null)
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            deleteButton.gameObject.SetActive(false);
        }
        else
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            deleteButton.gameObject.SetActive(true);

            LevelText.text = "Lv. " + data.currentLevel;
            XPText.text = "XP. " + data.currentXP;
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable)
    {
        saveSlotButton.interactable = interactable;
        deleteButton.interactable = interactable;
    }
}
