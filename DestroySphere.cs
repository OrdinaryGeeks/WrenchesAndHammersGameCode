using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySphere : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
            Destroy(this);
    }
    


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
