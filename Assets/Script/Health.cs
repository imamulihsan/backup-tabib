using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDataPersistence
{
    public float delay = 2f;
    public GameObject DeathPanel;



     public int currentHealth=2,maxHealth = 3;

    public UnityEvent<GameObject>OnHitWithReference,OnDeathWithReference;

    [SerializeField] public bool isDead= false;

    Rigidbody2D rb;

    Agent agent;

    Animator animator;

    CapsuleCollider2D capsuleCollider2D;
    BegalHealth begalHealth;

    healthAnimation healthAnimation;

    private CanvasGroup deathPanelCanvasGroup;

    float Wait = 0.0f;

    Animation anima;

    public void Awake()
    {
        animator= GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D=GetComponent<CapsuleCollider2D>();
        agent = GetComponent<Agent>();
        begalHealth =GameObject.FindGameObjectWithTag("Enemy").GetComponent<BegalHealth>();
        healthAnimation = GameObject.FindGameObjectWithTag("HealthUI").GetComponent<healthAnimation>();
        deathPanelCanvasGroup = DeathPanel.GetComponent<CanvasGroup>();
        anima = GetComponent<Animation>();
    }

    //Connect to GameData and Data Persistence for saved game health 
    public void LoadData(GameData data)
    {
        this.currentHealth = data.currentHealth;
    }

    public void SaveData(GameData data)
    {
        if (currentHealth != 0)
        {
            data.currentHealth = this. currentHealth;
        }
        else
        {
            currentHealth = 2;
        }
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead= false;
    }

    public void GetHit(int amount,GameObject sender)
    {   
        if(isDead)
        return;
        if(sender.layer == gameObject.layer)
        return;

       currentHealth = currentHealth - begalHealth.damage;
    
        if(currentHealth >0 )
        {
            OnHitWithReference?.Invoke(sender);   
        }
        else
        {
            StopAllCoroutines();
            rb.velocity = new UnityEngine.Vector3 (0,0);
            OnDeathWithReference?.Invoke(sender);
            StartCoroutine(Death());
        }

    }     

    public IEnumerator Death()
    {
        animator.SetTrigger("Death");
        isDead =true;
        yield return new  WaitForSeconds(delay);
       
        StartCoroutine(FadeInDeathPanel());
    }

    public void AddHealth(int healthBoost = 2)
    {
        int val = currentHealth+ healthBoost;
        currentHealth = val ;
    }

    public IEnumerator FadeInDeathPanel()
    {
        DeathPanel.SetActive(true);
        deathPanelCanvasGroup.alpha = 0;

        float duration = 1f;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            deathPanelCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / duration);
      
            yield return null;
        }

        deathPanelCanvasGroup.alpha = 1;
  
        Destroy(gameObject);
    }
     
   
}
