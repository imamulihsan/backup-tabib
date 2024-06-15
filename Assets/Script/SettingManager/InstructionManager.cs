using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public GameObject Page1, Page2, Page3;


    public void Page1Panel()
    {
        Page1.SetActive(true);
        Page2.SetActive(false);
        Page3.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void Page2Panel()
    {
        Page1.SetActive(false);
        Page2.SetActive(true);
        Page3.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void Page3Panel()
    {
        Page1.SetActive(false);
        Page2.SetActive(false);
        Page3.SetActive(true);
        AudioManager.Instance.PlaySFX("Button");

    }
}
