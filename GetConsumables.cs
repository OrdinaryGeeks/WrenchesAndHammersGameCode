using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetConsumables : MonoBehaviour
{

    public GameObject Runner;
    public GameObject Ninjaneer;

    public void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Sword"))
        {

            if (!Ninjaneer.GetComponent<Runner>().hasSword)
                Ninjaneer.GetComponent<Runner>().equipment.Add("Sword");
            Ninjaneer.GetComponent<Runner>().hasSword = true;
            Destroy(other.gameObject);



        }
        if (other.name.Contains("Rifle"))
        {
            if (!Ninjaneer.GetComponent<Runner>().hasGun)
                Ninjaneer.GetComponent<Runner>().equipment.Add("Gun");
            Ninjaneer.GetComponent<Runner>().hasGun = true;
            Destroy(other.gameObject);


        }
        if (other.name.Contains("Ammo"))
        {

            Ninjaneer.GetComponent<Runner>().ammoCount += 20;
            Destroy(other.gameObject);

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
