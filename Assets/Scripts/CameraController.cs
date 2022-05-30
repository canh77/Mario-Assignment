using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public Transform playerTransform;

    
    void Update()
    {
        if(playerTransform.position.x > transform.position.x )
        {
            transform.position = new Vector3(playerTransform.position.x,
                transform.position.y,transform.position.z);
             
        }    
    }
}
