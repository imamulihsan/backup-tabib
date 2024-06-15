using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionEffect : MonoBehaviour {

    private Health health;
 
    public int healthBoost;

    private void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    public void Use(int healthBoost = 2) {
     
      
        health.currentHealth=health.currentHealth+ healthBoost;
       
        Destroy(gameObject);
    
    }
}