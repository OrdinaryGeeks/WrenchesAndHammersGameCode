using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWallCollisions : MonoBehaviour
{
    public static bool LW;
    public static bool RW;

    public void OnCollisionEnter(Collision collision)
    {
      //  Debug.Log("Collision");

        if (collision.collider.gameObject.name.Contains("LeftWall"))
        {
          //  Debug.Log("LeftWall");

            LW = true;

        }

        if (collision.collider.gameObject.name.Contains("RightWall"))
        {
        //    Debug.Log("RightWall");
            RW = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.name.Contains("LeftWall"))
        {
            LW = false;
        //    Debug.Log("Making false LW");
        }

        if (collision.collider.gameObject.name.Contains("RightWall"))
        {
            RW = false;
          //  Debug.Log("Making RW false");
        }
    }
    //public void OnTriggerEnter(Collider other)
    //{

    //    if (other.gameObject.name == "LeftWall")
    //    {
    //        transform.up = Vector3.right;
    //        Debug.Log("LeftWall");
    //        LW = true;



    //    }
    //    if (other.gameObject.name == "RightWall")
    //    {
    //        transform.up = Vector3.left;
    //        Debug.Log("RightWall");
    //        RW = true;
    //    }

    //  //  character.GetComponent<Renderer>().material.color += other.gameObject.GetComponent<Renderer>().material.color;
    //}
    //public void OnTriggerExit(Collider other)
    //{

    //    if (other.gameObject.name == "LeftWall")
    //    {
    //        transform.up = Vector3.right;
    //        Debug.Log("LeftWall");
    //        LW = false;



    //    }
    //    if (other.gameObject.name == "RightWall")
    //    {
    //        transform.up = Vector3.left;
    //        Debug.Log("RightWall");
    //        RW = false;
    //    }

    //    character.GetComponent<Renderer>().material.color += other.gameObject.GetComponent<Renderer>().material.color;
    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
