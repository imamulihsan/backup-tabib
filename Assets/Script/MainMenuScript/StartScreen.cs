using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StartScreen : MonoBehaviour
{
    // [Header ("Animation")]
    // public bool scaleOnStart;
    // [SerializeField] float showTime = 0.3f, hideTime = 0.2f;
    // [SerializeField] Ease showEase = Ease.OutBack, hideEase = Ease.InBack;
    // [SerializeField] Transform tweenGo;

    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotMenu saveSlotMenu;

    [Header("Loading Scene")]
    public GameObject LoadingScreen;
    public Slider progressSlider;

    [Header("Scene Manager")]
    public GameObject MenuButton;
    public GameObject Option;
    public GameObject settingPanel;
    public GameObject instructionPanel;

    [Header("Menu Button")]
    [SerializeField] private Button loadGameButton;

    private void Start() 
    {
        DisableButtonsDependingOnData();
    }

    private void DisableButtonsDependingOnData()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            loadGameButton.interactable = false;
        }
    }

        //START MENU//
    public void StartGame()
    {
        // LoadingScreen.SetActive(true);
        // StartCoroutine(LoadSceneAsync(index));

        // //create a new game - which will iniialize our game data
        // DataPersistenceManager.instance.NewGame();

        // //Load the gameplay scene - which will in turn save the game because of
        // //OnSceneUnLoaded() in the DataPersistenceManager
        // SceneManager.LoadSceneAsync("SampleScene");
        // AudioManager.Instance.musicSource.Stop();
        // AudioManager.Instance.PlayMusic("Game");
        AudioManager.Instance.PlaySFX("Button");
        saveSlotMenu.ActivateMenu(false);
        this.DeactivateMenu();

    }


    public void LoadGame()
    {
        AudioManager.Instance.PlaySFX("Button");
        saveSlotMenu.ActivateMenu(true);
        this.DeactivateMenu();
    }

    // private void OnEnable() 
    // {
    //     if (scaleOnStart)
    //     {
    //         if (tweenGo == null)
    //         {
    //             tweenGo = transform;
    //         }
    //         tweenGo.localScale = Vector3.zero;
    //         tweenGo.DOScale(1, showTime).SetEase(showEase);
    //     }    
    // }

    public void OptionClicked()
    {
        if (Option.activeInHierarchy == false)
        {
            // transform.LeanScale(Vector2.one, 0.8f);
            Option.SetActive(true);
            settingPanel.SetActive(true);
            MenuButton.SetActive(false);
            instructionPanel.SetActive(false);
            
            AudioManager.Instance.PlaySFX("Button");
        }
        else
        {
            // transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
            Option.SetActive(false);
            MenuButton.SetActive(true);
            AudioManager.Instance.PlaySFX("Button");
        }
    }

    public void SettingClicked()
    {
        // image.transform.Rotate(0f, 0f, 0f);
        settingPanel.SetActive(true);
        instructionPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void InstructionClicked()
    {
        // image.transform.Rotate(0f, 0f, 0f);
        instructionPanel.SetActive(true);
        settingPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Button");
    }

    public void QuitGame()
    {
        Application.Quit();
        AudioManager.Instance.PlaySFX("Button");
    }

    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        // LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            progressSlider.value = progressValue;
            yield return null;
        }

        // progressSlider.value = 0;
        // LoadingScreen.SetActive(true);

        // AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);
        // asyncOperation.allowSceneActivation = false;
        // float progress = 0;
        // while (!asyncOperation.isDone)
        // {
        //     progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
        //     progressSlider.value = progress;
        //     if (progress >=0.9f)
        //     {
        //         progressSlider.value = 1;
        //         asyncOperation.allowSceneActivation = true;
        //     }
        //     yield return null;
        // }
    }
    
    //LOAD MENU//
    public void DataMenu(int index)
    {
        LoadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsync(index));

        //create a new game - which will iniialize our game data
        DataPersistenceManager.instance.LoadGame();

        //Load the gameplay scene - which will in turn save the game because of
        //OnSceneUnLoaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("SampleScene");
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Game");
        AudioManager.Instance.PlaySFX("Button");
    }

    public void ActivateMenu()
    {
        this.gameObject.SetActive(true);
        DisableButtonsDependingOnData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }

    // public void SaveGameClicked()
    // {
    //     DataPersistenceManager.instance.SaveGame();
    // }
}
