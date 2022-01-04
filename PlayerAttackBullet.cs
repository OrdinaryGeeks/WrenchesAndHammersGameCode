using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBullet : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {

        //    Debug.Log("In OTE Bullet");
        //  Debug.Log(other.gameObject.name);
        // if (!ColorNinja.AttackPlayer)

        if (!gameObject.transform.parent.GetComponent<Projectile>().collided)
        {
            if (other.gameObject.tag == "ColorNinjaASpheres")
            {
                ColorNinja.Health -= 2;


                Destroy(transform.parent.gameObject);



            }

            if (other.gameObject.tag == "ColorCopASpheres")
            {

                ColorCop.Health -= 2;

                Destroy(transform.parent.gameObject);

            }

            if (other.gameObject.tag == "EnemySpheres")
            {

                print("EnemeySpheres");
                GameObject.Find(other.gameObject.GetComponent<Identifier>().Owner).GetComponent<Opponent>().Health -= 1;
                Destroy(transform.parent.gameObject);
                gameObject.transform.parent.GetComponent<Projectile>().collided = true;
                //other.gameObject.GetComponent<Opponent>().Health -= 1;
            }
            else
            {

                //If hit a wall or environment destroy
                Destroy(transform.parent.gameObject);
                gameObject.transform.parent.GetComponent<Projectile>().collided = true;
            }
            }
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
