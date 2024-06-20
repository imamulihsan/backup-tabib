using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlantsHealth : MonoBehaviour
{
    public float delay = 0.5f;
    
    public int damage = 1;
     public float respawnTime = 20f;

    [SerializeField] public int currentHealth,maxHealth;

    public UnityEvent<GameObject>OnHitWithReference,OnDeathWithReference;
    public UnityEvent OnRespawnWithReference;
    [SerializeField] public bool isDead= false;

    Rigidbody2D rb;

    Agent agent;

    Animator animator;

    CapsuleCollider2D capsuleCollider2D;

    PlayerExperience playerExperience;
    AgentMover agentMover;

    

public void Awake(){
    animator= GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    capsuleCollider2D=GetComponent<CapsuleCollider2D>();
    agent = GetComponent<Agent>();
    agentMover = GetComponent<AgentMover>();
    playerExperience =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
   
}



    public void InitializeHealth(int healthValue){
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead= false;
    }

    public void GetHit(int amount,GameObject sender)
    {   if(isDead)
        return;
        if(sender.layer == gameObject.layer)
        return;

       currentHealth = currentHealth - playerExperience.Damage();
        StartCoroutine(Hitted(sender));
       
    }
    public IEnumerator Hitted(GameObject sender){
        float wait = 0.5f;
        if(currentHealth >0 ){
            OnHitWithReference?.Invoke(sender);
            animator.SetBool("GettingHit",true);
            yield return new WaitForSeconds(wait);
            animator.SetBool("GettingHit",false);
          
            
        }else{
            StopAllCoroutines();
            rb.velocity = new UnityEngine.Vector3 (0,0);
            OnDeathWithReference?.Invoke(sender);
             isDead =true;
             animator.SetTrigger("Death");
            StartCoroutine(Death()); 
        }

         
    }
    public IEnumerator Death(){
     
      
      

        yield return new WaitForSeconds(respawnTime);
        animator.SetTrigger("Respawn");
        yield return new WaitForSeconds(1f);
         isDead =false;
        gameObject.SetActive(true);
        OnRespawnWithReference?.Invoke();
        InitializeHealth(maxHealth); 

        capsuleCollider2D.enabled = true;
    }

    public void AddHealth(int healthBoost = 2)
    {
        
        int val = currentHealth+ healthBoost;
        currentHealth = val ;
    }

   
}
