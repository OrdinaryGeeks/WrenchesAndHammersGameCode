using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject runner;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       // transform.position = runner.transform.position;
        //Camera.transform.Rotate()
        //transform.rotation = runner.transform.roa
       // transform.Rotate(0.0f, Time.deltaTime, 0.0f);// = runner.transform.rotation;
        //transform.position = runner.transform.position + runner.transform.rotation * 
        transform.position = runner.transform.position +   new Vector3(0.0f, 5.0f, -2.0f);
      //  transform.LookAt(runner.transform.position);
    }
}
