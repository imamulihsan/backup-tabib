using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnLidahBuaya: MonoBehaviour
{
   public GameObject item ;
   Agent agent;
  public Transform thisGameObject;
   
   

   public void Start()
   {
    
    agent = GetComponent<Agent>();
    gameObject.tag="LidahBuaya";
    

   }
   public void SpawnDroppedItem()
   {
    Vector2 playerPos= new Vector2(thisGameObject.position.x, thisGameObject.position.y - 1.5f);
    Instantiate(item,playerPos,Quaternion.identity); 
   }
   
}
