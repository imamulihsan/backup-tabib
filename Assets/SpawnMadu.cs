using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnMadu: MonoBehaviour
{
   public GameObject item ;
   Agent agent;
   [SerializeField ] public Transform thisGameObject;
   
   

   public void Start()
   {

    agent = GetComponent<Agent>();
    gameObject.tag="Madu";
    

   }
   public void SpawnDroppedItem()
   {
    Vector2 playerPos= new Vector2(thisGameObject.position.x -1.5f , thisGameObject.position.y - 1.5f);
    Instantiate(item,playerPos,Quaternion.identity); 
   }
   
}
