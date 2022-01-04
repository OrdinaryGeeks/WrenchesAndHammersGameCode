using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  

public class Screwable : MonoBehaviour
{
    // Start is called before the first frame update

    public float Screwed;
    public Slider Slider;
    void Start()
    {
        //   Slider.maxValue = 10;
        //     Slider.minValue = -10;

        Screwed = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(Screwed != 0)
        {

            gameObject.transform.rotation = Quaternion.Euler(0.0f, Screwed, 0.0f); 


        }
       
    }
}
