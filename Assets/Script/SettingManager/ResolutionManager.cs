using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionManager : MonoBehaviour
{

    public TMP_Dropdown ResDropdown;
    public Toggle FullScreenToggle;

    Resolution[] AllResolutions;
    bool IsFullScreen;
    int SelectedResolution;
    List<Resolution> SelectedResolutionList = new List <Resolution>();
    // Start is called before the first frame update
    void Start()
    {
        IsFullScreen = true;
        AllResolutions = Screen.resolutions;

        List<string> resolutionStringList = new List<string>();
        string NewRes;
        foreach (Resolution res in AllResolutions)
        {
            NewRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionStringList.Contains(NewRes))
            {
                resolutionStringList.Add(NewRes);
                SelectedResolutionList.Add(res);
            }
        }

        ResDropdown.AddOptions(resolutionStringList);
    }

    public void ChangeResolution()
    {
        SelectedResolution = ResDropdown.value;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void ChangeFullScreen()
    {
        IsFullScreen = FullScreenToggle.isOn;
        Screen.SetResolution(SelectedResolutionList[SelectedResolution].width, SelectedResolutionList[SelectedResolution].height, IsFullScreen);
        AudioManager.Instance.PlaySFX("Button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
