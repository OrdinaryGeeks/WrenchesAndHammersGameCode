using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTransfer : MonoBehaviour
{
    public bool emitting;
    ParticleSystem ps;
    float elapsedTime;
    float startSize;
    // Start is called before the first frame update
    void Start()
    {
        startSize = 1.0f;
        emitting = false;
        ps = GetComponent<ParticleSystem>();
    }

    public void setEmittersActivated(string name)
    {
        if(name == "Tree1")
        {
            Debug.Log(Parkcolor2019.emittersActivated[0]);
            Parkcolor2019.emittersActivated[0] = false;
            Debug.Log(Parkcolor2019.emittersActivated[0]);
        }
        if(name == "Tree2")
        {
            Parkcolor2019.emittersActivated[1] = false;
        }
        if (name == "Tree3")
        {
            Parkcolor2019.emittersActivated[2] = false;
        }
        if (name == "Tree4")
        {
            Parkcolor2019.emittersActivated[3] = false;
        }
        if (name == "Tree5")
        {
            Parkcolor2019.emittersActivated[4] = false;
        }
        if (name == "Tree6")
        {
            Parkcolor2019.emittersActivated[5] = false;
        }
        if (name == "Tree7")
        {
            Parkcolor2019.emittersActivated[6] = false;
        }
        if (name == "Tree8")
        {
            Parkcolor2019.emittersActivated[7] = false;
        }
        if (name == "Tree9")
        {
            Parkcolor2019.emittersActivated[8] = false;
        }
        if (name == "Tree10")
        {
            Parkcolor2019.emittersActivated[9] = false;
        }
        if (name == "Tree11")
        {
            Parkcolor2019.emittersActivated[10] = false;
        }
        if (name == "Tree12")
        {
            Parkcolor2019.emittersActivated[11] = false;
        }
        if (name == "ClasicCar")
        {
            Parkcolor2019.emittersActivated[12] = false;
        }
        if (name == "FutureCar")
        {
            Parkcolor2019.emittersActivated[13] = false;
        }
        if (name == "FutureCopCar")
        {
            Parkcolor2019.emittersActivated[14] = false;
        }






    }
    public void OnTriggerStay(Collider other)
    {
        Runner.acquireColor = true;

      //  Debug.Log(gameObject.transform.parent.gameObject.transform.parent.gameObject.name);
        var main = ps.main;
        var emission = ps.emission;
        if (other.gameObject.tag == "Player")
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > .125f)
            {

                //    Debug.Log(main.startSize);

                var startSizeConstant = main.startSize.constant;
              startSizeConstant = main.startSize.constant - elapsedTime*2.0f;

                main.startSize = new ParticleSystem.MinMaxCurve(startSize, startSize);

                startSize -= elapsedTime * 2.0f;
                Debug.Log(startSize);
      
                if (startSize <= 0)
                {
                    // emission.enabled = false;
                    //   Runner.acquireColor = false;


                    setEmittersActivated(gameObject.transform.parent.gameObject.transform.parent.gameObject.name);
                    Debug.Log(gameObject.transform.parent.gameObject.name);
                    Destroy(gameObject.transform.parent.gameObject);
                    Parkcolor2019.emitterCount--;


                }
                elapsedTime = 0;

                Runner.R += main.startColor.color.r / 4;
                Runner.G += main.startColor.color.g / 4;
                Runner.B += main.startColor.color.b / 4;
            }



        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {

            Runner.acquireColor = false;
        }
    }
    public void OnTriggerEnter(Collider other)
    {

        var main = ps.main;

        if (other.gameObject.tag == "Player")
        {
        //    Debug.Log(main.startSize);
          //  main.startSize = main.startSize.constant - Time.deltaTime / 10;




        }
    }
    public void OnCollisionEnter(Collision collision)
    {

        var main = ps.main;
        
        if(collision.collider.gameObject.tag == "Player")
        {

            main.startSize = main.startSize.constant - Time.deltaTime / 10;



        }

    }
    // Update is called once per frame
    void Update()
    {

        
        
    }
}
