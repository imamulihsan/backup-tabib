using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform instructionPanelRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] Button nextButton, previousButton;

    private void Awake()
    {
        currentPage = 1;
        targetPos = instructionPanelRect.localPosition;
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            nextButton.interactable = true;
            previousButton.interactable = true;
            currentPage++;
            targetPos += pageStep;
            MovePage();
            AudioManager.Instance.PlaySFX("Button");
        }
        if (currentPage == maxPage)
        {
            nextButton.interactable = false;
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            previousButton.interactable = true;
            nextButton.interactable = true;
            currentPage--;
            targetPos -= pageStep;
            MovePage();
            AudioManager.Instance.PlaySFX("Button");
        }
        if (currentPage == 1)
        {
            previousButton.interactable = false;
        }
    }

    void MovePage()
    {
        instructionPanelRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }
}
