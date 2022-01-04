using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchesAndHammers : MonoBehaviour
{

    public static List<GameObject> robots;
    public GameObject Robot;
    public float Timer;
    // Start is called before the first frame update
    void Start()
    {
        robots = new List<GameObject>();

    }


    // Update is called once per frame
    void Update()
    {


        Timer += Time.deltaTime;
        if(Timer > 1)
        {

            Vector3 randomPoint = Random.insideUnitSphere;
            float x = randomPoint.x  * 3.0f ;
            float z = randomPoint.z * 3.0f ;



            Timer = 0;

            if(robots.Count < 5)
          robots.Add(Instantiate(Robot, new Vector3(x, 0.2f, z), Quaternion.identity));



        }
        
        
    }
}
