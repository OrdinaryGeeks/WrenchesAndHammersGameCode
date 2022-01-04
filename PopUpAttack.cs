using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpAttack : MonoBehaviour
{
    public GameObject Button;
     public Canvas canvas;
    public static bool spawnButton;
    // Start is called before the first frame update
    void Start()
    {
        spawnButton = false;
        
    }

    public void DisposeButton(Button button)
    {

        


    }
    // Update is called once per frame
    void Update()
    {
        
        if(spawnButton)
        {
            Debug.Log("Spawn a button");
           GameObject spawnedButton = Instantiate(Button, canvas.GetComponent<Transform>());

            spawnedButton.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>(), false);
            spawnButton = false;

            Vector2 pos = new Vector2(Runner.position.x, Runner.position.z);  // get the game object position
            
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(pos);  //convert game object position to VievportPoint

            // set MIN and MAX Anchor values(positions) to the same position (ViewportPoint)
            Button.GetComponent<RectTransform>().anchorMin = viewportPoint;
            Button.GetComponent<RectTransform>().anchorMax = viewportPoint;

            
            //Button.GetComponent<RectTransform>().anchoredPosition = viewportPoint;


        }


    }


}
