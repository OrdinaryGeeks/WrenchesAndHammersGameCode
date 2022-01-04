using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {

      //  Debug.Log("Got OTE Player attack");
      // Debug.Log(Runner.AttackColorNinja1);
      //  Debug.Log(Runner.isAttacking);
        // Debug.Log(ColorNinja.AttackPlayer);
        if (!Runner.AttackColorNinja1 && Runner.isAttacking)
            if (other.gameObject.tag == "ColorNinjaASpheres")
            {
              //  PopUpAttack.spawnButton = true;
             //   Debug.Log("got a slash");
                ColorNinja.Health -= 1;
                //Debug.Log("got a slash");
                //Runner.DamageTaken -= 2;

                Runner.AttackColorNinja1 = true;



            }
        if(!Runner.AttackColorCop1 && Runner.isAttacking)
        {
            if (other.gameObject.tag == "ColorCopASpheres")
            {
               // PopUpAttack.spawnButton = true;

                //   Debug.Log("got a slash");
                ColorCop.Health -= 1;
                //Debug.Log("got a slash");
                //Runner.DamageTaken -= 2;

                Runner.AttackColorCop1 = true;



            }


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
