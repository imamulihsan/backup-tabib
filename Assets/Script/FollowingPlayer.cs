using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingPlayer : MonoBehaviour
{
    // Start is called before the first frame update
   
    public Transform player;
    Camera cam;

    // Update is called once per frame

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update () {
        
        transform.position = player.transform.position + new Vector3(0, 0, -20);
    }
    
}
