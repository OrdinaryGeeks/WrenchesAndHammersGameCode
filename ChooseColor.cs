using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseColor : MonoBehaviour
{
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        // color.a = 255.0f;


        int intensity = Random.Range(0, 256);

        GetComponent<Renderer>().material.color = color;

    //    if (gameObject.tag == "Tree" || gameObject.tag == "Car")
      //      Debug.Log(color);
     //   GetComponent<Light>().color = color;
        //  Debug.Log(color);
        // Debug.Log(color);
    }

    public void updateColor()
    {

        color = Random.ColorHSV(0, 1, 1, 1, 1, 1);

    }

    public void setColor(float Red, float Green, float Blue)
    {
        Debug.Log(Red + " " + Green + " " + Blue);
        color.r = Red;
        color.g = Green;
        color.b = Blue;
        GetComponent<Renderer>().sharedMaterial.color = color;

    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = color;

    }
}
