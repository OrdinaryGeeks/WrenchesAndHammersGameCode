using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEmitter : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();

        var emission = ps.emission;
        emission.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        var main = ps.main;
        main.startColor = transform.parent.gameObject.GetComponent<ChooseColor>().color;
    }
}
