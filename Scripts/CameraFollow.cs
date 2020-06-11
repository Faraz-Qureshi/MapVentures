using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{


    private Transform   playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //  Current  position of Camera In  Var Temp
        Vector3 temp = transform.position;
        

        //Here we set the camera's positions to current player's position, Transform Alignment
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;
       //we  set the camera's temp position to the camera's current position
        transform.position = temp;

       

       
    }
}// class



