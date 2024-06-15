using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour, IDataPersistence
{
    Agent agent;
    Health health;
    GameObject player;
    NPCInteractable nPCInteractable;
    public int maxXP = 0;
    public int damageValue = 1;
    public int currentXP = 0;

    bool attackIsUpgraded = false;

    public GameObject levelUpPanel;

    bool healthIsUpgraded = false;

    public GameObject healthPanel;

    public Image[] attackPowerUI;
    public GameObject attackPanel;

    public int currentLevel = 1;
    bool levelIsUpgraded = false;

    public TextMeshProUGUI levelUI;

    CanvasGroup canvasGroup;

    void Awake()
    {
        agent = GetComponent<Agent>();
        nPCInteractable = GetComponent<NPCInteractable>();
        health = GetComponent<Health>();
        canvasGroup = levelUpPanel.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        Damage();
    }

    public int Damage()
    {
        if (currentXP >= 100 && levelIsUpgraded== false)
        {
            StartCoroutine(LevelUPNotification(2));
            levelIsUpgraded =true;
            currentLevel =2;
        }

        if (currentXP >= 500 && !attackIsUpgraded && !healthIsUpgraded && currentLevel ==2)
        {
            StartCoroutine(LevelUPNotification(3));
            damageValue += 1;
            health.maxHealth += 1;
            currentLevel += 1;

            healthPanel.SetActive(true);
            attackPanel.SetActive(true);
            levelIsUpgraded = true;
            attackIsUpgraded = true;
            healthIsUpgraded = true;
        }
        return damageValue;
    }

    IEnumerator LevelUPNotification(int newLevel)
    {
        yield return StartCoroutine(FadeInPanel());
        levelUI.text = newLevel.ToString();
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FadeOutPanel());
    }

    private IEnumerator FadeInPanel()
    {
        canvasGroup.alpha = 0;
        levelUpPanel.SetActive(true);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / 0.5f;  
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOutPanel()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 0.5f;  
            yield return null;
        }
        canvasGroup.alpha = 0;
        levelUpPanel.SetActive(false);
    }

   //Connect to GameData and Data Persistence for saved game health 
    public void LoadData(GameData data)
    {
        this.currentXP = data.currentXP; 
        this.damageValue = data.damageValue;
        this.currentLevel = data.currentLevel;
    }

    public void SaveData(GameData data)
    {
        if (health.currentHealth != 0)
        {
            data.currentXP = this. currentXP;
            data.damageValue = this.damageValue;
            data.currentLevel = this.currentLevel;
        }
        else
        {
            currentXP = 0;
            currentLevel = 1;
            damageValue = 1;
        }
    }
}
