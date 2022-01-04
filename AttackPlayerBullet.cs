using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerBullet : MonoBehaviour
{


    public void OnTriggerEnter(Collider other)
    {

     //   Debug.Log("In OTE AP Bullet");
       // if (!ColorNinja.AttackPlayer)
         //   if (other.gameObject.layer. == "PlayerSphere")
            {
            Runner.DamageTaken -= 2;
            

                Destroy(transform.parent.gameObject);



            }

        if(other.gameObject.layer == 9)
        {
            WrenchAndHammer.damageTaken += 1;
            Destroy(transform.parent.gameObject);
            
        }

      //  print(other.gameObject.layer);
        if (other.gameObject.layer == 13)
            Destroy(transform.parent.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
