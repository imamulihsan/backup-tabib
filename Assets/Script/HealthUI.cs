using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Health health;
    healthAnimation healthAnimation;

    public Image[] healthUI;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    float delay = 0.5f;

    Animator animator;

    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        animator = GetComponent<Animator>();
        healthAnimation = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<healthAnimation>();
    }

    void Update()
    {
        if (health.currentHealth > health.maxHealth)
        {
            health.currentHealth = health.maxHealth;
        }

        for (int i = 0; i < health.maxHealth; i++)
        {
            if (i < health.currentHealth)
            {
                healthUI[i].sprite = fullHeart;
                // Ensure full hearts are fully visible
                CanvasGroup canvasGroup = healthUI[i].GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 1f;
                }
            }
            else
            {
                healthUI[i].sprite = emptyHeart;
                StartCoroutine(FadeCanvasGroup(healthUI[i].GetComponent<CanvasGroup>(), 0f, 1f, delay));
            }
        }
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }
}
