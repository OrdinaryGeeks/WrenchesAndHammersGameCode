using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableSelected : MonoBehaviour
{
    public bool selected;
    public Renderer renderer;
    public Color originalColor;
    public Material material;
    public bool arc_Wrench;
    public bool matter_Hammer;
    public bool synth_Merge;
    public bool carry;
    public Transform Carry_Transform;
    public bool isUnlocked;
    public float wrench;
    public float unlock;
    public bool combine_range;
    public float combine_time;
    public bool combine_selected;
    //   public bool combine_deselected;
    public GameObject player;
    // Start is called before the first frame update
    public bool enemy_in_range;
    public bool enemy_rotating;
    void Start()
    {
        enemy_rotating = false;
        enemy_in_range = false;
        combine_range = false;
        combine_time = 0;
        // combine_deselected = true;
        combine_selected = false;
        renderer = gameObject.GetComponent<Renderer>();
        material = renderer.material;
        originalColor = renderer.material.color;
        wrench = 50;
        unlock = 15;
        isUnlocked = false;
        // combine = false;
    }



    public void checkUnlock()
    {

        // if(wrench)
        float difference = wrench - unlock;
        if (Mathf.Abs(difference) < 30)
            isUnlocked = true;


    }
    public void set_arc_wrench_exclusive()
    {
        arc_Wrench = true;
        synth_Merge = false;
        matter_Hammer = false;
        //combine = false;



    }

    public void set_synth_merge_exclusive()
    {
        arc_Wrench = false;
        synth_Merge = true;
        matter_Hammer = false;
        //   combine = false;

    }

    public void set_combine_exclsuve()
    {
        //combine = true;
        arc_Wrench = false;
        synth_Merge = false;
        matter_Hammer = false;
    }
    // Update is called once per frame
    void Update()
    {

        //  if (player.GetComponent<Scientist>().combine)


        //    if (Mathf.Abs(Vector3.Distance(transform.position, player.transform.position)) < 5)
        //         combine = true;
        //     else
        //         combine = false;


        /////   }
        //  else
        //      combine = false;


        if (arc_Wrench)
        {
            // Debug.Log("CS");
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);

        }
        else if (matter_Hammer)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 1);

        }
        else if (synth_Merge)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        else if (combine_selected)
        {

            gameObject.GetComponent<Renderer>().material.color = new Color(.0f, 0, 0);

        }
        else if (combine_range & !combine_selected && WrenchAndHammer.combine)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(.3f, 0f, 0f);

        }

        else if (enemy_in_range & !enemy_rotating)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
        else if (enemy_in_range & enemy_rotating)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0.3f, 0, 0);
        }
        else
            gameObject.GetComponent<Renderer>().material.color = originalColor;


        if (carry)
        {
            transform.rotation = Carry_Transform.rotation;
            transform.position = Carry_Transform.position;

        }
    }
}
