using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkWallKey : MonoBehaviour
{

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "JetPackWall")
        {

            Debug.Log("JPW");
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GunWall")
            if (Runner.playerColor.r >=Parkcolor2019.gunKeyColor.r && Runner.playerColor.g >= Parkcolor2019.gunKeyColor.g && Runner.playerColor.b >= Parkcolor2019.gunKeyColor.b)
            {
                Parkcolor2019.passGun = true;
                Debug.Log(Parkcolor2019.passGun);
                Runner.playerColor -= Parkcolor2019.gunKeyColor;
                Destroy(other.gameObject);
            }
            else
                Debug.Log(Parkcolor2019.gunKeyColor + " " + Runner.playerColor);
        if (other.gameObject.tag == "JetPackWall")

            if (Runner.playerColor.r >= Parkcolor2019.jetPackKeyColor.r && Runner.playerColor.g >= Parkcolor2019.jetPackKeyColor.g && Runner.playerColor.b >= Parkcolor2019.jetPackKeyColor.b)
            {
                Parkcolor2019.passJetPack = true;
                Debug.Log(Parkcolor2019.passJetPack);
                Destroy(other.gameObject);
                Runner.playerColor -= Parkcolor2019.jetPackKeyColor;
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
