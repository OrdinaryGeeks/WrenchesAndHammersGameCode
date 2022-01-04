using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

  //  GameObject attacker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {

       // Debug.Log(ColorNinja.AttackPlayer);
        if(!ColorNinja.AttackPlayer && ColorNinja.isAttacking)
        if(other.gameObject.tag=="PlayerSphere")
        {

             //   Debug.Log("got a slash");
          Runner.DamageTaken -= 2;

            ColorNinja.AttackPlayer = true;



        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
