using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnAmbush : MonoBehaviour
{
   public GameObject item ;
   Agent agent;
   private Transform thisGameObject;
   public string itemTag;
   
   

   public void Start()
   {
    thisGameObject =GameObject.FindGameObjectWithTag("Ambush").transform;
    agent = GetComponent<Agent>();
    gameObject.tag = itemTag;
    

   }
   public void SpawnDroppedItem()
   {
    Vector2 playerPos= new Vector2(thisGameObject.position.x -6 , thisGameObject.position.y  );
    Instantiate(item,playerPos,Quaternion.identity); 
   }
   
}
