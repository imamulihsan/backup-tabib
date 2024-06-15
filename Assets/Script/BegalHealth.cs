using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BegalHealth : MonoBehaviour
{
    public float delay = 1.5f;
    
    public int damage = 1;

    [SerializeField] public int currentHealth,maxHealth;

    public UnityEvent<GameObject>OnHitWithReference,OnDeathWithReference,OnRespawnWithReference;

    [SerializeField] public bool isDead= false;

    Rigidbody2D rb;

    Agent agent;

    Animator animator;

    CapsuleCollider2D capsuleCollider2D;

    PlayerExperience playerExperience;
    AgentMover agentMover;
    public float respawnTime = 20f;
    SpawnBegal spawnBegal;
    

    

public void Awake(){
    animator= GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    capsuleCollider2D=GetComponent<CapsuleCollider2D>();
    agent = GetComponent<Agent>();
    agentMover = GetComponent<AgentMover>();
    playerExperience =GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerExperience>();
    spawnBegal = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<SpawnBegal>();
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

       

        if(currentHealth >0 ){
            OnHitWithReference?.Invoke(sender);
            
        }else{
            capsuleCollider2D.enabled= false;
            OnDeathWithReference?.Invoke(sender);
            StopAllCoroutines();
          
           
            StartCoroutine(Death());
            
        }

    }     

    public IEnumerator Death(){
     
      
       
        
        isDead =true;
        agentMover.acceleration =0;
         animator.SetTrigger("Death");
        yield return new  WaitForSeconds(delay);
       
      StartCoroutine(Respawn(gameObject));

     
    }

    private IEnumerator Respawn(GameObject sender)
    {
        if(sender.layer != gameObject.layer)
        
        StopAllCoroutines();

        yield return new WaitForSeconds(respawnTime);
        animator.SetTrigger("Respawn");
        yield return new WaitForSeconds(1f);
         isDead =false;
        gameObject.SetActive(true);
        OnRespawnWithReference?.Invoke(sender);
        InitializeHealth(maxHealth); 

        capsuleCollider2D.enabled = true;

        agentMover.acceleration = 50;
        
       
    }

   
}
