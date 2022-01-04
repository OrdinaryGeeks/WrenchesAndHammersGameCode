using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 Direction;
    public bool selected;
    public bool oppSelected;

    void Start()
    {
        oppSelected = false;
        selected = false;
        Direction = Vector3.zero - transform.position;


    }

    // Update is called once per frame
    void Update()
    {

        if(!selected &! oppSelected)
        transform.position += Direction * Time.deltaTime * 2;
        
    }
}
