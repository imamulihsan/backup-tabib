using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuguHealing : MonoBehaviour
{
    CircleCollider2D circleCollider2D;
    Health health;
    public GameObject healingPanel;
    bool isHealingUsed = false;
    bool playerInRange = false;
      CanvasGroup canvasGroup;
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        canvasGroup = healingPanel.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0; 
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHealingUsed)
            {
                StartCoroutine(CooldownHealing());
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator CooldownHealing()
    {
        yield return StartCoroutine(FadeInPanel());
        health.currentHealth = health.maxHealth;
        isHealingUsed = true;
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FadeOutPanel());
        yield return new WaitForSeconds(60);
        isHealingUsed = false;
    }

    private IEnumerator FadeInPanel()
    {
        canvasGroup.alpha = 0;
        healingPanel.SetActive(true);
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime / 1f;  // Adjust the duration as needed
            yield return null;
        }
        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOutPanel()
    {
        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 1f;  // Adjust the duration as needed
            yield return null;
        }
        canvasGroup.alpha = 0;
        healingPanel.SetActive(false);
    }
}
