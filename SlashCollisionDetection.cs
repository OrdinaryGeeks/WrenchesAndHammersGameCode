using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashCollisionDetection : MonoBehaviour
{
    // Start is called before the first frame update

    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("OTE SLASH");
        if(other.gameObject.tag=="Player")
        {

            Debug.Log("Player");

          //  Runner.Health.value -= 2;


        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
