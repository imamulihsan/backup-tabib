using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauseSetting : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject Pause;
    public GameObject SoundPanel;
    
    [Header("Animation")]
    [SerializeField] RectTransform PausePanelRect;
    [SerializeField] float bottomPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup; //Dark panel canvas group

    private SaveSlotMenu saveSlotMenu;
    private SaveSlot[] saveSlots;

    public void PauseButton()
    {
        PausePanelIntro();
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
        Pause.SetActive(true);
        AudioManager.Instance.PlaySFX("Button");
    }

    public async void ResumeClicked()
    {
        await PausePanelOutro();
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void SettingClicked()
    {
        Pause.SetActive(false);
        SoundPanel.SetActive(true);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void BackClicked()
    {
        Pause.SetActive(true);
        SoundPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void QuitClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScreen");
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Theme");
        AudioManager.Instance.PlaySFX("Button");
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        PausePanelRect.DOAnchorPosY(middlePosY,tweenDuration).SetUpdate(true);
    }

    async Task PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await PausePanelRect.DOAnchorPosY(bottomPosY,tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }

}
