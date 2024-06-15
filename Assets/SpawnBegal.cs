using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class SpawnBegal : MonoBehaviour
{
   Agent agent;
   private Transform thisGameObject;
   
   float respawnTime = 5f;
   BegalHealth begalHealth;
   CapsuleCollider2D capsuleCollider2D;
   AgentMover agentMover;
   
    public UnityEvent OnRespawn;
   
public void Start()
{
    begalHealth =GetComponentInChildren<BegalHealth>();
    capsuleCollider2D = GetComponentInChildren<CapsuleCollider2D>();
    agentMover = GetComponentInChildren<AgentMover>();
    }
 public IEnumerator Respawn()
    {
       
       
       
 OnRespawn?.Invoke();
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("Respawning...");

        gameObject.SetActive(true); // Re-enable the game object
     
        begalHealth.InitializeHealth(begalHealth.maxHealth); // Reset health
        capsuleCollider2D.enabled = true;
        agentMover.acceleration = 50; // Reset the agent's acceleration if needed
        
        Debug.Log("done..");
    }
   
}
