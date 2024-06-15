using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnBawangPutih : MonoBehaviour
{
   public GameObject item ;
   Agent agent;
   private Transform thisGameObject;
   
   

   public void Start()
   {
    thisGameObject =GameObject.FindGameObjectWithTag("BawangPutih").transform;
    agent = GetComponent<Agent>();
    gameObject.tag="BawangPutih";
    

   }
   public void SpawnDroppedItem()
   {
    Vector2 playerPos= new Vector2(thisGameObject.position.x, thisGameObject.position.y - 1.5f);
    Instantiate(item,playerPos,Quaternion.identity); 
   }
   
}
