using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundColorEmitter : MonoBehaviour
{
    // Start is called before the first frame update

    private ParticleSystem ps;
    void Start()
    {

        ps = GetComponent<ParticleSystem>();

      //  Color color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        // color.a = 255.0f;

        var main = ps.main;
        int intensity = Random.Range(0, 256);

     //   main.startColor = color;
    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
