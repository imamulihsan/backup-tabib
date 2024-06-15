using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawn : MonoBehaviour
{
   public GameObject item ;
   Agent agent;
   private Transform player;
   public string itemTag;
   
   

   public void Start()
   {
    player =GameObject.FindGameObjectWithTag("Player").transform;
    agent = GetComponent<Agent>();
    gameObject.tag = itemTag;
    

   }
   public void SpawnDroppedItem()
   {
    Vector2 playerPos= new Vector2(player.position.x, player.position.y - 1.5f);
    Instantiate(item,playerPos,Quaternion.identity); 
   }
   
}
