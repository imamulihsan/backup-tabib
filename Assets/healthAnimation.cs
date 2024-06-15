using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthAnimation : MonoBehaviour
{
    float delay =0.5f;
 HealthUI healthUI;
Health health;
public Sprite emptyHeart;
 
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
          health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

       
    }

    public void Update()
    {
      Animation();
    }

     public IEnumerator Animation(){
      
        
         animator.SetBool("GettingHit",true);
            yield return new WaitForSeconds(delay);
            
     }
}



